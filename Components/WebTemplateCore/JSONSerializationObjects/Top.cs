using System.Collections.Generic;
using GoC.WebTemplate;

namespace WebTemplateCore.JSONSerializationObjects
{
    public class Top
    {
        public string CdnEnv { get; set; }
        public string SubTheme { get; set; }
        public List<Link> IntranetTitle { get; set; }
        public bool Search { get; set; }
        public List<LanguageLink> LngLinks { get; set; }
        public bool ShowPreContent { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public string LocalPath { get; set; }
        public bool SiteMenu { get; set; }
    }
}