using Application.Extensions;
using Application.Factories;
using Application.Interfaces;
using Application.Interfaces.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        private readonly ExceptionFactory _exceptionFactory;

        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService, ILogger<AuthService> logger, ExceptionFactory exceptionFactory)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _logger = logger;
            _exceptionFactory = exceptionFactory;
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


        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>New user in JSON format</returns>
        public async Task<string> RegisterUserAsync(string email, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(email);

            if (existingUser != null)
            {
                throw _exceptionFactory.UserAlreadyExists();
            }
            // TODO: set correct response error handling
            var user = new User
            {
                // RoleId = _userRepository.("Role").Id, 
                // TODO set user role id | rolesConstants
                Email = email,
                PasswordHash = _passwordHasher.HashPassword(null, password),
                CreatedAt = DateTime.UtcNow
            };

            _logger.LogInformation($"User created successfully. Email: {user.Email} Password: {user.PasswordHash} ");

            await _userRepository.AddAsync(user);
            _logger.LogInformation($"User saved in the database. Email: {user.Email} Password: {user.UserName} ");
            return $"{user.ToJsonString()}";
        }
    }
}
