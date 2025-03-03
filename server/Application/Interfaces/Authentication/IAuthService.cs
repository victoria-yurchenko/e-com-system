namespace Application.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(string identifier, string password);
        Task<string> AuthenticateUserAsync(string identifier, string password);
        Task<bool> ConfirmVerificationCodeAsync(string identifier, string verificationCode);
        Task<string> VerifyAccountAsync(string identifier);
    }
}
