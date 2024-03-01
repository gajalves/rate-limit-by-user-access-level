using Microsoft.AspNetCore.Mvc;
using rate_limit_by_user_access_level.Abstractions;
using rate_limit_by_user_access_level.Dto;

namespace rate_limit_by_user_access_level.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserService _userService;

        public AuthController(ITokenGenerator tokenGenerator, IUserService userService)
        {
            _tokenGenerator = tokenGenerator;
            _userService = userService;
        }
                
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto model)
        {
            var user = _userService.GetUserByEmail(model.Email);
            if (user == null)
                return NotFound(new { Message = "User not found!" });

            if (user.Password != model.Password)
                return BadRequest(new { Message = "Wrong Password!" });

            var token = _tokenGenerator.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
