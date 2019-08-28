using System.Web.Caching;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public class FileContentCacheProvider : CacheProvider<string>, IFileContentCacheProvider
    {
        public FileContentCacheProvider(Cache cache)
            : base(cache)
        {
        }
    }
}
