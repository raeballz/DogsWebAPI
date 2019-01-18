namespace DogRestAPI.Helpers
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Keep json functionality seperate
    /// Static for easy referece
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Called on inital run of application. 
        /// Populates the application context via JSON file.
        /// Migrates data from human readable json to 
        /// a database structure in EntityFramework
        /// </summary>
        public static Dictionary<string, string[]> PopulateDictionaryFromJsonFile(string jsonFileName = "dogs.json")
        {
            using (StreamReader reader = new StreamReader(jsonFileName)) //Only have stream reader open whilst handling it's variables.
            {
                string json = reader.ReadToEnd();

                //Dynamic because the type coming from the dog.json file is undefined in code base. 
                dynamic jsonObject = JsonConvert.DeserializeObject(json);

                //Convert our dynamic into a dictionary for easier processing
                
                return PopulateDictionary(jsonObject);
            }
        }

        /// <summary>
        /// Populate a dictionary from our parsed json
        /// dynamic object to make data proecessing 
        /// easier.
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        private static  Dictionary<string, string[]> PopulateDictionary(dynamic jsonObject)
        {
            Dictionary<string, string[]> jsonDictionary = new Dictionary<string, string[]>();

            //Iterate through all dog breeds
            foreach (var dogBreedProperty in jsonObject)
            {
                //Value is array of sub breeds.
                if (dogBreedProperty.Value.Count != 0)
                {
                    string[] subBreedArray = new string[dogBreedProperty.Value.Count];
                    int counter = 0;

                    foreach (string x in dogBreedProperty.Value)
                    {
                        subBreedArray[counter] = x;
                        counter++;
                    }

                    jsonDictionary.TryAdd(dogBreedProperty.Name, subBreedArray);
                }
                else
                {
                    jsonDictionary.TryAdd(dogBreedProperty.Name, new string[0]);
                }
            }
            return jsonDictionary;
        }
    }
}
