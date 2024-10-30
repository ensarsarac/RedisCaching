using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RedisCache.Example.Setting;
using StackExchange.Redis;

namespace RedisCache.Example.RedisCache
{
	public class RedisCacheService<T> : IRedisCacheService<T> where T : class
	{
		private readonly StackExchange.Redis.IDatabase _database;
		private readonly ConnectionMultiplexer _connectionMultiplexer;
		private readonly RedisCacheSetting _setting;

		public RedisCacheService(IOptions<RedisCacheSetting> options)
		{
			_setting = options.Value;
			_connectionMultiplexer = ConnectionMultiplexer.Connect(_setting.ConnectionString);
			_database = _connectionMultiplexer.GetDatabase();

		}

		public async Task<T> GetAsync<T>(string key)
		{
			var value = await  _database.StringGetAsync(key);
			if (!string.IsNullOrEmpty(value))
			{
				return JsonConvert.DeserializeObject<T>(value);
			}
			return default;
		}

		public async Task<bool> SetAsync<T>(string key, object value)
		{
			return await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), TimeSpan.FromMinutes(5));
		}

		public void RemoveAll(string key)
		{
			var redisEndpoints = _connectionMultiplexer.GetEndPoints(true);
			foreach (var redisEndpoint in redisEndpoints)
			{
				var redisServer = _connectionMultiplexer.GetServer(redisEndpoint);
				redisServer.FlushAllDatabases();
			}
		}

		public async Task Remove(string key)
		{
			await _database.KeyDeleteAsync(key);
		}

		

	}
}
