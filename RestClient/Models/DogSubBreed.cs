namespace DogRestAPI.Models
{
    /// <summary>
    /// Sub Breed Class to nest within Breed.
    /// </summary>
    public class DogSubBreed
    {
        //Reference ID to Parent Breed
        public long ParentBreedId { get; set; }

        //Id for primary key
        public long DogSubBreedId { get; set; }

        //Name for sub-breed
        public string SubBreedName { get; set; }
    }
}
