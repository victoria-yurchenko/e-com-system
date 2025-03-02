using Application.Enums;

namespace Application.Interfaces.Verification
{
    public interface IVerificationService
    {
        Task<string> GenerateVerificationCodeAsync(VerificationType verificationType, string identifier);
        Task<bool> ConfirmVerificationCodeAsync(VerificationType verificationType, string identifier, string verificationCode);
    }
}
