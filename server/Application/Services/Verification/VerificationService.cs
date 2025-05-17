using Application.Enums;
using Application.Interfaces.Cache;
using Application.Interfaces.Verification;
using Application.Utils;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Application.Services.Verification.VerificationService.GenerateVerificationCodeAsync()");

            var verificationCode = VerificationCodeGenerator.GenerateCode();
            var cacheKey = GetCacheKey(operation, identifier);

            await _verificationCache.SaveAsync(cacheKey, verificationCode, TimeSpan.FromMinutes(15));
            _logger.LogInformation($"Generated verification code for {operation} ({identifier}): {verificationCode}");
            return verificationCode;
        }

        public async Task<bool> ConfirmVerificationCodeAsync(Operation operation, string identifier, string verificationCode)
        {
            _logger.LogInformation("Application.Services.Verification.VerificationService.ConfirmVerificationCodeAsync()");

            var cacheKey = GetCacheKey(operation, identifier);
            var storedCode = await _verificationCache.GetAsync(cacheKey);

            if (storedCode == verificationCode)
            {
                await _verificationCache.RemoveAsync(cacheKey);
                return true;
            }

            _logger.LogInformation($"{verificationCode} verified successfully for {identifier}.");
            return false;
        }

        private static string GetCacheKey(Operation operation, string identifier)
        {
            return $"{operation}:{identifier}";
        }
    }
}
