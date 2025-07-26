using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Travellin.Api.Controllers
{
    [Authorize(Roles ="Guest")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("Msg")]
        public IActionResult SayHello()
        {
            return Ok("Hello");
        }
    }
}
