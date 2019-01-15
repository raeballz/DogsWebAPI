using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestAPI.Models
{
    /// <summary>
    /// Controls the database context via Entity Framework
    /// </summary>
    public class DogBreedContext : DbContext
    {
        public DogBreedContext(DbContextOptions<DogBreedContext> options) : base(options)
        {
        }

        public DbSet<DogBreedItem> DogBreedItemList { get; set; }
    }
}
