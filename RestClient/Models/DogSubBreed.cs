namespace DogRestAPI.Models
{
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
