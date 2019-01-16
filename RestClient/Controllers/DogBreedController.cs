namespace DogRestAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
                PopulateContextFromJson();
            }
        }

        /// <summary>
        /// Called on inital run of application. 
        /// Populates the application context via JSON file.
        /// </summary>
        private void PopulateContextFromJson()
        {
            ///TODO: If not populated, read in Json Payload
            ///But just create these objects for now.
            List<DogBreedItem> dogBreeds = new List<DogBreedItem>
            {
                new DogBreedItem { DogBreedItemId = 1, BreedName = "Hound1" },
                new DogBreedItem { DogBreedItemId = 2, BreedName = "Hound2" }
            };

            dogBreeds.ForEach(breed => dataContext.DogBreedItemList.Add(breed));
            dataContext.SaveChanges();

            List<DogSubBreed> subBreeds = new List<DogSubBreed>
            {
                new DogSubBreed {  ParentBreedId = dataContext.DogBreedItemList.Find((long)1).DogBreedItemId, SubBreedName = "Hound1.1"},
                new DogSubBreed { ParentBreedId = dataContext.DogBreedItemList.Find((long)1).DogBreedItemId, SubBreedName = "Hound1.2" },
                new DogSubBreed { ParentBreedId = dataContext.DogBreedItemList.Find((long)2).DogBreedItemId, SubBreedName = "Hound2.1" },
                new DogSubBreed { ParentBreedId = dataContext.DogBreedItemList.Find((long)2).DogBreedItemId, SubBreedName = "Hound2.2" }
            };

            subBreeds.ForEach(subBreed => dataContext.DogSubBreedItemList.Add(subBreed));
            dataContext.SaveChanges();

            dataContext.DogBreedItemList.First(x => x.DogBreedItemId == 1).SubBreeds = new List<DogSubBreed>() { dataContext.DogSubBreedItemList.First(x => x.DogSubBreedId == 1), dataContext.DogSubBreedItemList.First(x => x.DogSubBreedId == 2)};
            dataContext.DogBreedItemList.First(x => x.DogBreedItemId == 2).SubBreeds = new List<DogSubBreed>() { dataContext.DogSubBreedItemList.First(x => x.DogSubBreedId == 3), dataContext.DogSubBreedItemList.First(x => x.DogSubBreedId == 4)};

            ///Push changes up so entity db can add a sub-breed to each DogBreed
            dataContext.SaveChanges();
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
            IEnumerable<DogBreedItem> items = dataContext.DogBreedItemList.Include(n => n.SubBreeds).AsEnumerable();
            
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
                requestedBreed = await dataContext.DogBreedItemList.FirstAsync(x => x.DogBreedItemId == id);
                return Ok(requestedBreed);
            }
            catch
            {
                return NotFound();
            }
        }

        //[HttpGet("{breedname}")]
        //public async Task<ActionResult<DogBreedItem>> GetBreedByName(string breedName)
        //{
        //    DogBreedItem requestedBreed = await controllerContext.DogBreedItemList.FirstAsync(x => x.BreedName == breedName);

        //    if (requestedBreed == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(requestedBreed);
        //    }
        //}
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
            try
            {
                breedToDelete = await dataContext.DogBreedItemList.FirstAsync(x => x.DogBreedItemId == id);
                dataContext.DogBreedItemList.Remove(breedToDelete);
                await dataContext.SaveChangesAsync();
                return Ok(breedToDelete);
            }
            catch
            {
                return NotFound();
            }
        }

        ///// <summary>
        ///// Method searches for breed by name, then removes it
        ///// </summary>
        ///// <param name="breedName">Name of the breed we wish to delete</param>
        ///// <returns>204 If Successful, 404 Not found If Unsuccesful</returns>
        //[HttpDelete("{breedname}")]
        //public async Task<ActionResult<DogBreedItem>> DeleteBreedByName(string breedName)
        //{
        //    DogBreedItem breedToDelete;
        //    try
        //    {
        //        breedToDelete = await controllerContext.DogBreedItemList.FirstAsync(x => x.BreedName == breedName);
        //        controllerContext.DogBreedItemList.Remove(breedToDelete);
        //        await controllerContext.SaveChangesAsync();
        //        return Ok(breedToDelete);
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //}
        #endregion

        private void PutBreed()
        {
            throw new NotImplementedException();
        }

        private void PostBreed()
        {
            throw new NotImplementedException();
        }
    }
}