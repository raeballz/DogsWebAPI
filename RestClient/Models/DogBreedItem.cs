using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestAPI.Models
{
    /// <summary>
    /// Dogbreed contains a collection of subbreeds and an Id key 
    /// to guarantee no overlaps in the database.
    /// </summary>
    public class DogBreedItem
    {
        //Index of dogbreed in entity DB.
        public long Id { get; set; }

        //Name in json property
        public string BreedName { get; set; }

        //List of sub-breeds in dog breed
        public ICollection<DogSubBreed> SubBreed{ get; set; }
    }

    /// <summary>
    /// Sub Breed Class to nest within Breed.
    /// </summary>
    public class DogSubBreed
    {
        //Id for primary key
        public long Id { get; set; }

        //Name for sub-breed
        public string SubBreedName { get; set; }
    }
}
