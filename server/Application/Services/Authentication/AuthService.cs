using Application.Factories;
using Application.Interfaces.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using Application.Services.Notifications;
using Application.Enums;
using Application.Interfaces.Cache;
using Action = Application.Enums.Action;
using Application.Utils;

namespace Application.Services.Authentication
{
    public class AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtService jwtService,
        ILogger<AuthService> logger,
        ExceptionFactory exceptionFactory,
        NotificationService notificationService,
        IVerificationCache verificationCache) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IJwtService _jwtService = jwtService;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly ExceptionFactory _exceptionFactory = exceptionFactory;
        private readonly NotificationService _notificationService = notificationService;
        private readonly IVerificationCache _verificationCache = verificationCache;

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
        public async Task<string> RegisterUserAsync(string identifier, string password)
        {
            await EnsureUserDoesNotExistAsync(identifier);

            // var user = await CreateAndSaveUser(email, password);
            var verificationCode = VerificationCodeGenerator.GenerateCode();
            // Save to Redis for 15 minutes
            await _verificationCache.SaveAsync(identifier, verificationCode, TimeSpan.FromMinutes(15));
            // _logger.LogInformation($"User saved in the database. Email: {user.Email} Password: {user.UserName} ");
            await SendRegistrationLinkAsync(identifier, verificationCode);

            // return $"{identifier.ToJsonString()}";
            return "User registered successfully"; // TODO fix this line
        }

        public async Task<string> ConfirmVerificationCodeAsync(VerificationType verificationType, string identifier, string verificationCode)
        {
            var cachedVerificationCode = await _verificationCache.GetAsync<string>(identifier);

        private async Task<User> CreateAndSaveUser(string email, string password)
        {
            var user = CreateUser(email, password);
            await _userRepository.AddAsync(user);
            return user;
        }

        private async Task SendRegistrationLinkAsync(string recipient, string verificationCode)
        {
            var notificationParams = GetNotificationParams(recipient, verificationCode);
            await _notificationService.SendNotificationAsync(notificationParams);
        }

        private async Task EnsureUserDoesNotExistAsync(string identifier)
        {
            var existingUser = await _userRepository.GetByEmailAsync(identifier);

            if (existingUser != null)
            {
                throw _exceptionFactory.UserAlreadyExists();
            }
            // TODO: set correct response error handling
        }

        private static Dictionary<string, object> GetNotificationParams(string recipient, string verificationCode)
        {
            return new Dictionary<string, object>()
            {
                { "operation", Operation.RegistrationActivationLink },
                { "action", Action.SendByEmail },
                { "recipient", recipient },
                { "verificationCode", verificationCode }
            };
        }

        private User CreateUser(string email, string password)
        {
            var user = new User
            {
                // RoleId = _userRepository.("Role").Id, 
                // TODO set user role id | rolesConstants
                Email = email,
                PasswordHash = _passwordHasher.HashPassword(null, password),
                CreatedAt = DateTime.UtcNow
            };
            return user;
        }
    }
}
