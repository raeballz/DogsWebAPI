using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestAPI.Test
{
    public class TestDataPopulator
    {
        //private void PopulateWithTestData()
        //{
        //    ///TODO: If not populated, read in Json Payload
        //    ///But just create these objects for now.
        //    List<DogBreedItem> dogBreeds = new List<DogBreedItem>
        //    {
        //        new DogBreedItem { DogBreedItemId = 1, BreedName = "Hound1" },
        //        new DogBreedItem { DogBreedItemId = 2, BreedName = "Hound2" }
        //    };

        //    dogBreeds.ForEach(breed => dataContext.DogBreedItemList.Add(breed));
        //    dataContext.SaveChanges();

        //    List<DogSubBreed> subBreeds = new List<DogSubBreed>
        //    {
        //        new DogSubBreed {  ParentBreedId = dataContext.DogBreedItemList.Find((long)1).DogBreedItemId, SubBreedName = "Hound1.1"},
        //        new DogSubBreed { ParentBreedId = dataContext.DogBreedItemList.Find((long)1).DogBreedItemId, SubBreedName = "Hound1.2" },
        //        new DogSubBreed { ParentBreedId = dataContext.DogBreedItemList.Find((long)2).DogBreedItemId, SubBreedName = "Hound2.1" },
        //        new DogSubBreed { ParentBreedId = dataContext.DogBreedItemList.Find((long)2).DogBreedItemId, SubBreedName = "Hound2.2" }
        //    };

        //    subBreeds.ForEach(subBreed => dataContext.DogSubBreedItemList.Add(subBreed));
        //    dataContext.SaveChanges();

        //    dataContext.DogBreedItemList.First(breed => breed.DogBreedItemId == 1).SubBreeds = new List<DogSubBreed>() { dataContext.DogSubBreedItemList.First(breed => breed.DogSubBreedId == 1), dataContext.DogSubBreedItemList.First(breed => breed.DogSubBreedId == 2) };
        //    dataContext.DogBreedItemList.First(breed => breed.DogBreedItemId == 2).SubBreeds = new List<DogSubBreed>() { dataContext.DogSubBreedItemList.First(breed => breed.DogSubBreedId == 3), dataContext.DogSubBreedItemList.First(breed => breed.DogSubBreedId == 4) };

        //    ///Push changes up so entity db can add a sub-breed to each DogBreed
        //    dataContext.SaveChanges();
        //}
    }
}
