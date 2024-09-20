using Microsoft.AspNetCore.Mvc;

namespace CampusCuisine.Controllers
{
    [ApiController]
    [Route("")]
    public class StatusController : ControllerBase
    {

        [HttpGet]
        public ActionResult Status()
        {
            return Ok(new { Status = "Server is online." });
        }

    }
}