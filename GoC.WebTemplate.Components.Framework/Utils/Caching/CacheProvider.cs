using System.Web.Caching;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public class CacheProvider<T> : ICacheProvider<T>
    {
        private readonly Cache _cache;

        public CacheProvider(Cache cache)
        {
            _cache = cache;
        }

        public T Get(string key)
        {
            return (T)_cache[key];
        }

        public void Set(string key, T value)
        {
            if (_cache[key] == null)
            {
                _cache.Add(
                    key,
                    value,
                    null,
                    Cache.NoAbsoluteExpiration,
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.High,
                    null
                );
            }
        }

        public void SetWithCacheDependency(string key, string filePath, T value)
        {
            if (_cache[key] == null)
            {
                var cacheDependency = new CacheDependency(filePath);

                _cache.Add(
                    key,
                    value,
                    cacheDependency,
                    Cache.NoAbsoluteExpiration,
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.High,
                    null
                );
            }
        }
    }
}
