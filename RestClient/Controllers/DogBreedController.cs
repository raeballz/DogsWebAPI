namespace DogRestAPI.Controllers
{
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
                dataContext.DogBreedItemList.Add(dogBreed);
                await dataContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return UnprocessableEntity(); //TODO: Check this is the correct errorcode to respond with.
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