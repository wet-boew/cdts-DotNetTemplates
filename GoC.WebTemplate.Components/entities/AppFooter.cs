using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    internal class AppFooter
    {
        public string CdnEnv { get; set; }
        public List<IFooterSection> FooterSections { get; set; }
        public List<Link> ContactLink { get; set; }
        public string TermsLink { get; set; }
        public string PrivacyLink { get; set; }
        public string SubTheme { get; set; }
        public string LocalPath { get; set; }
    }
}