using System.Configuration;

namespace GoC.WebTemplate.Components.ConfigSections
{
    // ReSharper disable once InconsistentNaming
    public class LeavingSecureSiteWarningElement : ConfigurationElement
    {

        /// <summary>
        /// enabled
        /// </summary>
        [ConfigurationProperty("enabled", DefaultValue = false, IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        /// <summary>
        /// enabled
        /// </summary>
        [ConfigurationProperty("displayModalWindow", DefaultValue = true, IsRequired = true)]
        public bool DisplayModalWindow
        {
            get { return (bool)this["displayModalWindow"]; }
            set { this["displayModalWindow"] = value; }
        }

        /// <summary>
        /// URL that users are sent to when "yes" is selected on the warning message.  URL to write application code clean up before redirecting to selected url.
        /// </summary>
        [ConfigurationProperty("redirectURL", IsRequired = false)]
        public string RedirectURL
        {
            get { return (string)this["redirectURL"]; }
            set { this["redirectURL"] = value; }
        }

        /// <summary>
        /// domains that should not raise the warning message
        /// </summary>
        [ConfigurationProperty("excludedDomains", IsRequired = false)]
        public string ExcludedDomains
        {
            get { return (string)this["excludedDomains"]; }
            set { this["excludedDomains"] = value; }
        }
    }
}