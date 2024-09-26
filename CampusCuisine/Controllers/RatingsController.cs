using CampusCuisine.Data;
using CampusCuisine.Errors;
using CampusCuisine.Model;
using CampusCuisine.Services;
using Microsoft.AspNetCore.Mvc;

namespace CampusCuisine.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            try
            {
                var ratingEntity = await ratingsService.GetRatingById(id);

                return mapper.RatingEntityToRating(ratingEntity);
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
        public async Task<ActionResult<Rating>> PutRating([FromRoute] Guid id, [FromBody] Rating rating)
        {
            try
            {
                var ratingEntity = await ratingsService.UpdateRating(id, rating);

                return mapper.RatingEntityToRating(ratingEntity);
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
        public async Task<ActionResult<Rating>> DeleteRating([FromRoute] Guid id)
        {
            try
            {
                await ratingsService.DeleteRating(id);

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

    }
}