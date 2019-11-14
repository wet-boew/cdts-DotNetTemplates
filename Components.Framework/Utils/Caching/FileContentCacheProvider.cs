using System.Web.Caching;
using System.Web;
using System;
using System.IO;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public class FileContentCacheProvider : CacheProvider<string>, IFileContentCacheProvider
    {
        public FileContentCacheProvider(Cache cache)
            : base(cache)
        {
        }

        public string GetFullFilePath(string fileName, string staticFilePath)
        {
            return HttpContext.Current.Server.MapPath(Path.Combine(staticFilePath, fileName));
        }
    }
}
