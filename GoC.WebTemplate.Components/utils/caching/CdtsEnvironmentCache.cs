using GoC.WebTemplate.Components.Configs.Cdts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    internal class CdtsEnvironmentCache
    {
        //Because of how JSonDesrialization works we need to have a container class for the environments.
        private class EnvironmentContainer
        {
            public List<CdtsEnvironment> Environments { get; set; }
        }

        private const string ResouceName = @"GoC.WebTemplate.Components.Configs.Cdts.CdtsEnvironments.json";

        private readonly object _lock = new object();

        private readonly ICdtsCacheProvider _cacheProvider;

        public CdtsEnvironmentCache(ICdtsCacheProvider cacheProvider)
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
                        // We don't catch exceptions because this file needs to exist. 
                        // So we want the app to crash if it isn't.
                        content = DeserializeEnvironments();

                        //---[ Now that the data is loaded, add it to the cache
                        _cacheProvider.Set(cacheKey, content);
                    }
                }
            }

            return content;
        }

        /// <summary>
        /// Deserialize the CDTSEnvironment objects, this is public incase someone wants to implement their
        /// own caching implementation
        /// </summary>
        /// <returns>A dictionary of environments with the ICDTSEnvironment.Name being the key.</returns>
        public IDictionary<string, ICdtsEnvironment> DeserializeEnvironments()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResouceName))
            {
                if (stream == null)
                    throw new MissingManifestResourceException($"Cannot find resource {ResouceName}.");

                using (var reader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var serializer = new JsonSerializer
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    };
                    var environments = serializer.Deserialize<EnvironmentContainer>(jsonReader);
                    return environments.Environments.Cast<ICdtsEnvironment>().ToDictionary(x => x.Name, x => x);
                }
            }
        }
    }
}