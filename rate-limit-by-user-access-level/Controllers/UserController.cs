using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rate_limit_by_user_access_level.Abstractions;

namespace rate_limit_by_user_access_level.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAll());
        }
    }
}
