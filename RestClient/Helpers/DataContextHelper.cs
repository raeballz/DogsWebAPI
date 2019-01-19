namespace DogRestAPI.Helpers
{
    using DogRestAPI.Models;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Used to help manage the adding of new items to the database
    /// </summary>
    public static class DataContextHelper
    {
        /// <summary>
        /// Adds the breed to the context, without sub breeds. 
        /// </summary>
        /// <param name="breedName">Name of breed we are adding.</param>
        /// <param name="dataContext">Data context we are adding the breed to</param>
        public static void PopulateDbContextWithNewDogBreed(string breedName, DogBreedContext dataContext)
        {
            DogBreedItem dogBreed = new DogBreedItem
            {
                BreedName = breedName
            };

            dataContext.DogBreedItemList.Add(dogBreed);
            dataContext.SaveChanges();
        }

        /// <summary>
        /// Creates all sub-breeds and saves them in the context handed to it.
        /// </summary>
        /// <param name="dogSubbreedArray"></param>
        /// <param name="parentBreedId">Id of the breed this sub breed belongs to</param>
        /// <param name="dataContext">Data context we are adding the dog to</param>
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
    }
}
