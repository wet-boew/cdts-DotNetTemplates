using System.Configuration;

namespace CDTS_Core.WebTemplate
{
    public class LeavingSecureSiteWarningElement : ConfigurationElement
    {
        [ConfigurationProperty("enabled", DefaultValue = false, IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return (bool)base["enabled"];
            }
            set
            {
                base["enabled"] = value;
            }
        }

        [ConfigurationProperty("displayModalWindow", DefaultValue = true, IsRequired = true)]
        public bool DisplayModalWindow
        {
            get
            {
                return (bool)base["displayModalWindow"];
            }
            set
            {
                base["displayModalWindow"] = value;
            }
        }

        [ConfigurationProperty("redirectURL", IsRequired = false)]
        public string RedirectURL
        {
            get
            {
                return (string)base["redirectURL"];
            }
            set
            {
                base["redirectURL"] = value;
            }
        }

        [ConfigurationProperty("excludedDomains", IsRequired = false)]
        public string ExcludedDomains
        {
            get
            {
                return (string)base["excludedDomains"];
            }
            set
            {
                base["excludedDomains"] = value;
            }
        }
    }
}
