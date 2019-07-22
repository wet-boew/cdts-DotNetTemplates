using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Utils.Caching;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Framework.Utils.Caching
{
    public static class CacheExtensions
    {
        public static ICacheProvider<string> GetFileContentCacheProvider(this System.Web.Caching.Cache cache)
        {
            return new CacheProvider<string>(cache);
        }

        public static ICacheProvider<IDictionary<string, ICdtsEnvironment>> GetCdtsEnvironmentCacheProvider(this System.Web.Caching.Cache cache)
        {
            return new CacheProvider<IDictionary<string, ICdtsEnvironment>>(cache);
        }
    }
}
