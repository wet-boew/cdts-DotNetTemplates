using System.Collections.Generic;

namespace CDTS_Core.WebTemplateCore.JsonSerializationObjects
{
    internal class AppFooter
    {
        public string CdnEnv
        {
            get;
            set;
        }

        public bool ShowFeatures
        {
            get;
            set;
        }

        public List<FooterLink> FooterSections
        {
            get;
            set;
        }

        public List<Link> ContactLink { get; set; }

        public string TermsLink
        {
            get;
            set;
        }

        public string PrivacyLink
        {
            get;
            set;
        }

        public string SubTheme
        {
            get;
            set;
        }

        public string LocalPath
        {
            get;
            set;
        }
    }

}
