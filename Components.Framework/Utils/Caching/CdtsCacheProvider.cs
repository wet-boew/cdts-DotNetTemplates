using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Cdts;
using System.Collections.Generic;
using System.Web.Caching;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public class CdtsCacheProvider : CacheProvider<IDictionary<string, ICdtsEnvironment>>, ICdtsCacheProvider
    {
        public CdtsCacheProvider(Cache cache)
            : base(cache)
        {
        }
    }

    public class CdtsSRIHashesCacheProvider : CacheProvider<IDictionary<string, IDictionary<string, string>>>, ICdtsSRIHashesCacheProvider
    {
        public CdtsSRIHashesCacheProvider(Cache cache)
            : base(cache)
        {
        }
    }
}
