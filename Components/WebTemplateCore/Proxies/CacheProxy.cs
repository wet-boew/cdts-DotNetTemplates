using System.Web;
using System.Web.Caching;

namespace GoC.WebTemplate.Components.Proxies
{
    public class CacheProxy : ICacheProxy
    {
        public void SaveToCache<T>(string key, string filename, T environments)
        {
            using (var cacheDep = new CacheDependency(filename))
                HttpRuntime.Cache.Insert(key,
                    environments,
                    cacheDep,
                    Cache.NoAbsoluteExpiration,
                    Cache.NoSlidingExpiration);
        }

        public T GetFromCache<T>(string key)
        {
            return (T) HttpRuntime.Cache.Get(key);
        }
    }
}