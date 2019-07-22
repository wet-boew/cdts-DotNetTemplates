using GoC.WebTemplate.Components.Utils.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace GoC.WebTemplate.Components.Core.Utils.Caching
{
    public class CacheProvider<T> : ICacheProvider<T>
    {
        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get(string key)
        {
            // Look for cache key.
            _cache.TryGetValue(key, out T cacheEntry);

            return cacheEntry;
        }

        public void Set(string key, T value)
        {
            // Set cache options.
            //TODO: Check that MemoryCacheEntryOptions match the previous .NET Framework options (Cache.NoAbsoluteExpiration and Cache.NoSlidingExpiration)
            var cacheEntryOptions = new MemoryCacheEntryOptions();

            _cache.Set(key, value, cacheEntryOptions);
        }

        public void SetWithCacheDependency(string key, string filePath, T value)
        {
            // Set cache options.
            //TODO: Check that MemoryCacheEntryOptions match the previous .NET Framework options (Cache.NoAbsoluteExpiration and Cache.NoSlidingExpiration)
            var cacheEntryOptions = new MemoryCacheEntryOptions();

            _cache.Set(key, value, cacheEntryOptions);
        }
    }
}
