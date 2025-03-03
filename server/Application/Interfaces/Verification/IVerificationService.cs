using Application.Enums;

namespace Application.Interfaces.Verification
{
    public interface IVerificationService
    {
        Task<string> GenerateVerificationCodeAsync(Operation operation, string identifier);
        Task<bool> ConfirmVerificationCodeAsync(Operation operation, string identifier, string verificationCode);
    }
}
