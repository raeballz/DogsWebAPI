namespace DogRestAPI.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Dogbreed contains a collection of subbreeds and an Id key 
    /// to guarantee no overlaps in the database.
    /// </summary>
    public class DogBreedItem
    {
        //Index of dogbreed in entity DB.
        public long DogBreedItemId { get; set; }

        //Name in json property
        public string BreedName { get; set; }

        //List of sub-breeds in dog breed
        public List<DogSubBreed> SubBreeds{ get; set; }
    }
}
