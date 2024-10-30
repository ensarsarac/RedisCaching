using StackExchange.Redis;

namespace RedisCache.Example.Services
{
	public static class RedisService
	{
		static ConfigurationOptions sentinelOptions => new()
		{
			EndPoints =
			{
				{"localhost", 6385 },
				{"localhost", 6386 },
				//sentinel sunucular
			},
			CommandMap = CommandMap.Sentinel,
			AbortOnConnectFail = false
		};

		static ConfigurationOptions masterOptions => new()
		{

			AbortOnConnectFail = false
		};

		public static async Task<IDatabase> RedisMasterDatabase()
		{
			ConnectionMultiplexer sentinelConnection = await ConnectionMultiplexer.SentinelConnectAsync(sentinelOptions);
			System.Net.EndPoint masterEndpoint = null;
			foreach (System.Net.EndPoint endpoint in sentinelConnection.GetEndPoints())
			{
				IServer server = sentinelConnection.GetServer(endpoint);

				if (!server.IsConnected)
					continue;
				masterEndpoint = await server.SentinelGetMasterAddressByNameAsync("mymaster");
				break;
			}

			var localMasterIP = masterEndpoint.ToString() switch
			{
				"172.18.0.8:6379" => "localhost:6379",
				"172.18.0.2:6379" => "localhost:6380",
				"172.18.0.7:6379" => "localhost:6381",
			};

			ConnectionMultiplexer masterConnection = await ConnectionMultiplexer.ConnectAsync(localMasterIP);
			return masterConnection.GetDatabase();

		}
	}
}
