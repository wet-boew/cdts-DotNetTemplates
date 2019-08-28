namespace GoC.WebTemplate.Components.Utils.Caching
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
