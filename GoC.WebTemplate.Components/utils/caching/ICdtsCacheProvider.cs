using GoC.WebTemplate.Components.Configs;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Utils.Caching
{
    public interface ICdtsCacheProvider : ICacheProvider<IDictionary<string, ICdtsEnvironment>>
    {
    }
}
