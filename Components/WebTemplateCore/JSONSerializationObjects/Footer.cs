using System.Collections.Generic;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
    public class Footer
    {
        public string CdnEnv { get; set; }
        public string SubTheme { get; set; }
        public bool ShowFooter { get; set; }
        public List<Link> ContactLinks { get; set; }
        public List<FooterLink> PrivacyLink { get; set; }
        public List<FooterLink> TermsLink { get; set; } 
        public string LocalPath { get; set; }
    }
}