using CampusCuisine.Data;
using CampusCuisine.Model;
using Microsoft.AspNetCore.Mvc;

namespace CampusCuisine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingsController : ControllerBase
    {

        private readonly AppDbContext dbContext;

        public RatingsController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Rating>> GetRatingById([FromRoute] Guid id)
        {
            var ratingEntity = await dbContext.Ratings.FindAsync(id);

            if (ratingEntity is null)
            {
                return NotFound(new { Message = "Rating Id does not exist!" });
            }

            return new Rating
            {
                Id = ratingEntity.Id,
                RecipeId = ratingEntity.RecipeId,
                Value = ratingEntity.Value,
                Comment = ratingEntity.Comment
            };
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Rating>> PutRating([FromRoute] Guid id, [FromBody] Rating rating)
        {
            var ratingEntity = await dbContext.Ratings.FindAsync(id);

            if (ratingEntity is null)
            {
                return NotFound(new { Message = "Rating Id does not exist!" });
            }

            ratingEntity.Value = rating.Value ?? 0;
            ratingEntity.Comment = rating.Comment ?? "";

            await dbContext.SaveChangesAsync();

            return new Rating
            {
                Id = ratingEntity.Id,
                RecipeId = ratingEntity.RecipeId,
                Value = ratingEntity.Value,
                Comment = ratingEntity.Comment
            };
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Rating>> DeleteRating([FromRoute] Guid id)
        {
            var ratingEntity = await dbContext.Ratings.FindAsync(id);

            if (ratingEntity is null)
            {
                return NotFound(new { Message = "Rating Id does not exist!" });
            }

            dbContext.Ratings.Remove(ratingEntity);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}