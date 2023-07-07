using System.Runtime.Caching;

namespace SiteNinja.Storage
{
    public interface ISiteDataStore
    {
        T GetData<T>(string key);

        void SetData<T>(string key, T value, DateTimeOffset expirationTime);
    }

    public class SiteDataStore : ISiteDataStore
    {
        private readonly ObjectCache _memoryCache;

        public SiteDataStore()
        {
            _memoryCache = System.Runtime.Caching.MemoryCache.Default;
        }

        public T GetData<T>(string key)
        {
            T item = (T)_memoryCache.Get(key);
            return item;
        }

        public void SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _memoryCache.Set(key, value, expirationTime);
            }
        }
    }
}
