using Application.Factories;
using Application.Interfaces.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Logging;
using Application.Interfaces.Repositories;
using Application.Services.Notifications;
using Application.Enums;
using Application.Interfaces.Cache;
using Application.Interfaces.Verification;

namespace Application.Services.Authentication
{
    public class AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IJwtService jwtService, ExceptionFactory exceptionFactory, NotificationService notificationService, IVerificationService verificationService, IVerificationCache verificationCache) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IJwtService _jwtService = jwtService;
        private readonly ExceptionFactory _exceptionFactory = exceptionFactory;
        private readonly NotificationService _notificationService = notificationService;
        private readonly IVerificationService _verificationService = verificationService;
        private readonly IVerificationCache _verificationCache = verificationCache;

        public async Task<string> AuthenticateUserAsync(string identifier, string password)
        {
            var user = await _userRepository.GetByEmailAsync(identifier) ?? throw new Exception("Invalid credentials.");
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new Exception("Invalid credentials.");
            }

            return GenerateJwt(user);
        }


        public async Task<string> SendAccountVerificationCodeAsync(string identifier)
        {
            await CheckUserExist(identifier);
            var verificationCode = await _verificationService.GenerateVerificationCodeAsync(Operation.Verification, identifier); ;
            // Save to Redis for 15 minutes
            await _verificationCache.SaveAsync(identifier, verificationCode, TimeSpan.FromMinutes(15));
            await SendVerificationCodeAsync(identifier, verificationCode);

            return $"Code was sent to {identifier}";
        }

        public async Task<bool> ConfirmVerificationCodeAsync(string identifier, string verificationCode)
        {
            var isCodeCorrect = await _verificationService.ConfirmVerificationCodeAsync(Operation.Verification, identifier, verificationCode);
            await _verificationCache.SaveAsync(identifier, Operation.VerificationCompleted.ToString(), TimeSpan.FromMinutes(15));
            return isCodeCorrect;
        }

        // Reterns JWT
        public async Task<string> RegisterUserAsync(string identifier, string password)
        {
            var isVerified = await _verificationCache.GetAsync(identifier) == Operation.VerificationCompleted.ToString();
            await CheckUserExist(identifier, [isVerified]);

            var user = await CreateAndSaveUser(identifier, password);
            await _verificationCache.RemoveAsync(identifier);

            return GenerateJwt(user);
        }

        private async Task<User> CreateAndSaveUser(string email, string password)
        {
            var user = CreateUser(email, password);
            await _userRepository.AddAsync(user);
            return user;
        }

        private async Task SendVerificationCodeAsync(string recipient, string verificationCode)
        {
            var notificationParams = GetDispatcherParameters(recipient, verificationCode);
            await _notificationService.SendNotificationAsync(notificationParams);
        }

        private async Task<bool> EnsureUserDoesNotExistAsync(string identifier)
        {
            return await _userRepository.GetByEmailAsync(identifier) != null;
        }

        private static Dictionary<string, object> GetDispatcherParameters(string identifier, string verificationCode)
        {
            return new Dictionary<string, object>
            {
                { "operation", Operation.Verification },
                { "deliveryMethod", DeliveryMethod.Email },
                { "recipient", identifier },
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

        private async Task CheckUserExist(string identifier, params bool[] parameters)
        {
            var userExists = await EnsureUserDoesNotExistAsync(identifier);
            if (!userExists && parameters.Contains(false))
            {
                throw _exceptionFactory.UserAlreadyExists();
            }
        }

        private string GenerateJwt(User user)
        {
            return _jwtService.GenerateToken(user.Id.ToString(), user.Email);
        }
    }
}
