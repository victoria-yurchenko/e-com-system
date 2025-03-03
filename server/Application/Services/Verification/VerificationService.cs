using Application.Dispatchers;
using Application.Enums;
using Application.Interfaces.Cache;
using Application.Interfaces.Verification;
using Application.Utils;
using Microsoft.Extensions.Logging;
using Action = Application.Enums.Action;

namespace Application.Services.Verification
{
    public class VerificationService(
        IVerificationCache verificationCache,
        ILogger<VerificationService> logger) : IVerificationService
    {
        private readonly IVerificationCache _verificationCache = verificationCache;
        private readonly ILogger<VerificationService> _logger = logger;

        public async Task<string> GenerateVerificationCodeAsync(Operation operation, string identifier)
        {
            var verificationCode = VerificationCodeGenerator.GenerateCode();
            var cacheKey = GetCacheKey(operation, identifier);

            await _verificationCache.SaveAsync(cacheKey, verificationCode, TimeSpan.FromMinutes(15));
            // _logger.LogInformation($"✅ Generated verification code for {verificationType} ({identifier}): {verificationCode}");
            return verificationCode;
        }

        public async Task<bool> ConfirmVerificationCodeAsync(Operation operation, string identifier, string verificationCode)
        {
            var cacheKey = GetCacheKey(operation, identifier);
            var storedCode = await _verificationCache.GetAsync(cacheKey);

            if (storedCode == verificationCode)
            {
                await _verificationCache.RemoveAsync(cacheKey);
                return true;
                // _logger.LogWarning($"❌ Invalid verification code for {verificationType} ({identifier})");
            }

            // _logger.LogInformation($"✅ {verificationType} verified successfully for {identifier}.");
            return false;
        }

        private string GetCacheKey(Operation operation, string identifier)
        {
            return $"{operation}:{identifier}";
        }
    }
}
