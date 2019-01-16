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
        private readonly DogBreedContext controllerContext;

        /// <summary>
        /// Ctor for DogBreedController. 
        /// Inits datacontext, and populates it if empty.
        /// </summary>
        /// <param name="context">Hand in data context from storage if available?</param>
        public DogBreedController(DogBreedContext context)
        {
            this.controllerContext = context;

            if (this.controllerContext.DogBreedItemList.Count() == 0)
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
            controllerContext.DogBreedItemList.Add(new DogBreedItem { Id = 1, BreedName = "Hound0", SubBreed = new List<DogSubBreed>() });
            controllerContext.DogBreedItemList.Add(new DogBreedItem { Id = 2, BreedName = "Hound1", SubBreed = new List<DogSubBreed>() });

            ///Push changes up so entity db can add a sub-breed to each DogBreed
            controllerContext.SaveChanges();

            ///Populate sub-breeds in current breed dataset.
            controllerContext.DogBreedItemList.First(x => x.Id == 1).SubBreed.Add(new DogSubBreed { SubBreedName = "SubBreed0" });
            controllerContext.DogBreedItemList.First(x => x.Id == 1).SubBreed.Add(new DogSubBreed { SubBreedName = "SubBreed1" });

            controllerContext.DogBreedItemList.First(x => x.Id == 2).SubBreed.Add(new DogSubBreed { SubBreedName = "SubBreed2" });
            controllerContext.DogBreedItemList.First(x => x.Id == 2).SubBreed.Add(new DogSubBreed { SubBreedName = "SubBreed3" });

            controllerContext.SaveChanges();
        }

        /// <summary>
        /// Method fetches all breeds currently stored within the context. 
        /// Performed async, due to it being a web call.
        /// </summary>
        /// <returns>List of all breeds within the context.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DogBreedItem>>> GetAllBreeds()
        {
            List<DogBreedItem> items = await controllerContext.DogBreedItemList.ToListAsync();
            
            if (items.Count == 0)
            {
                return NoContent();
            }

            return Ok(items);
        }

        /// <summary>
        /// Method searches for breed by name, then removes it
        /// </summary>
        /// <param name="name"></param>
        /// <returns>204 If Successful, 404 Not found If Unsuccesful</returns>
        [HttpDelete("{breedname}")]
        public async Task<ActionResult<DogBreedItem>> DeleteBreedByName(string breedName)
        {
            DogBreedItem breedToDelete = await controllerContext.DogBreedItemList.FirstAsync(x => x.BreedName == breedName);

            if (breedToDelete == null)
            {
                return NotFound();
            }
            else
            {
                controllerContext.DogBreedItemList.Remove(breedToDelete);
                await controllerContext.SaveChangesAsync();
                return Ok(breedToDelete);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DogBreedItem>> DeleteBreedByID(long id)
        {
            DogBreedItem breedToDelete = await controllerContext.DogBreedItemList.FirstAsync(x => x.Id == id);

            if (breedToDelete == null)
            {
                return NotFound();
            }
            else
            {
                controllerContext.DogBreedItemList.Remove(breedToDelete);
                await controllerContext.SaveChangesAsync();
                return Ok(breedToDelete);
            }
        }

        private void PutBreed()
        {
            throw new NotImplementedException();
        }

        private void PostBreed()
        {
            throw new NotImplementedException();
        }

        private void GetBreedByName()
        {
            throw new NotImplementedException();
        }

        private void GetBreedByID()
        {
            throw new NotImplementedException();
        }
    }
}