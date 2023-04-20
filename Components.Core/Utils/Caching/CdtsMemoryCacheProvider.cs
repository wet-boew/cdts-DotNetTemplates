using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Utils.Caching;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Core.Utils.Caching
{
    public class CdtsMemoryCacheProvider : MemoryCacheProvider<EnvironmentMaps>, ICdtsCacheProvider
    {
        public CdtsMemoryCacheProvider(IMemoryCache cache)
            : base(cache)
        {
        }
    }
}
