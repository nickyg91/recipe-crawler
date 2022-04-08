using StackExchange.Redis;
using System;

namespace RecipeCrawler.Data.Redis
{
    public class RedisConnection
    {
        private Lazy<ConnectionMultiplexer> Connection;
        private readonly string _connectionString;
        public IDatabaseAsync RedisInstance { get; set; }
        public RedisConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Connect()
        {
            if (Connection == null || !Connection.Value.IsConnected && !Connection.Value.IsConnecting)
            {
                Connection = new Lazy<ConnectionMultiplexer>(await ConnectionMultiplexer.ConnectAsync(_connectionString));
            }
        }
    }
}
