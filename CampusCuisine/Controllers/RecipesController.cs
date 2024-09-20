using CampusCuisine.Model;
using Microsoft.AspNetCore.Mvc;

namespace CampusCuisine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {

        private static readonly Dictionary<int, Recipe> recipes = [];
        private static readonly Dictionary<int, Dictionary<int, Rating>> ratings = [];

        [HttpGet]
        public ActionResult GetAllRecipes()
        {
            return Ok(new { recipes = recipes.Values.ToList() });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Recipe> CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipes.ContainsKey(recipe.Id))
            {
                return BadRequest(new { Message = "Recipe Id already exists!" });
            }

            recipes.Add(recipe.Id, recipe);
            ratings.Add(recipe.Id, []);

            return recipe;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Recipe> GetRecipeById([FromRoute] int id)
        {
            if (!recipes.ContainsKey(id))
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            return recipes[id];
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Recipe> PutRecipe([FromRoute] int id, [FromBody] Recipe recipe)
        {
            if (!recipes.ContainsKey(id))
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            recipe.Id = id;
            recipes[recipe.Id] = recipe;

            return recipes[recipe.Id];
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<Recipe> PatchRecipe([FromRoute] int id, [FromBody] Recipe recipe)
        {
            if (!recipes.ContainsKey(id))
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            recipes[recipe.Id].Category = recipe.Category;

            return recipes[recipe.Id];
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Recipe> DeleteRecipe([FromRoute] int id)
        {
            if (!recipes.ContainsKey(id))
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            recipes.Remove(id);
            ratings.Remove(id);

            return NoContent();
        }

        [HttpPost]
        [Route("{id}/ratings")]
        public ActionResult<Rating> CreateRating([FromRoute] int id, [FromBody] Rating rating)
        {
            if (!recipes.ContainsKey(id))
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }
            if (id != rating.RecipeId)
            {
                return BadRequest(new { Message = "Id and Recipe Id do not match!" });
            }
            if (ratings[id].ContainsKey(rating.Id))
            {
                return BadRequest();
            }

            ratings[id].Add(rating.Id, rating);

            return rating;
        }

        [HttpGet]
        [Route("{id}/ratings")]
        public ActionResult GetAllRatingsForRecipe([FromRoute] int id)
        {
            if (!recipes.ContainsKey(id))
            {
                return NotFound(new { Message = "Recipe Id does not exist!" });
            }

            return Ok(new { Ratings = ratings[id] });
        }

    }
}