using GoC.WebTemplate.Components.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class Top
    {
        public string CdnEnv { get; set; }
        public string SubTheme { get; set; }
        public List<IntranetTitle> IntranetTitle { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool Search { get; set; }
        public List<LanguageLink> LngLinks { get; set; }
        public bool ShowPreContent { get; set; }

        //We have to use custom serializers because breadcrumbs can be both bool and a list of breadcrumbs.
        [JsonConverter(typeof(BreadcrumbListConverter))]
        public BreadcrumbList Breadcrumbs { get; set; }
        public string LocalPath { get; set; }

        [JsonProperty(DefaultValueHandling=DefaultValueHandling.Include)]
        public bool TopSecMenu { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool SiteMenu { get; set; }
        public List<CustomSearch> CustomSearch { get; set; }

        [JsonProperty(PropertyName = "GCToolsModal")]
        public bool GcToolsModal { get; set; }
    }
}
