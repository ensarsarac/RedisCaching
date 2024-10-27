using Microsoft.Extensions.Options;
using RedisCache.Example.Setting;
using StackExchange.Redis;

namespace RedisCache.Example.RedisCache
{
	public class RedisCacheService : IRedisCacheService
	{
		private readonly StackExchange.Redis.IDatabase _database;
		private readonly ConnectionMultiplexer _connectionMultiplexer;
		private readonly RedisCacheSetting _setting;

		public RedisCacheService(IOptions<RedisCacheSetting> options)
		{
			_setting = options.Value;
			_connectionMultiplexer = ConnectionMultiplexer.Connect(_setting.RedisCache);
			_database = _connectionMultiplexer.GetDatabase();

		}

		public T GetData<T>(string key)
		{
			throw new NotImplementedException();
		}

		public object RemoveData(string key)
		{
			throw new NotImplementedException();
		}

		public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
		{
			throw new NotImplementedException();
		}
	}
}
