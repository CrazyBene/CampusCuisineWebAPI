using System.Net;
using CampusCuisine.Data;
using CampusCuisine.Entity;
using CampusCuisine.Errors;
using CampusCuisine.Model;
using CampusCuisine.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusCuisine.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            try
            {
                var recipeEntity = await recipeService.CreateRecipe(recipe);

                return Created("", mapper.RecipeEntityToRecipe(recipeEntity));
            }
            catch (BadDataException e)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: e.ErrorMessage
                );
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById([FromRoute] Guid id)
        {
            try
            {
                var recipeEntity = await recipeService.GetRecipeById(id);

                return mapper.RecipeEntityToRecipe(recipeEntity);
            }
            catch (NotFoundException e)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    detail: e.ErrorMessage
                );
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> PutRecipe([FromRoute] Guid id, [FromBody] Recipe recipe)
        {
            try
            {
                var recipeEntity = await recipeService.UpdateRecipe(id, recipe);

                return mapper.RecipeEntityToRecipe(recipeEntity);
            }
            catch (NotFoundException e)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    detail: e.ErrorMessage
                );
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> PatchRecipe([FromRoute] Guid id, [FromBody] Recipe recipe)
        {
            try
            {
                var recipeEntity = await recipeService.UpdateRecipe(id, recipe);

                return mapper.RecipeEntityToRecipe(recipeEntity);
            }
            catch (NotFoundException e)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    detail: e.ErrorMessage
                );
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteRecipe([FromRoute] Guid id)
        {
            try
            {
                await recipeService.DeleteRecipe(id);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    detail: e.ErrorMessage
                );
            }
        }

        [HttpPost]
        [Route("{id}/ratings")]
        public async Task<ActionResult<Rating>> CreateRating([FromRoute] Guid id, [FromBody] Rating rating)
        {
            try
            {
                var ratingEntity = await ratingsService.CreateRating(id, rating);

                return mapper.RatingEntityToRating(ratingEntity);
            }
            catch (BadDataException e)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: e.ErrorMessage
                );
            }
            catch (NotFoundException e)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    detail: e.ErrorMessage
                );
            }
        }

        [HttpGet]
        [Route("{id}/ratings")]
        public async Task<ActionResult> GetAllRatingsForRecipe([FromRoute] Guid id)
        {
            try
            {
                var ratingEntities = await ratingsService.GetAllRatingsByRecipeId(id);

                return Ok(new
                {
                    ratings = ratingEntities.Select(entity => mapper.RatingEntityToRating(entity))
                });
            }
            catch (NotFoundException e)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    detail: e.ErrorMessage
                );
            }
        }

    }
}