namespace Application.Interfaces.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email);
    }
}
