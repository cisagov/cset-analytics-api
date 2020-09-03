using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace CsetAnalytics.Api.Controllers
{
    [ApiController]
    [Route("api/ping")]
    [EnableCors("CorsApi")]
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
