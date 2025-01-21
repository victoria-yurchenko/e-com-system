using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Presentation.Services.Interfaces;

namespace Presentation.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<string> AuthenticateUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterUserAsync(string email, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                throw new Exception("Email is already in use.");
            }

            // Создание нового пользователя
            var user = new User
            {
                Email = email,
                PasswordHash = _passwordHasher.HashPassword(null, password),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return "User registered successfully.";
        }
    }
}
