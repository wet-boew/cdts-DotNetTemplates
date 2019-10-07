using System.Diagnostics;
using System.IO;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public  class FileContentCache
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

        public string LoadFile(string filePath)
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
