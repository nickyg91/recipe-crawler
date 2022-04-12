namespace RecipeCrawler.Data.Redis
{
    public interface IRedisService
    {
        Task Connect();
        Task StoreKey<T>(string key, T value);
        Task StoreKey(string key, string value);
        Task<string> GetKey(string key);
        Task<T?> GetKey<T>(string key);
    }
}
