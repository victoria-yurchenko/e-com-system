using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<string> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new Exception("Invalid credentials.");
            }

            return _jwtService.GenerateToken(user.Id.ToString(), user.Email);
        }

        public async Task<string> RegisterUserAsync(string email, string password)
        {
            _logger.LogInformation($"RegisterUserAsync starts... Email: {email} Password: {password}");

            var existingUser = await _userRepository.GetByEmailAsync(email);
            _logger.LogInformation($"existingUser == null: {existingUser == null}");

            
            if (existingUser != null)
            {
                _logger.LogError("Email is already in use.");
                throw new Exception("Email is already in use.");
            }

            var user = new User
            {
                Email = email,
                PasswordHash = _passwordHasher.HashPassword(null, password),
                CreatedAt = DateTime.UtcNow
            };

            _logger.LogInformation($"User created successfully. Email: {user.Email} Password: {user.PasswordHash} ");

            await _userRepository.AddAsync(user);
            return "User registered successfully.";
        }
    }
}
