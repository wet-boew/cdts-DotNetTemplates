using System;
using System.Configuration;

namespace CDTS_Core.WebTemplate
{
    public class GCConfigurations
    {
        [ConfigurationProperty("xmlns", IsRequired = false)]
        public string XmlNameSpace { get; set; }

        [ConfigurationProperty("customSearch")]
        public string CustomSearch { get; set; }

        [ConfigurationProperty("signOutLinkURL")]
        public string SignOutLinkURL { get; set; }

        [ConfigurationProperty("signInLinkURL")]
        public string SignInLinkURL { get; set; }

        [ConfigurationProperty("customSiteMenuURL")]
        public string CustomSiteMenuURL { get; set; }

        [ConfigurationProperty("showSiteMenu", DefaultValue = true)]
        [Obsolete("no longer used. Will be removed in a future release.")]
        public bool ShowSiteMenu { get; set; }

        [ConfigurationProperty("showGlobalNav")]
        [Obsolete("no longer used. Will be removed in a future release.")]
        public bool ShowGlobalNav { get; set; }

        [ConfigurationProperty("sessionTimeOut")]
        public SessionTimeOutElement SessionTimeOut { get; set; }

        [ConfigurationProperty("leavingSecureSiteWarning")]
        public LeavingSecureSiteWarningElement LeavingSecureSiteWarning { get; set; }

        [ConfigurationProperty("version", IsRequired = true)]
        public string Version { get; set; }

        [ConfigurationProperty("theme", IsRequired = false)]
        public string Theme { get; set; }

        [ConfigurationProperty("environment", IsRequired = true)]
        public string Environment { get; set; }

        [ConfigurationProperty("loadJQueryFromGoogle", DefaultValue = false, IsRequired = true)]
        public bool LoadJQueryFromGoogle { get; set; }

        [ConfigurationProperty("showPreContent", DefaultValue = false, IsRequired = true)]
        public bool ShowPreContent { get; set; }

        [ConfigurationProperty("showPostContent", DefaultValue = false, IsRequired = true)]
        public bool ShowPostContent { get; set; }

        [ConfigurationProperty("showFeedbackLink", DefaultValue = false, IsRequired = true)]
        public bool ShowFeedbackLink { get; set; }

        [ConfigurationProperty("feedbackLinkUrl", IsRequired = false)]
        public string FeedbackLinkurl { get; set; }

        [ConfigurationProperty("showSearch", DefaultValue = true, IsRequired = true)]
        public bool ShowShearch { get; set; }

        [ConfigurationProperty("showSharePageLink", DefaultValue = false, IsRequired = true)]
        public bool ShowSharePageLink { get; set; }

        [ConfigurationProperty("showLanguageLink", DefaultValue = true, IsRequired = true)]
        public bool ShowLanguageLink { get; set; }

        [ConfigurationProperty("showFeatures", DefaultValue = false, IsRequired = true)]
        public bool ShowFeatures { get; set; }

        [ConfigurationProperty("useHTTPS", IsRequired = false)]
        public bool? UseHttps { get; set; }

        [ConfigurationProperty("staticFilesLocation", IsRequired = true)]
        public string StaticFilesLocation { get; set; }
    }
}