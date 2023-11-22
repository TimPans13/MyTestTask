using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.Models;
using MyTestTask.Services.Interfaces;

namespace MyTestTask.Controllers
{
    [Route("api/v1/contacts")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var token = await authService.Login(model);

            if (token != null)
            {
                return Ok(new { Token = token });
            }

            return BadRequest("Invalid login attempt");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var token = await authService.Register(model);

            if (token != null)
            {
                return Ok(new { Token = token });
            }

            return BadRequest("Registration failed");
        }

        [HttpPost]
        [Route("logout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Logout()
        {
            await authService.Logout();
            return Ok("Logout successful");
        }
    }

}
