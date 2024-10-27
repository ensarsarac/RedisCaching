namespace RedisCache.Example.RedisCache
{
	public interface IRedisCacheService
	{
		T GetData<T>(string key);
		bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
		object RemoveData(string key);
	}
}
