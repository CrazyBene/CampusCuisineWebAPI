using CampusCuisine.Model;
using CampusCuisine.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusCuisine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RecipesController : ControllerBase
    {

        private readonly RecipeService recipeService;
        private readonly RatingsService ratingsService;
        private readonly Mapper mapper;

        public RecipesController(RecipeService recipeService, RatingsService ratingsService, Mapper mapper)
        {
            this.recipeService = recipeService;
            this.ratingsService = ratingsService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllRecipes()
        {
            var recipeEntities = await recipeService.GetAllRecipes();

            return Ok(new
            {
                recipes = recipeEntities.Select(entity => mapper.RecipeEntityToRecipe(entity))
            });
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipe)
        {
            var recipeEntity = await recipeService.CreateRecipe(recipe);

            return Created("", mapper.RecipeEntityToRecipe(recipeEntity));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById([FromRoute] Guid id)
        {
            var recipeEntity = await recipeService.GetRecipeById(id);

            return mapper.RecipeEntityToRecipe(recipeEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> PutRecipe([FromRoute] Guid id, [FromBody] Recipe recipe)
        {
            var recipeEntity = await recipeService.UpdateRecipe(id, recipe);

            return mapper.RecipeEntityToRecipe(recipeEntity);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> PatchRecipe([FromRoute] Guid id, [FromBody] Recipe recipe)
        {
            var recipeEntity = await recipeService.UpdateRecipe(id, recipe);

            return mapper.RecipeEntityToRecipe(recipeEntity);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteRecipe([FromRoute] Guid id)
        {
            await recipeService.DeleteRecipe(id);

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/ratings")]
        public async Task<ActionResult<Rating>> CreateRating([FromRoute] Guid id, [FromBody] Rating rating)
        {
            var ratingEntity = await ratingsService.CreateRating(id, rating);

            return mapper.RatingEntityToRating(ratingEntity);
        }

        [HttpGet]
        [Route("{id}/ratings")]
        public async Task<ActionResult> GetAllRatingsForRecipe([FromRoute] Guid id)
        {
            var ratingEntities = await ratingsService.GetAllRatingsByRecipeId(id);

            return Ok(new
            {
                ratings = ratingEntities.Select(entity => mapper.RatingEntityToRating(entity))
            });
        }

    }
}