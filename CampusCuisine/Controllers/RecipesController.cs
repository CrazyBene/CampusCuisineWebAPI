using CampusCuisine.Data;
using CampusCuisine.Entity;
using CampusCuisine.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusCuisine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {

        private readonly AppDbContext dbContext;

        public RecipesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRecipes()
        {
            return Ok(new
            {
                recipes = await dbContext.Recipes
                .Select(entity => new Recipe { Id = entity.Id, Name = entity.Name, Category = entity.Category, Ingredients = entity.Ingredients, Instructions = entity.Instructions })
                .ToArrayAsync()
            });
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipe)
        {
            var recipeEntity = new RecipeEntity(
                Guid.NewGuid(),
                recipe.Name ?? "",
                recipe.Category ?? "",
                recipe.Ingredients ?? "",
                recipe.Instructions ?? ""
            );

            var existingEntity = await dbContext.Recipes.FindAsync(recipeEntity.Id);
            if (existingEntity is not null)
            {
                return BadRequest(new { Message = "Recipe Id does already exist!" });
            }

            dbContext.Recipes.Add(recipeEntity);
            await dbContext.SaveChangesAsync();

            recipe.Id = recipeEntity.Id;

            return Created("", recipe);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById([FromRoute] Guid id)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            return new Recipe
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Category = recipeEntity.Category,
                Ingredients = recipeEntity.Ingredients,
                Instructions = recipeEntity.Instructions
            };
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> PutRecipe([FromRoute] Guid id, [FromBody] Recipe recipe)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            recipeEntity.Name = recipe.Name ?? "";
            recipeEntity.Category = recipe.Category ?? "";
            recipeEntity.Ingredients = recipe.Ingredients ?? "";
            recipeEntity.Instructions = recipe.Instructions ?? "";

            await dbContext.SaveChangesAsync();

            recipe.Id = id;

            return recipe;
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> PatchRecipe([FromRoute] Guid id, [FromBody] Recipe recipe)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            recipeEntity.Name = recipe.Name ?? recipeEntity.Name;
            recipeEntity.Category = recipe.Category ?? recipeEntity.Category;
            recipeEntity.Ingredients = recipe.Ingredients ?? recipeEntity.Ingredients;
            recipeEntity.Instructions = recipe.Instructions ?? recipeEntity.Instructions;

            await dbContext.SaveChangesAsync();

            return new Recipe
            {
                Id = recipeEntity.Id,
                Name = recipeEntity.Name,
                Category = recipeEntity.Category,
                Ingredients = recipeEntity.Ingredients,
                Instructions = recipeEntity.Instructions
            };
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> DeleteRecipe([FromRoute] Guid id)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            var ratingsEntityForRecipe = await dbContext.Ratings.Where(rating => rating.RecipeId == id).ToArrayAsync();

            foreach (var ratingEntity in ratingsEntityForRecipe)
            {
                dbContext.Ratings.Remove(ratingEntity);
            }

            dbContext.Recipes.Remove(recipeEntity);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/ratings")]
        public async Task<ActionResult<Rating>> CreateRating([FromRoute] Guid id, [FromBody] Rating rating)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            var ratingEntity = new RatingEntity(
                Guid.NewGuid(),
                id,
                rating.Value ?? 0,
                rating.Comment
            );

            dbContext.Ratings.Add(ratingEntity);
            await dbContext.SaveChangesAsync();

            rating.Id = ratingEntity.Id;
            rating.RecipeId = id;

            return rating;
        }

        [HttpGet]
        [Route("{id}/ratings")]
        public async Task<ActionResult> GetAllRatingsForRecipe([FromRoute] Guid id)
        {
            var recipeEntity = await dbContext.Recipes.FindAsync(id);

            if (recipeEntity is null)
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            return Ok(new
            {
                ratings = await dbContext.Ratings
                .Where(rating => rating.RecipeId == id)
                .Select(entity => new Rating { Id = entity.Id, RecipeId = entity.RecipeId, Value = entity.Value, Comment = entity.Comment })
                .ToArrayAsync()
            });
        }

    }
}