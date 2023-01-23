using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class TransactionalFooter
    {
        public string CdnEnv { get; set; }
        public string SubTheme { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool ShowFooter { get; set; }
        public List<Link> ContactLinks { get; set; }
        public List<FooterLink> PrivacyLink { get; set; }
        public List<FooterLink> TermsLink { get; set; }
        public string LocalPath { get; set; }
        public bool HideFooterMain { get; set; }
        public bool HideFooterCorporate { get; set; }
        public ContextualFooter ContextualFooter { get; set; }
    }
}
