using GoC.WebTemplate.Components.Utils.Caching;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using System.IO;

namespace GoC.WebTemplate.Components.Core.Utils.Caching
{
    public class FileContentMemoryCacheProvider : MemoryCacheProvider<string>, IFileContentCacheProvider
    {
        private readonly IHostingEnvironment HostingEnvironment;

        public FileContentMemoryCacheProvider(IMemoryCache cache, IHostingEnvironment hostingEnvironment)
            : base(cache)
        {
            HostingEnvironment = hostingEnvironment;
        }

        public string GetFullFilePath(string fileName, string staticFilePath)
        {
            return Path.Combine(HostingEnvironment.ContentRootPath, staticFilePath, fileName);
        }
    }
}
