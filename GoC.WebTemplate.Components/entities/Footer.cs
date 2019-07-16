using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class Footer
    {
        public string CdnEnv { get; set; }
        public string SubTheme { get; set; }
        public bool ShowFooter { get; set; }
        public List<Link> ContactLinks { get; set; }
        public string PrivacyLink { get; set; }
        public string TermsLink { get; set; } 
        public string LocalPath { get; set; }
    }
}