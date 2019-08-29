using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Cdts;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public interface ICdtsCacheProvider : ICacheProvider<IDictionary<string, ICdtsEnvironment>>
    {
    }
}
