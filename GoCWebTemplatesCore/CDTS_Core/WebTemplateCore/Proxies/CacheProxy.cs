using CDTS_Core.CDTSExtensions;
using Microsoft.Extensions.Caching.Memory;

namespace CDTS_Core.WebTemplateCore.Proxies
{
    public class CacheProxy : ICacheProxy
    {
        private IMemoryCache MemCache;
        public CacheProxy(IMemoryCache memCache)
        {
            MemCache = memCache;
        }
        public void SaveToCache<T>(string key, string filename, T environments)
        {
            FileCacheDependency dependencies = new FileCacheDependency(filename);
            //HttpRuntime.Cache.Insert(key, environments, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
            MemCache.Set(key, environments);
        }

        public T GetFromCache<T>(string key)
        {
            return (T)MemCache.Get(key);
        }
    }

}
