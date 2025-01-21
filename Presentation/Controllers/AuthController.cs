using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already in use.");
            }

            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(null, registerDto.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(string userId, string email)
        {
            var token = _jwtService.GenerateToken(userId, email);
            return Ok(new { Token = token });
        }
    }
}
