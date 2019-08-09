using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using GoC.WebTemplate.Components.Utils.Caching;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Framework.Utils.Caching
{
    public static class CacheExtensions
    {
        public static IFileContentCacheProvider GetFileContentCacheProvider(this System.Web.Caching.Cache cache)
        {
            return new FileContentCacheProvider(cache);
        }

        public static ICdtsCacheProvider GetCdtsEnvironmentCacheProvider(this System.Web.Caching.Cache cache)
        {
            return new CdtsCacheProvider(cache);
        }
    }
}
