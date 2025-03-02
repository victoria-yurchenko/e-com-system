namespace Application.Interfaces.Cache
{
    public interface IVerificationCache
    {
        Task SaveAsync(string email, string code, TimeSpan expiration);
        Task<string?> GetAsync(string email);
        Task RemoveAsync(string email);
    }
}