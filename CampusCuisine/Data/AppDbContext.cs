using CampusCuisine.Entity;
using Microsoft.EntityFrameworkCore;

namespace CampusCuisine.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<RecipeEntity> Recipes { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }

    }

}