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

        public async Task<List<T>> GetList<T>(string key, int page, int pageSize)
        {
            var setValues = Database.SetScanAsync(key, pageSize: pageSize, pageOffset: (page - 1) * pageSize);
            var list = new List<T>();
            var numberOfItems = 0;
            await foreach (var item in setValues)
            {
                if (numberOfItems == pageSize)
                {
                    break;
                }
                var value = JsonSerializer.Deserialize<T>(item);
                if (value != null)
                {
                    list.Add(value);
                    numberOfItems++;
                }
                
            }
            return list;
        }

        public async Task<long> GetSetCount(string key)
        {
            return await Database.SetLengthAsync(key);
        }

        public async Task<bool> AddToSet<T>(string key, T value)
        {
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                await JsonSerializer.SerializeAsync(stream, value);
                stream.Seek(0, SeekOrigin.Begin);
                var json = await reader.ReadToEndAsync();
                var added = await Database.SetAddAsync(key, json);
                return added;
            }
        }

        public async Task<bool> AddToSortedSet<T>(string key, T value)
        {
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                await JsonSerializer.SerializeAsync(stream, value);
                stream.Seek(0, SeekOrigin.Begin);
                var json = await reader.ReadToEndAsync();
                var added = await Database.SortedSetAddAsync(key, json, json.GetHashCode());
                return added;
            }
        }

        public async Task<long> GetSortedSetCount(string key)
        {
            return await Database.SortedSetLengthAsync(key);
        }

        public async Task<List<T>> GetItemsFromSortedSet<T>(string key, int page, int pagesize, string? searchBy)
        {
            var items = await Database.SortedSetRangeByRankWithScoresAsync(key, (page - 1) * (pagesize - 1), ((page - 1) * (pagesize - 1)) + (pagesize - 1));
            if (!string.IsNullOrEmpty(searchBy))
            {
                items = items.Where(x => x.ToString().Contains(searchBy, StringComparison.OrdinalIgnoreCase)).ToArray();
            }
            var itemsToReturn = new List<T>();
            using var stream = new MemoryStream();
            foreach (var item in items)
            {
                stream.Write(System.Text.Encoding.UTF8.GetBytes(item.Element.ToString()));
                stream.Position = 0;
                var result = await JsonSerializer.DeserializeAsync<T>(stream);
                if (result != null)
                {
                    itemsToReturn.Add(result);
                }
                stream.Position = 0;
                stream.SetLength(0);
            }
            return itemsToReturn;
        }
    }
}
