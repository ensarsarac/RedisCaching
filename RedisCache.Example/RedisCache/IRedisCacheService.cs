namespace RedisCache.Example.RedisCache
{
	public interface IRedisCacheService<T> where T : class
	{
		Task<T> GetAsync<T>(string key);
		Task<bool> SetAsync<T>(string key, object value);
		Task Remove(string key);
		void RemoveAll(string key);
	}
}
