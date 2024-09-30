using CampusCuisine.Model;
using CampusCuisine.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusCuisine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RatingsController : ControllerBase
    {

        private readonly RatingsService ratingsService;
        private readonly Mapper mapper;

        public RatingsController(RatingsService ratingsService, Mapper mapper)
        {
            this.ratingsService = ratingsService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Rating>> GetRatingById([FromRoute] Guid id)
        {
            var ratingEntity = await ratingsService.GetRatingById(id);

            return mapper.RatingEntityToRating(ratingEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Rating>> PutRating([FromRoute] Guid id, [FromBody] Rating rating)
        {
            var ratingEntity = await ratingsService.UpdateRating(id, rating);

            return mapper.RatingEntityToRating(ratingEntity);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Rating>> DeleteRating([FromRoute] Guid id)
        {
            await ratingsService.DeleteRating(id);

            return NoContent();
        }

    }
}