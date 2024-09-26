using CampusCuisine.Entity;
using CampusCuisine.Model;

namespace CampusCuisine.Services
{
    public class Mapper
    {

        public Recipe RecipeEntityToRecipe(RecipeEntity recipeEntity)
        {
            return new Recipe
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Category = recipeEntity.Category,
                Ingredients = recipeEntity.Ingredients,
                Instructions = recipeEntity.Instructions
            };
        }

        public Rating RatingEntityToRating(RatingEntity ratingEntity)
        {
            return new Rating
            {
                Id = ratingEntity.Id,
                RecipeId = ratingEntity.RecipeId,
                Value = ratingEntity.Value,
                Comment = ratingEntity.Comment
            };
        }

    }
}