﻿namespace DogRestAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using DogRestAPI.Helpers;
    using DogRestAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Main logic for REST API functions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DogBreedController : ControllerBase
    {
        /// <summary>
        /// Context holds data entities for rest controller perform functions on.
        /// </summary>
        private readonly DogBreedContext dataContext;

        /// <summary>
        /// Ctor for DogBreedController. 
        /// Inits datacontext, and populates it if empty.
        /// </summary>
        /// <param name="context">Hand in data context from storage if available?</param>
        public DogBreedController(DogBreedContext context)
        {
            this.dataContext = context;

            if (this.dataContext.DogBreedItemList.Count() == 0)
            {
                Dictionary<string, string[]> jsonDictionary = JsonHelper.PopulateDictionaryFromJsonFile();
                foreach (string breedKey in jsonDictionary.Keys)
                {
                    DataContextHelper.PopulateDbContextWithNewDogBreed(breedKey, this.dataContext);
                }

                long breedId = dataContext.DogBreedItemList.Min(x => x.DogBreedItemId);
                foreach (string[] subBreeds in jsonDictionary.Values)
                {
                    DataContextHelper.PopulateDbContextWithNewDogBreedSubbreeds(subBreeds, breedId, this.dataContext);
                    breedId++;
                }
            }
        }

        #region HTTPGet
        /// <summary>
        /// Method fetches all breeds currently stored within the context. 
        /// Performed async, due to it being a web call.
        /// </summary>
        /// <returns>200 + List of all breeds within the context or 204 if list is empty</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DogBreedItem>>> GetAllBreeds()
        {
            using (WebClient client = new WebClient())
            {
                IEnumerable<DogBreedItem> items = dataContext.DogBreedItemList.Include(breed => breed.SubBreeds).AsEnumerable();

                if (items.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(items);
            }
        }

        /// <summary>
        /// Function to get the breed via it's identifying number.
        /// GET /api/dogbreed/{param}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 + requested breed or 404 not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DogBreedItem>> GetBreedByID(long id)
        {
            DogBreedItem requestedBreed;
            try
            {
                requestedBreed = await dataContext.DogBreedItemList.Include(breed => breed.SubBreeds).FirstAsync(breed => breed.DogBreedItemId == id);
                return Ok(requestedBreed);
            }
            catch
            {
                return NotFound("Did not exist");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{parentBreedID}/subbreed")]
        public async Task<ActionResult<DogBreedItem>> GetBreedSubBreeds(long parentBreedID)
        {
            List<DogSubBreed> requestedSubBreeds;
            try
            {
                requestedSubBreeds = dataContext.DogSubBreedItemList.Where(x => x.ParentBreedId == parentBreedID).ToList();
                return Ok(requestedSubBreeds);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentBreedID"></param>
        /// <returns></returns>
        [HttpGet("{parentBreedID}/subbreed/{subBreedId}")]
        public async Task<ActionResult<DogBreedItem>> GetBreedSubBreedByID(long parentBreedID, long subBreedId)
        {
            DogBreedItem requestedBreed;
            DogSubBreed requestedSubBreed;
            try
            {
                requestedBreed = await dataContext.DogBreedItemList.Include(breed => breed.SubBreeds).FirstAsync(breed => breed.DogBreedItemId == parentBreedID);
                requestedSubBreed = requestedBreed.SubBreeds.First(x => x.DogSubBreedId == subBreedId);
                return Ok(requestedSubBreed);
            }
            catch
            {
                return NotFound();
            }
        }
        #endregion

        #region HTTPDelete
        /// <summary>
        /// HTTP Method to delete breed via ID
        /// </summary>
        /// <param name="id">Unique Id of Dog Breed</param>
        /// <returns>204 If Successful, 404 Not found If Unsuccesful</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DogBreedItem>> DeleteBreedByID(long id)
        {
            if (id == -1)
            {
                await DeleteAllEntries();
            }

            DogBreedItem breedToDelete;
            List<DogSubBreed> subBreedsToDelete = new List<DogSubBreed>();
            try
            {
                breedToDelete = await dataContext.DogBreedItemList.FirstAsync(breed => breed.DogBreedItemId == id);
                subBreedsToDelete = dataContext.DogSubBreedItemList.Where(subbreed => subbreed.ParentBreedId == breedToDelete.DogBreedItemId).ToList();
                subBreedsToDelete.ForEach(subbreed => dataContext.DogSubBreedItemList.Remove(subbreed));
                dataContext.DogBreedItemList.Remove(breedToDelete);
                await dataContext.SaveChangesAsync();
                return Ok(breedToDelete);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// HTTP Method to delete breed via ID
        /// </summary>
        /// <param name="id">Unique Id of Dog Breed</param>
        /// <returns>204 If Successful, 404 Not found If Unsuccesful</returns>
        [HttpDelete("{id}/subbreed/{subBreedId}")]
        public async Task<ActionResult<DogBreedItem>> DeleteSubBreedByID(long id, long subBreedId)
        {
            DogSubBreed subBreedToDelete = new DogSubBreed();
            try
            {
                var parentBreed = await dataContext.DogBreedItemList.FirstAsync(breed => breed.DogBreedItemId == id);
                subBreedToDelete = dataContext.DogSubBreedItemList.First(subbreed => subbreed.ParentBreedId == parentBreed.DogBreedItemId && subbreed.DogSubBreedId == subBreedId);
                dataContext.DogSubBreedItemList.Remove(subBreedToDelete);
                await dataContext.SaveChangesAsync();
                return Ok(subBreedToDelete);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// HTTP Method to delete breed via ID
        /// </summary>
        /// <param name="id">Unique Id of Dog Breed</param>
        /// <returns>204 If Successful, 404 Not found If Unsuccesful</returns>
        public async Task<ActionResult<DogBreedItem>> DeleteAllEntries()
        {
            DogBreedItem breedToDelete;
            List<DogSubBreed> subBreedsToDelete = new List<DogSubBreed>();

            long id = dataContext.DogBreedItemList.First().DogBreedItemId;
            foreach (var breed in dataContext.DogBreedItemList)
            {
                breedToDelete = await dataContext.DogBreedItemList.FirstAsync(x => x.DogBreedItemId == id);
                subBreedsToDelete = dataContext.DogSubBreedItemList.Where(subbreed => subbreed.ParentBreedId == breedToDelete.DogBreedItemId).ToList();
                if (subBreedsToDelete.Count != 0)
                {
                    subBreedsToDelete.ForEach(subbreed => dataContext.DogSubBreedItemList.Remove(subbreed));
                }
                dataContext.DogBreedItemList.Remove(breedToDelete);
                await dataContext.SaveChangesAsync();
                try
                {
                    id = dataContext.DogBreedItemList.First().DogBreedItemId;
                }
                catch
                {
                    //List is now empty
                }
            }

            await dataContext.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region HTTPPost
        /// <summary>
        /// HTTPPost for pushing new dogbreed objects up.
        /// </summary>
        /// <param name="dogBreed">The dogbreed JSON payload</param>
        /// <returns>200 OK if successful, or Unprocessable entity if unsaveable/unserialsable</returns>
        [HttpPost]
        public async Task<ActionResult<DogBreedItem>> PostBreed(DogBreedItem dogBreed)
        {
            try
            {
                //Validate breed details
                if (dataContext.DogBreedItemList.Any(x => x.BreedName == dogBreed.BreedName))
                {
                    return UnprocessableEntity($"\"Error\" : \"Breed Name {dogBreed.BreedName} already exists.\"");
                }

                if (dogBreed.BreedName == "" || dogBreed.BreedName == null)
                {
                    return UnprocessableEntity("\"Error\" : \"Please provide a 'breedName' property, and non-empty string value.\"");
                }

                bool emptySubBreedList = false;

                //Validate subbreed details
                ///If we have nothing in the payload for subbreeds, create an empty list for it.
                if (dogBreed.SubBreeds == null)
                {
                    dogBreed.SubBreeds = new List<DogSubBreed>();
                    emptySubBreedList = true;
                }

                if (!emptySubBreedList)
                {
                    if (dogBreed.SubBreeds.Count != 0)
                    {
                        foreach (var x in dogBreed.SubBreeds)
                        {
                            if (x.SubBreedName == "" || x.SubBreedName == null)
                            {
                                return UnprocessableEntity("\"Error\" : \"Please provide a 'subBreedName' property on each sub-breed, and non-empty string value.\"");
                            }
                        }
                    }
                }

                var hashSet1 = new HashSet<string>();
                if (!dogBreed.SubBreeds.All(subBreed => hashSet1.Add(subBreed.SubBreedName)))
                {
                    return UnprocessableEntity($"\"Error\" : \"Sub-breed name {hashSet1.Last()} is duplicated within the same breed. Please ensure sub-breed values are unique within the breed.\"");
                }

                //Save dog breed to context when we've verified it has a unique name
                dataContext.DogBreedItemList.Add(dogBreed);
                await dataContext.SaveChangesAsync();

                //If we've got sub-breeds
                if (dogBreed.SubBreeds.Count != 0)
                {
                    //Set subbreed parentId to the Id that got generated when the parent was saved to the database.
                    dogBreed = dataContext.DogBreedItemList.First(x => x.BreedName == dogBreed.BreedName);
                    dogBreed.SubBreeds.ForEach(x => x.ParentBreedId = dogBreed.DogBreedItemId);
                    await dataContext.SaveChangesAsync();
                }

                //Return object we successfully built
                return Created($"api/dogbreed/{dogBreed.DogBreedItemId}", dataContext.DogBreedItemList.Find(dogBreed.DogBreedItemId));
            }
            catch
            {
                dataContext.DogBreedItemList.Remove(dogBreed);
                await dataContext.SaveChangesAsync();
                return UnprocessableEntity("Could not process. Check your formatting for missing parenthesis and list seperators.");
            }
        }

        [HttpPost("{parentBreedId}/subbreed/")]
        public async Task<ActionResult<DogSubBreed>> PostSubBreed(long parentBreedId, long subBreedId, DogSubBreed dogSubBreed)
        {
            dogSubBreed.ParentBreedId = parentBreedId;
            DogBreedItem parentBreed;
            try
            {
                parentBreed = dataContext.DogBreedItemList.Include(breed => breed.SubBreeds).First(x => x.DogBreedItemId == parentBreedId);
            }
            catch
            {
                return UnprocessableEntity("\"Error\" : \"Parent breed does not exist.\"");
            }

            try
            {
                if (parentBreed.SubBreeds.Exists(x => x.DogSubBreedId == dogSubBreed.DogSubBreedId))
                {
                    return UnprocessableEntity("\"Error\" : \"A dog with that subbreed Id already exists.\"");
                }

                if (parentBreed.SubBreeds.Exists(x => x.SubBreedName == dogSubBreed.SubBreedName))
                {
                    return UnprocessableEntity("\"Error\" : \"A breed can not contain two sub-breeds with the same name. Same sub-breed names are fine within seperate breed objects.\"");
                }

                dataContext.DogSubBreedItemList.Add(dogSubBreed);
                await dataContext.SaveChangesAsync();

                DogSubBreed createdSubBreed = await dataContext.DogSubBreedItemList.Where(x => x.SubBreedName == dogSubBreed.SubBreedName).FirstAsync(x => x.ParentBreedId == parentBreedId);
                dataContext.DogBreedItemList.Find(parentBreed.DogBreedItemId).SubBreeds.Add(createdSubBreed);
                await dataContext.SaveChangesAsync();

                return Created($"api/dogbreed/{parentBreedId}/subbreed/{createdSubBreed.DogSubBreedId}", createdSubBreed);
            }            
            catch
            {
                return BadRequest();
            }
        }

        #endregion

        #region HTTPPut
        /// <summary>
        /// Updates an existing breed, where breed is the id.
        /// </summary>
        /// <param name="id">The id of the breed to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<DogBreedItem>> PutBreed(long id, DogBreedItem dogBreedItem)
        {
            if (id != dogBreedItem.DogBreedItemId)
            {
                return BadRequest();
            }
            dataContext.Entry(dogBreedItem).State = EntityState.Modified;
            await dataContext.SaveChangesAsync();
            return Ok();
        }
        #endregion
    }
}