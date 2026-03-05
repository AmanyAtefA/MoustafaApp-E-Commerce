using System.Text.Json;
using StackExchange.Redis;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(RedisConnection redisConnection)
    {
        _database = redisConnection.Database;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);

        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value);

        if (expiry.HasValue)
        {
            await _database.StringSetAsync(
                key,
                json,
                expiry.Value,
                when: When.Always,
                flags: CommandFlags.None
            );
        }
        else
        {
            await _database.StringSetAsync(key, json);
        }
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}