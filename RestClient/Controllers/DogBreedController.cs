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
        private readonly DogBreedContext controllerContext;

        public DogBreedController(DogBreedContext context)
        {
            this.controllerContext = context;

            if (this.controllerContext.DogBreedItemList.Count() == 0)
            {
                PopulateContextFromJson();
            }
        }

        private void PopulateContextFromJson()
        {
            ///TODO: If not populated, read in Json Payload
            ///But just create these objects for now.
            var hound0 = controllerContext.DogBreedItemList.Add( new DogBreedItem { Id = 1, BreedName = "Hound0", SubBreed = new List<DogSubBreed>()});
            var hound1 = controllerContext.DogBreedItemList.Add( new DogBreedItem { Id = 2, BreedName = "Hound1", SubBreed = new List<DogSubBreed>()});

            ///Push changes up so entity db can add a sub-breed to each DogBreed
            controllerContext.SaveChanges();

            controllerContext.DogBreedItemList.First(x => x.Id == 1).SubBreed.Add (new DogSubBreed { SubBreedName = "SubBreed0"});
            controllerContext.DogBreedItemList.First(x => x.Id == 1).SubBreed.Add (new DogSubBreed { SubBreedName = "SubBreed1" });

            controllerContext.DogBreedItemList.First(x => x.Id == 2).SubBreed.Add(new DogSubBreed { SubBreedName = "SubBreed2" });
            controllerContext.DogBreedItemList.First(x => x.Id == 2).SubBreed.Add(new DogSubBreed { SubBreedName = "SubBreed3" });

            controllerContext.SaveChanges();
        }

        public IEnumerable<DogBreedItem> GetAllBreeds()
        {
            ///still stubbed
            throw new NotImplementedException();
            return controllerContext.DogBreedItemList.ToList();
        }

        private void DeleteBreedByName()
        {
            throw new NotImplementedException();
        }

        private void DeleteBreedByID()
        {
            throw new NotImplementedException();
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