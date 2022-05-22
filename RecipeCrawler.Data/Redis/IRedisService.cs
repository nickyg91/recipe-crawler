namespace RecipeCrawler.Data.Redis
{
    public interface IRedisService
    {
        void Connect();
        Task StoreKey<T>(string key, T value) where T : class;
        Task StoreKey(string key, string value);
        Task<string> GetKey(string key);
        Task<T?> GetKey<T>(string key);
        Task<bool> KeyExists(string key);
        Task<List<T>> GetList<T>(string key, int page, int pageSize);
        Task<long> GetSetCount(string key);
        Task<bool> AddToSet<T>(string key, T value);
        Task<bool> AddToSortedSet<T>(string key, T value);
        Task<long> GetSortedSetCount(string key);
        Task<List<T>> GetItemsFromSortedSet<T>(string key, int page, int pagesize, string searchBy);
    }
}
