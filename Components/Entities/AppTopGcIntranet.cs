using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    internal class AppTopGcIntranet: AppTop
    {
        public List<IntranetTitle> IntranetTitle { get; set; }

        [JsonProperty(PropertyName = "GCToolsModal")]
        public bool GcToolsModal { get; set; }
    }
}