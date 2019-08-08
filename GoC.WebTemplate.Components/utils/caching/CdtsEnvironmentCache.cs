using GoC.WebTemplate.Components.Configs;
using System.Collections.Generic;
using System.Diagnostics;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public class CdtsEnvironmentCache
    {
        private readonly object _lock = new object();
        private const string cdtsEnvironmentsFilePath = "configs/CdtsEnvironments.json";

        private readonly ICacheProvider<IDictionary<string, ICdtsEnvironment>> _cacheProvider;

        public CdtsEnvironmentCache(ICacheProvider<IDictionary<string, ICdtsEnvironment>> cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public IDictionary<string, ICdtsEnvironment> GetContent()
        {
            Debug.Assert(_cacheProvider != null, "Cache proxy cannot be null");

            var cacheKey = Constants.CACHE_KEY_ENVIRONMENTS;
            var content = _cacheProvider.Get(cacheKey);

            // Implements the double-check pattern.
            if (content == null)
            {
                lock (_lock)
                {
                    content = _cacheProvider.Get(cacheKey);

                    if (content == null)
                    {
                        // Map the path.
                        //TODO: Map the filename to the web server path or throw Exception if filename isn't absolute path or create an IPathMapper.
                        string filePath = cdtsEnvironmentsFilePath;

                        // Read file content.

                        // We don't catch exceptions because this file needs to exist. 
                        // So we want the app to crash if it isn't.
                        content = JsonSerializationHelper.DeserializeEnvironments();

                        //---[ Now that the data is loaded, add it to the cache
                        _cacheProvider.SetWithCacheDependency(cacheKey, filePath, content);
                    }
                }
            }

            return content;
        }
    }
}
