using System.Collections.Generic;
using GoC.WebTemplate;

namespace WebTemplateCore.JSONSerializationObjects
{

    /// <summary>
    /// Used to serialize to JSON for the application template in the wet.builder.appFooter call
    /// </summary>
    internal class AppFooter
    {
        public string CdnEnv { get; set; }
        public bool ShowFeatures { get; set; }
        public List<FooterLink> FooterSections { get; set;  }
        public string ContactLink { get; set; }
        public string TermsLink { get; set; }
        public string PrivacyLink { get; set; }
        public string SubTheme { get; set; }
        public string LocalPath { get; set; }
    }
}