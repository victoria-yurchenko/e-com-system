namespace Application.Interfaces.Authentication
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(string email, string password);
        Task<string> AuthenticateUserAsync(string email, string password);
    }
}
