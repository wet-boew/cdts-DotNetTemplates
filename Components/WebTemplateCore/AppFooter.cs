using System.Collections.Generic;

namespace GoC.WebTemplate
{

    /// <summary>
    /// Used to serialize to JSON for the application template in the wet.builder.appFooter call
    /// </summary>
    internal class AppFooter
    {
        public string CdnEnvVar { get; set; }
        public bool ShowFeatures { get; set; }
        public bool GlobalNav { get; set; }
        public List<FooterLink> FooterSections { get; set;  }
        public List<Link> ContactLinks { get; set; }
        public string TermsLink { get; set; }
        public string PrivacyLink { get; set; }
        public string SubTheme { get; set; }
        public string LocalPath { get; set; }
        public List<FooterLink> CustomFooterLinks { get; set; }
    }
}