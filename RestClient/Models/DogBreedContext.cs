﻿namespace DogRestAPI.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Controls the database context via Entity Framework
    /// </summary>
    public class DogBreedContext : DbContext
    {
        /// <summary>
        /// Ctor to set up DogBreed data-base
        /// </summary>
        /// <param name="options"></param>
        public DogBreedContext(DbContextOptions<DogBreedContext> options) : base(options)
        { 
        }

        /// <summary>
        /// Set of dog breeds in the database
        /// </summary>
        public DbSet<DogBreedItem> DogBreedItemList { get; set; }


        /// <summary>
        /// Set of dog breeds in the item list
        /// </summary>
        public DbSet<DogSubBreed> DogSubBreedItemList { get; set; }
    }
}
