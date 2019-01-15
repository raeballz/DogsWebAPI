using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestAPI.Models
{
    /// <summary>
    /// First sweep at a possible model for JSON deserialisation
    /// </summary>
    public class DogBreedItem
    {
        //Index of dog in list? Might not be needed, unique ID is probably breedName
        public long Id { get; set; }

        //Name in json property
        public string BreedName { get; set; }

        //List of sub-breeds in dog breed
        public ICollection<DogSubBreed> SubBreed{ get; set; }
    }

    public class DogSubBreed
    {
        public long Id { get; set; }

        public string SubBreedName { get; set; }
    }
}
