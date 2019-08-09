using System.Diagnostics;
using System.IO;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    internal class FileContentCache
    {
        private readonly object _lock = new object();

        private readonly ICacheProvider<string> _cacheProvider;

        public FileContentCache(ICacheProvider<string> cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public string GetContent(string filename)
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
                        //TODO: Map the filename to the web server path or throw Exception if filename isn't absolute path or create an IPathMapper.
                        string filePath = filename;

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

                        //---[ Now that the data is loaded, add it to the cache
                        _cacheProvider.SetWithCacheDependency(cacheKey, filePath, content);
                    }
                }
            }

            return content;
        }
    }
}
