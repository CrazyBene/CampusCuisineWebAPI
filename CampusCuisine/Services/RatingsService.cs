using CampusCuisine.Data;
using CampusCuisine.Entity;
using CampusCuisine.Errors;
using CampusCuisine.Model;
using Microsoft.EntityFrameworkCore;

namespace CampusCuisine.Services
{
    public class RatingsService
    {

        private readonly AppDbContext dbContext;

        public RatingsService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RatingEntity> CreateRating(Guid recipeId, Rating rating)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(recipeId);

            if (recipeEntity is null)
            {
                throw new NotFoundException($"Recipe Id {recipeId} does not exist!");
            }

            var ratingEntity = new RatingEntity(
                rating.Id ?? Guid.NewGuid(),
                recipeId,
                rating.Value ?? 0,
                rating.Comment
            );

            var existingEntity = await dbContext.Ratings.FindAsync(ratingEntity.Id);
            if (existingEntity is not null)
            {
                throw new BadDataException($"Rating Id {ratingEntity.Id} does already exist!");
            }

            dbContext.Ratings.Add(ratingEntity);
            await dbContext.SaveChangesAsync();

            return ratingEntity;
        }

        public async Task<List<RatingEntity>> GetAllRatingsByRecipeId(Guid recipeId)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(recipeId);

            if (recipeEntity is null)
            {
                throw new NotFoundException($"Recipe Id {recipeId} does not exist!");
            }

            return await dbContext.Ratings.Where(rating => rating.RecipeId == recipeId).ToListAsync();
        }

        public async Task<RatingEntity> GetRatingById(Guid id)
        {
            var ratingEntity = await dbContext.Ratings.FindAsync(id);

            if (ratingEntity is null)
            {
                throw new NotFoundException($"Rating Id {id} does not exist!");
            }

            return ratingEntity;
        }

        public async Task<RatingEntity> UpdateRating(Guid id, Rating rating)
        {
            var ratingEntity = await dbContext.Ratings.FindAsync(id);

            if (ratingEntity is null)
            {
                throw new NotFoundException($"Rating Id {id} does not exist!");
            }

            ratingEntity.Value = rating.Value ?? 0;
            ratingEntity.Comment = rating.Comment ?? "";

            await dbContext.SaveChangesAsync();

            return ratingEntity;
        }

        public async Task DeleteRating(Guid id)
        {
            var ratingEntity = await dbContext.Ratings.FindAsync(id);

            if (ratingEntity is null)
            {
                throw new NotFoundException($"Rating Id {id} does not exist!");
            }

            dbContext.Ratings.Remove(ratingEntity);
            await dbContext.SaveChangesAsync();

            return;
        }

    }
}