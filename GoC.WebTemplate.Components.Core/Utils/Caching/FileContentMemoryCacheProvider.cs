using GoC.WebTemplate.Components.Utils.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace GoC.WebTemplate.Components.Core.Utils.Caching
{
    public class FileContentMemoryCacheProvider : MemoryCacheProvider<string>, IFileContentCacheProvider
    {
        public FileContentMemoryCacheProvider(IMemoryCache cache)
            : base(cache)
        {
        }
    }
}
