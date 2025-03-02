using Application.Dispatchers;
using Application.Enums;
using Application.Interfaces.Cache;
using Application.Interfaces.Verification;
using Application.Utils;
using Microsoft.Extensions.Logging;
using Action = Application.Enums.Action;

namespace Application.Services.Verification
{
    public class VerificationService : IVerificationService
    {
        private readonly IVerificationCache _verificationCache;
        private readonly ILogger<VerificationService> _logger;
        private readonly BaseDispatcher<VerificationDispatcher> _dispatcher;

        public VerificationService(
            IVerificationCache verificationCache,
            ILogger<VerificationService> logger,
            BaseDispatcher<VerificationDispatcher> dispatcher)
        {
            _verificationCache = verificationCache;
            _logger = logger;
            _dispatcher = dispatcher;
        }

        public async Task<string> GenerateVerificationCodeAsync(VerificationType verificationType, string identifier)
        {
            var verificationCode = VerificationCodeGenerator.GenerateCode();
            var cacheKey = GetCacheKey(verificationType, identifier);

            await _verificationCache.SaveAsync(cacheKey, verificationCode, TimeSpan.FromMinutes(15));
            _logger.LogInformation($"✅ Generated verification code for {verificationType} ({identifier}): {verificationCode}");

            var notificationParams = new Dictionary<string, object>
            {
                { "operation", Operation.RegistrationActivationLink },
                { "action", Action.SendByEmail },
                { "recipient", identifier },
                { "verificationCode", verificationCode }
            };

            await _dispatcher.DispatchAsync(notificationParams);

            return verificationCode;
        }

        public async Task<bool> ConfirmVerificationCodeAsync(VerificationType verificationType, string identifier, string verificationCode)
        {
            var cacheKey = GetCacheKey(verificationType, identifier);
            var storedCode = await _verificationCache.GetAsync(cacheKey);

            if (storedCode == null || storedCode != verificationCode)
            {
                _logger.LogWarning($"❌ Invalid verification code for {verificationType} ({identifier})");
                return false;
            }

            await _verificationCache.RemoveAsync(cacheKey);
            _logger.LogInformation($"✅ {verificationType} verified successfully for {identifier}.");

            return true;
        }

        private string GetCacheKey(VerificationType verificationType, string identifier)
        {
            return $"{verificationType}:{identifier}";
        }
    }
}
