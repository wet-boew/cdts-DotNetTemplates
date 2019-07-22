namespace GoC.WebTemplate.Components.Utils
{
    public class Cache : ICache
    {
        public void SaveToCache<T>(string key, string filename, T environments)
        {
            //var cacheDep = new CacheDependency(filename);
            //HttpRuntime.Cache.Insert(key,
            //    environments,
            //    cacheDep,
            //    Cache.NoAbsoluteExpiration,
            //    Cache.NoSlidingExpiration);
        }

        public T GetFromCache<T>(string key)
        {
            return default(T);
            //return (T) HttpRuntime.Cache.Get(key);
        }
    }
}