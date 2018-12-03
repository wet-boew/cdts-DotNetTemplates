using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace CDTS_Core.CDTSExtensions
{
    public static class MemoryCacheExtensions
    {
        public static void Set<TItem>(this IMemoryCache cache, string key, TItem value, FileCacheDependency dependency)
        {
            var fileInfo = new FileInfo(dependency.FileName);
            var fileProvider = new PhysicalFileProvider(fileInfo.DirectoryName);
            cache.Set(key, value, new MemoryCacheEntryOptions()
                .AddExpirationToken(fileProvider.Watch(fileInfo.Name)));

        }
    }
}
