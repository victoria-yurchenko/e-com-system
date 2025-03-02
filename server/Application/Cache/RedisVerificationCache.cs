using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.Cache;

namespace Application.Cache
{
    public class RedisVerificationCache : IVerificationCache
{
    private readonly IDatabase _database;

    public RedisVerificationCache(IConfiguration configuration)
    {
        var redis = ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"] ?? "");
        _database = redis.GetDatabase();
    }

    public async Task SaveAsync(string email, string code, TimeSpan expiration)
    {
        await _database.StringSetAsync(email, code, expiration);
    }

    public async Task<string?> GetAsync(string email)
    {
        return await _database.StringGetAsync(email);
    }

    public async Task RemoveAsync(string email)
    {
        await _database.KeyDeleteAsync(email);
    }
}

}