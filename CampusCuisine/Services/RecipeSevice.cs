using CampusCuisine.Data;
using CampusCuisine.Entity;
using CampusCuisine.Errors;
using CampusCuisine.Model;
using Microsoft.EntityFrameworkCore;

namespace CampusCuisine.Services
{
    public class RecipeService
    {

        private readonly AppDbContext dbContext;

        public RecipeService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RecipeEntity> CreateRecipe(Recipe recipe)
        {
            var recipeEntity = new RecipeEntity(
                recipe.Id ?? Guid.NewGuid(),
                recipe.Name ?? "",
                recipe.Category ?? "",
                recipe.Ingredients ?? "",
                recipe.Instructions ?? ""
            );

            var existingEntity = await dbContext.Recipes.FindAsync(recipeEntity.Id);
            if (existingEntity is not null)
            {
                throw new BadDataException($"Recipe Id {recipeEntity.Id} does already exist!");
            }

            dbContext.Recipes.Add(recipeEntity);
            await dbContext.SaveChangesAsync();

            return recipeEntity;
        }

        public async Task<List<RecipeEntity>> GetAllRecipes()
        {
            return await dbContext.Recipes.ToListAsync();
        }

        public async Task<RecipeEntity> GetRecipeById(Guid id)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                throw new NotFoundException($"Recipe Id {id} does not exist!");
            }

            return recipeEntity;
        }

        public async Task<RecipeEntity> UpdateRecipe(Guid id, Recipe recipe)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                throw new NotFoundException($"Recipe Id {id} does not exist!");
            }

            recipeEntity.Name = recipe.Name ?? recipeEntity.Name;
            recipeEntity.Category = recipe.Category ?? recipeEntity.Category;
            recipeEntity.Ingredients = recipe.Ingredients ?? recipeEntity.Ingredients;
            recipeEntity.Instructions = recipe.Instructions ?? recipeEntity.Instructions;
            await dbContext.SaveChangesAsync();

            return recipeEntity;
        }

        public async Task DeleteRecipe(Guid id)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                throw new NotFoundException($"Recipe Id {id} does not exist!");
            }

            var ratingsEntityForRecipe = await dbContext.Ratings.Where(rating => rating.RecipeId == id).ToArrayAsync();

            foreach (var ratingEntity in ratingsEntityForRecipe)
            {
                dbContext.Ratings.Remove(ratingEntity);
            }

            dbContext.Recipes.Remove(recipeEntity);
            await dbContext.SaveChangesAsync();

            return;
        }

    }
}