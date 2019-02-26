using System;
using System.Configuration;
using GoC.WebTemplate.Components.ConfigSections;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components
{
    public class Configurations : ConfigurationSection
    {
        public static Configurations Settings { get; } = ConfigurationManager.GetSection("GoC.WebTemplate") as Configurations;
        
        //This section is required for deserilization of the section from the web config
        //This property enables validation on the xml of the config section
        //This property is no actually used in the code
        [ConfigurationProperty("xmlns", IsRequired = false)]
        public string XmlNameSpace
        {
            get { return (string) this["xmlns"]; }
            set { this["xmlns"] = value; }
        }

        [ConfigurationProperty("customSearch")]
        public string CustomSearch
        {
            get { return (string) this["customSearch"]; }
            set { this["customSearch"] = value; }
        }
        [ConfigurationProperty("signOutLinkURL")]
        public string SignOutLinkURL
        {
            get { return (string) this["signOutLinkURL"];  }
            set { this["signOutLinkURL"] = value; }
        }

        [ConfigurationProperty("signInLinkURL")]
        public string SignInLinkURL
        {
            get { return (string) this["signInLinkURL"];  }
            set { this["signInLinkURL"] = value; }
        }

        [ConfigurationProperty("customSiteMenuURL")]
        public string CustomSiteMenuURL
        {
            get { return (string) this["customSiteMenuURL"];  }
            set { this["customSiteMenuURL"] = value; }
        }
        
        // Create a "sessionTimeOut element."
        [ConfigurationProperty("sessionTimeOut")]
        public SessionTimeOutElement SessionTimeOut
        {
            get { return (SessionTimeOutElement)this["sessionTimeOut"]; }
            set { this["sessionTimeOut"] = value; }
        }

        // Create a "leavingSecureSiteWarning element."
        [ConfigurationProperty("leavingSecureSiteWarning")]
        public LeavingSecureSiteWarningElement LeavingSecureSiteWarning
        {
            get { return (LeavingSecureSiteWarningElement)this["leavingSecureSiteWarning"]; }
            set { this["leavingSecureSiteWarning"] = value; }
        }

        /// <summary>
        /// version
        /// </summary>
        [ConfigurationProperty("version", IsRequired = true)]
        public string Version
        {
            get { return (string)this["version"]; }
            set { this["version"] = value; }
        }

        /// <summary>
        /// theme
        /// </summary>
        [ConfigurationProperty("theme", IsRequired = false)]
        public string Theme
        {
            get { return (string)this["theme"]; }
            set { this["theme"] = value; }
        }


        /// <summary>
        /// cdts environment to use
        /// </summary>
        [ConfigurationProperty("environment", IsRequired = true)]
        public string Environment
        {
            get { return (string)this["environment"]; }
            set { this["environment"] = value; }
        }

        /// <summary>
        /// LoadJQueryFromGoogle
        /// </summary>
        [ConfigurationProperty("loadJQueryFromGoogle", DefaultValue = false, IsRequired = true)]
        public Boolean LoadJQueryFromGoogle
        {
            get { return (Boolean)this["loadJQueryFromGoogle"]; }
            set { this["loadJQueryFromGoogle"] = value; }
        }
        /// <summary>
        /// ShowPreContent
        /// </summary>
        [ConfigurationProperty("showPreContent", DefaultValue = false, IsRequired = true)]
        public Boolean ShowPreContent
        {
            get { return (Boolean)this["showPreContent"]; }
            set { this["showPreContent"] = value; }
        }
        /// <summary>
        /// ShowPostContent
        /// </summary>
        [ConfigurationProperty("showPostContent", DefaultValue = false, IsRequired = true)]
        public Boolean ShowPostContent
        {
            get { return (Boolean)this["showPostContent"]; }
            set { this["showPostContent"] = value; }
        }
        /// <summary>
        /// ShowFeedbackLink
        /// </summary>
        [ConfigurationProperty("showFeedbackLink", DefaultValue = false, IsRequired = true)]
        public Boolean ShowFeedbackLink
        {
            get { return (Boolean)this["showFeedbackLink"]; }
            set { this["showFeedbackLink"] = value; }
        }
        /// <summary>
        /// URL used to redirect users when they click the feedback link
        /// </summary>
        [ConfigurationProperty("feedbackLinkUrl", IsRequired = false)]
        public string FeedbackLinkUrl
        {
            get { return (string)this["feedbackLinkUrl"]; }
            set { this["feedbackLinkUrl"] = value; }
        }
        /// <summary>
         /// URL used to redirect users when they click the feedback link
         /// This link is specific for french if the user was already in the french culture
         /// If it is empty will asume FeebackLinkurl is bilingual or also non-existant
         /// </summary>
        [ConfigurationProperty("feedbackLinkUrlFr", IsRequired = false)]
        public string FeedbackLinkUrlFr
        {
            get { return (string)this["feedbackLinkUrlFr"]; }
            set { this["feedbackLinkUrlFr"] = value; }
        }
        /// <summary>
        /// ShowSearch
        /// </summary>
        [ConfigurationProperty("showSearch", DefaultValue = true, IsRequired = true)]
        public Boolean ShowShearch
        {
            get { return (Boolean)this["showSearch"]; }
            set { this["showSearch"] = value; }
        }
        /// <summary>
        /// ShowSharePageLink
        /// </summary>
        [ConfigurationProperty("showSharePageLink", DefaultValue = false, IsRequired = true)]
        public Boolean ShowSharePageLink
        {
            get { return (Boolean)this["showSharePageLink"]; }
            set { this["showSharePageLink"] = value; }
        }
        /// <summary>
        /// ShowLanguageLink
        /// </summary>
        [ConfigurationProperty("showLanguageLink", DefaultValue = true, IsRequired = true)]
        public Boolean ShowLanguageLink
        {
            get { return (Boolean)this["showLanguageLink"]; }
            set { this["showLanguageLink"] = value; }
        }

        /// <summary>
        /// StaticFilesLocation
        /// </summary>
        [ConfigurationProperty("useHTTPS", IsRequired = false)]
        public bool? UseHttps
        {
            get { return (bool?)this["useHTTPS"]; }
            set { this["useHTTPS"] = value; }
        }

        /// <summary>
        /// StaticFilesLocation
        /// </summary>
        [ConfigurationProperty("staticFilesLocation", IsRequired = true)]
        public string StaticFilesLocation
        {
            get { return (string)this["staticFilesLocation"]; }
            set { this["staticFilesLocation"] = value; }
        }
    }

}
