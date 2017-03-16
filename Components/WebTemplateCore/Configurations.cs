﻿using System;
using System.Configuration;
using GoC.WebTemplate.ConfigSections;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{
    public class Configurations : ConfigurationSection
    {
        private static Configurations settings = ConfigurationManager.GetSection("GoC.WebTemplate") as Configurations;

        public static Configurations Settings => settings;

        // Create a "sessionTimeOut element."
        [ConfigurationProperty("sessionTimeOut")]
        public SessionTimeOutElement SessionTimeOut
        {
            get
            {
                return (SessionTimeOutElement)this["sessionTimeOut"];
            }
            set
            { this["sessionTimeOut"] = value; }
        }

        // Create a "cdtsEnvironments collection."
        [ConfigurationProperty("cdtsEnvironments")]
        public CDTSEnvironmentCollection CDTSEnvironments => base["cdtsEnvironments"] as CDTSEnvironmentCollection;

        // Create a "leavingSecureSiteWarning element."
        [ConfigurationProperty("leavingSecureSiteWarning")]
        public leavingSecureSiteWarningElement leavingSecureSiteWarning
        {
            get
            {
                return (leavingSecureSiteWarningElement)this["leavingSecureSiteWarning"];
            }
            set
            { this["leavingSecureSiteWarning"] = value; }
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
        [ConfigurationProperty("theme", IsRequired = true)]
        public string Theme
        {
            get { return (string)this["theme"]; }
            set { this["theme"] = value; }
        }

        /// <summary>
        /// subTheme
        /// </summary>
        [ConfigurationProperty("subTheme")]
        public string SubTheme
        {
            get { return (string)this["subTheme"]; }
            set { this["subTheme"] = value; }
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
        /// use Https
        /// </summary>
        [ConfigurationProperty("useHTTPS", DefaultValue = true, IsRequired = true)]
        public Boolean useHTTPS
        {
            get { return (Boolean)this["useHTTPS"]; }
            set { this["useHTTPS"] = value; }
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
        public string FeedbackLinkurl
        {
            get { return (string)this["feedbackLinkUrl"]; }
            set { this["feedbackLinkUrl"] = value; }
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
        /// ShowFeatures
        /// </summary>
        [ConfigurationProperty("showFeatures", DefaultValue = false, IsRequired = true)]
        public Boolean ShowFeatures
        {
            get { return (Boolean)this["showFeatures"]; }
            set { this["showFeatures"] = value; }
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

    // Define the "sessionTimeOut" element 

    //Define the "cdtsEnvironments" collection

    //Define the "cdtsEnvironment" element
}
