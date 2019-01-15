using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogRestAPI.Controllers
{
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
                ///TODO: If not populated, read in Json Payload
            }
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

        private void GetAllBreeds()
        {
            throw new NotImplementedException();
        }
    }
}