using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    /// <summary>
    /// Objects of this class are meant to be serialized to a JSON object to be passed as part of "base" 
    /// parameter to the 'wet.builder.setup' JavaScript function in the template pages. (see Setup class)
    /// </summary>
    public class SetupBase
    {
        public string SubTheme { get; set; }
        public string JqueryEnv { get; set; }
        public ExitSecureSite ExitSecureSite { get; set; }
        public List<WebAnalytics> WebAnalytics { get; set; }
        public bool? SRIEnabled { get; set; }
    }
}
