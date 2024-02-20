using GoC.WebTemplate.Components.Entities;

namespace GoC.WebTemplate.Components.Configs
{
    public interface IWebTemplateSettings
    {
        /// <summary>
        /// A custom site menu to be used in place of the standard canada.ca site menu
        /// This defaults to null (use standard menu)
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
#pragma warning disable CA1056
        string CustomSiteMenuUrl { get; set; }
#pragma warning restore CA1056

        /// <summary>
        /// The environment to use (akamai, ESDCPRod, ESDCNonProd)
        /// The environment provided will determine the CDTS that will be used (url and cdnenv)
        /// Set by application via the web.config or programmatically
        /// </summary>
        string Environment { get; set; }
        
        FeedbackLink FeedbackLink { get; set; }

        /// <summary>
        /// Use this in the intranet theme to change the GCTools links into a Modal
        /// </summary>
        bool GcToolsModal { get; set; }       

        LeavingSecureSiteSettings LeavingSecureSiteWarning { get; set; }

        /// <summary>
        /// Determines if the jQuery files should be loaded from google or from the CDN
        /// Set by application via web.config or programmatically
        /// </summary>
        bool LoadScriptsFromGoogle { get; set; }

        /// <summary>
        /// Defines the session timeout properties
        /// The objects properties are set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        SessionTimeout SessionTimeout { get; set; }

        /// <summary>
        /// Determines if the language toggle link  is to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        bool ShowLanguageLink { get; set; }

        /// <summary>
        /// Determines if the Post Content of the footer are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        bool ShowPostContent { get; set; }

        /// <summary>
        /// Determines if the Pre Content of the header are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        bool ShowPreContent { get; set; }

        /// <summary>
        /// Determines if the Search control of the header are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        bool ShowSearch { get; set; }

        /// <summary>
        /// Determines if the Share Link of the footer are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        bool ShowSharePageLink { get; set; }

#pragma warning disable CA1056
        /// <summary>
        /// The link to use for the sign in button, will only appear if <see cref="ShowSignInLink"/> is set to true
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        string SignInLinkUrl { get; set; }

        /// <summary>
        /// The link to use for the sign out button, will only appear if <see cref="ShowSignOutLink"/> is set to true
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        string SignOutLinkUrl { get; set; }
#pragma warning restore CA1056

        /// <summary>
        /// Used to determine if SRI is enabled
        /// </summary>
        bool SRIEnabled { get; set; }

        string StaticFilesLocation { get; set; }

        /// <summary>
        /// Determines if the communication between the browser and the CDTS should be encrypted
        /// Set by application via web.config or programmatically
        /// </summary>
        bool? UseHttps { get; set; }

        /// <summary>
        /// Represents the Version of the CDN files to use to build the page. ex 4.0.17
        /// Set by application via web.config or programmatically
        /// </summary>
        string Version { get; set; }

        /// <summary>        
        /// Use this variable to activate and customize the built in Adobe Analytics (AA)
        /// </summary>
        WebAnalytics WebAnalytics { get; set; }
    }
}