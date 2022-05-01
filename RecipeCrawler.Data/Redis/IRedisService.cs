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
        Task<bool> StoreListItem<T>(T item);
        Task<List<T>> GetList<T>(string key, int page, int pageSize);
    }
}
