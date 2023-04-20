using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Cdts;
using System.Collections.Generic;
using System.Web.Caching;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public class CdtsCacheProvider : CacheProvider<EnvironmentMaps>, ICdtsCacheProvider
    {
        public CdtsCacheProvider(Cache cache)
            : base(cache)
        {
        }
    }
}
