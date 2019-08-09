using GoC.WebTemplate.Components.Framework.Utils.Caching;
using GoC.WebTemplate.Components.Utils.Caching;
using System.Web.Caching;

namespace GoC.WebTemplate.Components.Core.Utils.Caching
{
    public class FileContentCacheProvider : CacheProvider<string>, IFileContentCacheProvider
    {
        public FileContentCacheProvider(Cache cache)
            : base(cache)
        {
        }
    }
}
