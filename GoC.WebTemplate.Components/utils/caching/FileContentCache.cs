using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GoC.WebTemplate.Components.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001008d5f6450b0971b527406a5095d6ff2dd6b581b4693a50577abe42714af66bfc82e74dda446e8e985acba56b46f6522f13a94299f118456e17d9bac7e8fd7791eb780c63101dee66774dde04b7649f3de9793ad24ca052c2e842b8e40ef814a0b3d96cde9e39540055a95d230068ba89226ead08cade22467f28f6f64fa726cb0")]
namespace GoC.WebTemplate.Components.Utils.Caching
{
    internal class FileContentCache
    {
        private readonly object _lock = new object();

        private readonly IFileContentCacheProvider _cacheProvider;

        public FileContentCache(IFileContentCacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public string GetContent(string filename, string staticFilePath)
        {
            Debug.Assert(_cacheProvider != null, "Cache proxy cannot be null");

            var cacheKey = $"{Constants.CACHE_KEY_STATICFILES_PREFIX}.{filename}";
            var content = _cacheProvider.Get(filename);

            // Implements the double-check pattern.
            if (content == null)
            {
                lock (_lock)
                {
                    content = _cacheProvider.Get(filename);

                    if (content == null)
                    {
                        // Map the path.
                        string filePath = _cacheProvider.GetFullFilePath(filename, staticFilePath);

                        content = LoadFile(filePath);

                        // Now that the data is loaded, add it to the cache
                        _cacheProvider.SetWithCacheDependency(cacheKey, filePath, content);
                    }
                }
            }

            return content;
        }

        internal string LoadFile(string filePath)
        {
            string content;

            // Read file content.
            try
            {
                content = File.ReadAllText(filePath);
            }
            catch (DirectoryNotFoundException)
            {
                //TODO: Why is this not being logged?
                content = string.Empty;
            }
            catch (FileNotFoundException)
            {
                //TODO: Why is this not being logged?
                content = string.Empty;
            }

            return content;
        }
    }
}
