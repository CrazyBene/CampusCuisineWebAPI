using CampusCuisine.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CampusCuisine.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }

    }

}