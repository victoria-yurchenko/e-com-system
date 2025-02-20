using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IAuthService authService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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
