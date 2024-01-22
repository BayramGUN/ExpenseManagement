namespace ExpenseManagement.Api.Caching;

public interface ICacheService 
{
    Task<T> GetData<T>(string key);
    Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime);
    Task <object> RemoveDataAsync<T>(string key);
}