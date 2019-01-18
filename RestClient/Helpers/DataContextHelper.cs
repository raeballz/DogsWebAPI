using DogRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestAPI.Helpers
{
    public static class DataContextHelper
    {
        /// <summary>
        /// Creates all sub-breeds and saves them in the context handed to it.
        /// </summary>
        /// <param name="dogSubbreedArray"></param>
        /// <param name="parentBreedId"></param>
        public static void PopulateDbContextWithNewDogBreedSubbreeds(string[] dogSubbreedArray, long parentBreedId, DogBreedContext dataContext)
        {
            DogBreedItem parentBreed = dataContext.DogBreedItemList.Find(parentBreedId);

            foreach (string subBreedName in dogSubbreedArray)
            {
                DogSubBreed subBreed = new DogSubBreed() { ParentBreedId = parentBreedId, SubBreedName = subBreedName };
                dataContext.DogSubBreedItemList.Add(subBreed);
            }

            //Now save DB context so that we can retreive them with a generated ID.
            dataContext.SaveChanges();

            //Build a list of all sub breeds where the sub-subbreed's parent ID is the parent Id we're looking for.
            List<DogSubBreed> subBreeds = dataContext.DogSubBreedItemList.Where(x => x.ParentBreedId == parentBreedId).ToList();

            //set it in the context using our reference
            dataContext.DogBreedItemList.Find(parentBreedId).SubBreeds = subBreeds;
            dataContext.SaveChanges();
        }

        /// <summary>
        /// Adds the breed to the context, without sub breeds. 
        /// </summary>
        /// <param name="breedName"></param>
        /// <param name="dataContext"></param>
        public static void PopulateDbContextWithNewDogBreed(string breedName, DogBreedContext dataContext)
        {
            DogBreedItem dogBreed = new DogBreedItem();
            dogBreed.BreedName = breedName;

            dataContext.DogBreedItemList.Add(dogBreed);
            dataContext.SaveChanges();
        }
    }
}
