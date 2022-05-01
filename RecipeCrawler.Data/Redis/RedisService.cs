using StackExchange.Redis;
using System.Text.Json;

namespace RecipeCrawler.Data.Redis
{
    public class RedisService : IRedisService
    {
        private Lazy<ConnectionMultiplexer> Connection;
        private readonly string _connectionString;
        public IDatabaseAsync Database { get; set; }
        public RedisService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Connect()
        {
            if (Connection == null || !Connection.Value.IsConnected && !Connection.Value.IsConnecting)
            {
                Connection = new Lazy<ConnectionMultiplexer>(ConnectionMultiplexer.Connect(_connectionString));
                Database = Connection.Value.GetDatabase();
            }
        }

        public async Task StoreKey<T>(string key, T value) where T : class
        {
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                await JsonSerializer.SerializeAsync(stream, value);
                stream.Seek(0, SeekOrigin.Begin);
                var json = await reader.ReadToEndAsync();
                await Database.StringSetAsync(key, json);
            }
        }

        public async Task StoreKey(string key, string value)
        {
            await Database.StringSetAsync(key, value);
        }

        public async Task<string> GetKey(string key)
        {
            return await Database.StringGetAsync(key);
        }

        public async Task<T?> GetKey<T>(string key)
        {
            var value = await Database.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(value)))
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream);
                }
            }
            return default;
        }

        public async Task<bool> KeyExists(string key)
        {
            return await Database.KeyExistsAsync(key);
        }

        public async Task<bool> StoreListItem<T>(T item)
        {
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                await JsonSerializer.SerializeAsync(stream, item);
                stream.Seek(0, SeekOrigin.Begin);
                var json = await reader.ReadToEndAsync();

                var isAdded = await Database.SetAddAsync("badUrls", json);

                return isAdded;
            }
        }
    }
}
