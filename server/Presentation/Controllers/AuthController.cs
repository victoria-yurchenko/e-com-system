using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            await _authService.RegisterUserAsync(registerDto.Email, registerDto.Password);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            var token = _authService.AuthenticateUserAsync(loginUserDto.Email, loginUserDto.Password);
            return Ok(new { Token = token });
        }
    }
}
