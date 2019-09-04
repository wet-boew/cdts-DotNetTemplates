using GoC.WebTemplate.Components.Configs.Cdts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GoC.WebTemplate.Components.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001008d5f6450b0971b527406a5095d6ff2dd6b581b4693a50577abe42714af66bfc82e74dda446e8e985acba56b46f6522f13a94299f118456e17d9bac7e8fd7791eb780c63101dee66774dde04b7649f3de9793ad24ca052c2e842b8e40ef814a0b3d96cde9e39540055a95d230068ba89226ead08cade22467f28f6f64fa726cb0")]
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
        internal IDictionary<string, ICdtsEnvironment> DeserializeEnvironments()
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