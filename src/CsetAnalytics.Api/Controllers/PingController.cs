using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace CsetAnalytics.Api.Controllers
{
    [ApiController]
    [Route("api/ping")]
    public class PingController : Controller
    {
        [Route("GetPing")]
        [HttpGet]
        [EnableCors]
        public IActionResult GetPing()
        {
            return Ok();
        }
    }
}
