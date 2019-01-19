namespace DogRestAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

                int breedId = 1;
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
            IEnumerable<DogBreedItem> items = dataContext.DogBreedItemList.Include(breed => breed.SubBreeds).AsEnumerable();
            
            if (items.Count() == 0)
            {
                return NoContent();
            }

            return Ok(items);
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
                if (dataContext.DogBreedItemList.Any( x => x.BreedName == dogBreed.BreedName))
                {                    
                    return UnprocessableEntity($"\"Error\" :: \"Breed Name {dogBreed.BreedName} already exists.\"");
                }

                if (dogBreed.BreedName == "" || dogBreed.BreedName == null)
                {
                    return UnprocessableEntity("\"Error\" : \"Please provide a 'breedName' property, and non-empty string value.\"");
                }            

                //Validate subbreed details
                ///If we have nothing in the payload for subbreeds, create an empty list for it.
                if (dogBreed.SubBreeds == null)
                {
                    dogBreed.SubBreeds = new List<DogSubBreed>();
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
                    dogBreed = dataContext.DogBreedItemList.First( x => x.BreedName == dogBreed.BreedName);                    
                    dogBreed.SubBreeds.ForEach(x => x.ParentBreedId = dogBreed.DogBreedItemId);
                    await dataContext.SaveChangesAsync();
                }

                //Return object we successfully built
                return Ok(dataContext.DogBreedItemList.Find(dogBreed.DogBreedItemId));
            }
            catch
            {
                dataContext.DogBreedItemList.Remove(dogBreed);
                await dataContext.SaveChangesAsync();
                return UnprocessableEntity("Could not process. Check your formatting for missing parenthesis and list seperators.");
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