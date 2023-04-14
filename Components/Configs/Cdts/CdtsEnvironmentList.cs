using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Configs.Cdts
{
    public class CdtsEnvironmentList
    {
        /**
         * Map of theme/version -> fileName -> sriHash
         *
         * This deeply nested Dictionary of Dictionary is to avoid creating custom
         * classes for each level of nesting.
         */
        public IDictionary<string, IDictionary<string, string>> ThemeSRIHashes { get; set; }
    }
}
