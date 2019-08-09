using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Framework.Utils.Caching;
using GoC.WebTemplate.Components.Utils.Caching;
using System.Collections.Generic;
using System.Web.Caching;

namespace GoC.WebTemplate.Components.Core.Utils.Caching
{
    public class CdtsCacheProvider : CacheProvider<IDictionary<string, ICdtsEnvironment>>, ICdtsCacheProvider
    {
        public CdtsCacheProvider(Cache cache)
            : base(cache)
        {
        }
    }
}
