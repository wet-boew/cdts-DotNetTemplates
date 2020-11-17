using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Entities;
using System;
using System.Globalization;

namespace GoC.WebTemplate.Components.Configs
{
    public class WebTemplateSettings
    {
        private string _environment;
        private string _customSiteMenuUrl;

        /// <summary>
        /// The link to use for the sign out button, will only appear if <see cref="ShowSignOutLink"/> is set to true
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public string SignOutLinkUrl { get; set; }

        /// <summary>
        /// The link to use for the sign in button, will only appear if <see cref="ShowSignInLink"/> is set to true
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public string SignInLinkUrl { get; set; }

        /// <summary>
        /// A custom site menu to be used in place of the standard canada.ca site menu
        /// This defaults to null (use standard menu)
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public string CustomSiteMenuUrl
        {
            get
            {
                return _customSiteMenuUrl;
            }
            set
            {
                //TODO: Why are we forcing string.Empty as a NULL?
                _customSiteMenuUrl = (string.IsNullOrEmpty(value) ? null : value);
            }
        }

        /// <summary>
        /// Defines the session timeout properties
        /// The objects properties are set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public SessionTimeout SessionTimeout { get; set; } = new SessionTimeout();

        public LeavingSecureSiteSettings LeavingSecureSiteWarning { get; set; } = new LeavingSecureSiteSettings();

        /// <summary>
        /// Represents the Version of the CDN files to use to build the page. ex 4.0.17
        /// Set by application via web.config or programmatically
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// The environment to use (akamai, ESDCPRod, ESDCNonProd)
        /// The environment provided will determine the CDTS that will be used (url and cdnenv)
        /// Set by application via the web.config or programmatically
        /// </summary>
        public string Environment
        {
            get
            {
                return _environment;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                _environment = value.ToUpper(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Determines if the jQuery files should be loaded from google or from the CDN
        /// Set by application via web.config or programmatically
        /// </summary>
        public bool LoadScriptsFromGoogle { get; set; }

        /// <summary>
        /// Determines if the breadcrumbs of the top are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowBreadcrumbs { get; set; }

        /// <summary>
        /// Determines if the Pre Content of the header are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowPreContent { get; set; }

        /// <summary>
        /// Determines if the Post Content of the footer are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowPostContent { get; set; }

        public FeedbackLink FeedbackLink { get; set; } = new FeedbackLink();
        
        /// <summary>
        /// Determines if the Search control of the header are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowSearch { get; set; }

        /// <summary>
        /// Determines if the Share Link of the footer are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowSharePageLink { get; set; }

        /// <summary>
        /// Determines if the language toggle link  is to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowLanguageLink { get; set; }

        /// <summary>
        /// Determines if the communication between the browser and the CDTS should be encrypted
        /// Set by application via web.config or programmatically
        /// </summary>
        public bool? UseHttps { get; set; }

        public string StaticFilesLocation { get; set; }

        /// <summary>        
        /// Use this variable to activate and customize the built in Adobe Analytics (AA)
        /// </summary>
        public WebAnalytics WebAnalytics { get; set; } = new WebAnalytics();

        /// <summary>
        /// Use this in the intranet theme to change the GCTools links into a Modal
        /// </summary>
        public bool GcToolsModal { get; set; }

        public WebTemplateSettings() { }
        public WebTemplateSettings(GocWebTemplateConfigurationSection configurationSection)
        {
            if (configurationSection == null)
                throw new ArgumentNullException(nameof(configurationSection));

            CustomSiteMenuUrl = configurationSection.CustomSiteMenuURL;
            Environment = configurationSection.Environment;
            FeedbackLink.Show = configurationSection.ShowFeedbackLink;
            FeedbackLink.Url = configurationSection.FeedbackLinkUrl;
            FeedbackLink.UrlFr = configurationSection.FeedbackLinkUrlFr;
            LeavingSecureSiteWarning =
                new LeavingSecureSiteSettings()
                {
                    DisplayModalWindow = configurationSection.LeavingSecureSiteWarning.DisplayModalWindow,
                    Enabled = configurationSection.LeavingSecureSiteWarning.Enabled,
                    ExcludedDomains = configurationSection.LeavingSecureSiteWarning.ExcludedDomains,
                    RedirectUrl = configurationSection.LeavingSecureSiteWarning.RedirectURL
                };
            LoadScriptsFromGoogle = configurationSection.LoadJQueryFromGoogle;
            SessionTimeout =
                new SessionTimeout()
                {
                    AdditionalData = configurationSection.SessionTimeOut.AdditionalData,
                    Enabled = configurationSection.SessionTimeOut.Enabled,
                    Method = configurationSection.SessionTimeOut.Method,
                    Inactivity = configurationSection.SessionTimeOut.Inactivity,
                    LogoutUrl = configurationSection.SessionTimeOut.LogoutUrl,
                    ReactionTime = configurationSection.SessionTimeOut.ReactionTime,
                    RefreshCallBackUrl = configurationSection.SessionTimeOut.RefreshCallBackUrl,
                    RefreshLimit = configurationSection.SessionTimeOut.RefreshLimit,
                    RefreshOnClick = configurationSection.SessionTimeOut.RefreshOnClick,
                    SessionAlive = configurationSection.SessionTimeOut.SessionAlive
                };
            ShowBreadcrumbs = configurationSection.ShowBreadcrumbs;
            ShowLanguageLink = configurationSection.ShowLanguageLink;
            ShowPostContent = configurationSection.ShowPostContent;
            ShowPreContent = configurationSection.ShowPreContent;
            ShowSearch = configurationSection.ShowSearch;
            ShowSharePageLink = configurationSection.ShowSharePageLink;
            SignInLinkUrl = configurationSection.SignInLinkURL;
            SignOutLinkUrl = configurationSection.SignOutLinkURL;
            StaticFilesLocation = configurationSection.StaticFilesLocation;
            UseHttps = configurationSection.UseHttps;
            Version = configurationSection.Version;
            WebAnalytics =
                new WebAnalytics()
                {
                    Active = configurationSection.UseWebAnalytics ?? false,
                    Environment = WebAnalytics.EnvironmentOption.production,
                    Version = 2
                };
            GcToolsModal = configurationSection.GcToolsModal ?? false;
        }
    }
}