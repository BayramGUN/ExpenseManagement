
using System.Text.Json;
using ExpenseManagement.Base.Constants.Caching;
using StackExchange.Redis;

namespace ExpenseManagement.Api.Caching;

public class CacheService : ICacheService
{
    private IDatabase cacheDataBase;

    public CacheService()
    {
        var redis = ConnectionMultiplexer.Connect(RedisConnectionKeys.RedisConnectionUrl);
        cacheDataBase = redis.GetDatabase();
    }

    public async Task<T> GetData<T>(string key)
    {
        var value = await cacheDataBase.StringGetAsync(key);
        return (!string.IsNullOrEmpty(value)) ? JsonSerializer.Deserialize<T>(value) : default;
    }

    public async Task<object> RemoveDataAsync<T>(string key)
    {
        var isExist = await cacheDataBase.KeyExistsAsync(key);
        return isExist? await cacheDataBase.KeyDeleteAsync(key) : false;
    }

    public async Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = await cacheDataBase.StringSetAsync(key, JsonSerializer.Serialize(value), expiryTime);
        return isSet;
    }
}