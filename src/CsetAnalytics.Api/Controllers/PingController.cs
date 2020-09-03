using Microsoft.AspNetCore.Mvc;

namespace CsetAnalytics.Api.Controllers
{
    [ApiController]
    [Route("api/ping")]
    public class PingController : Controller
    {
        [Route("GetPing")]
        [HttpGet]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
