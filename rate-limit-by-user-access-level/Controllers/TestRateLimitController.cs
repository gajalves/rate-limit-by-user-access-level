using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rate_limit_by_user_access_level.Abstractions;

namespace rate_limit_by_user_access_level.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRateLimitController : ControllerBase
    {
        private readonly ICountRequests _countRequests;

        public TestRateLimitController(ICountRequests countRequests)
        {
            _countRequests = countRequests;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok($"Request: {_countRequests.Count()}");
        }
    }
}
