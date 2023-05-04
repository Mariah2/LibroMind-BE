using LibroMind_BE.Services.Interfaces;
using LibroMind_BE.Services.Models.Post;
using Microsoft.AspNetCore.Mvc;

namespace LibroMind_BE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginPostDTO loginPost)
        {
            return Ok(await _authenticationService.LoginAsync(loginPost));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterPostDTO registerPost)
        {
            await _authenticationService.RegisterAsync(registerPost);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
