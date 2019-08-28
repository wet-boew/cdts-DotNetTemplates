using GoC.WebTemplate.Components.Configs;
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
}
