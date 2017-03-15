using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Net;
using System.Web;
using System.Globalization;
using System.Web.Caching;
using GOC.WebTemplate.ConfigSections;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{

    
    public class ApplicationDefTop
    {
        
        public string MenuPath { get; set; }
        public string CdnEnvVar { get; set; }
        public string AppName { get; set;  }
        public List<LanguageLink> LangLinks { get; set; }
        public bool SiteMenu { get; set; }

        public bool Secure { get; set; }
        /// <summary>
        /// This is an array but it should only have one item in it. 
        /// </summary>
        public List<Link> SignIn { get; set; }
        /// <summary>
        /// This is an array but it should only have one item in it. 
        /// </summary>
        public List<Link> SignOut { get; set; }
        public bool Search { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public string SubTheme { get; set; }
        public List<Link> IntranetTitle { get; set; }
        public bool ShowPreContent { get; set; }
        public string LocalPath { get; set; }
    }

    public class LanguageLink : Link
    {
        public string Lang { get; set; }

    }

    public class ApplicationDefFooter
    {
        public string CdnEnvVar { get; set; }
        public bool ShowFeatures { get; set; }
        public bool GlobalNav { get; set; }
        public List<Link> FooterSections { get; set;  }
        public List<Link> ContactLinks { get; set; }
        public string TermsLink { get; set; }
        public string PrivacyLink { get; set; }
    }

    public class Core
    {
        #region Enums

        /// <summary>
        /// Enum that represents the context of the list of links
        /// </summary>
        /// <remarks>The enum item name must match the parameter name expected by the Closure Template for the template to work.  The enum item name is used by the template as the paramter name when calling the Closure Template.</remarks>
        // ReSharper disable InconsistentNaming
        public enum LinkTypes { contactLinks, newsLinks, aboutLinks };
        /// <summary>
        /// Enum that represents the social sites to be displayed when the user clicks the "Share this Page" link.
        /// The list of accepted sites can be found here: http://wet-boew.github.io/v4.0-ci/docs/ref/share/share-en.html
        /// </summary>
        /// <remarks>The enum item name must match the name expected by the Closure Template for the template to work.  The enum item name is used by the Closure Template to retreive the url, image, text for that social site</remarks>
        public enum SocialMediaSites { bitly, blogger, delicious, digg, diigo, email, facebook, gmail, googleplus, linkedin, myspace, pinterest, reddit, stumbleupon, tumblr, twitter, yahoomail }; //NOTE: The item names must match the parameter names expected by the Closure Template for this to work

        /// <summary>
        /// Enum that represents the environments available with the CDTSs
        /// </summary>
        /// <remarks>
        /// Must be uppercase for logic to work
        /// The CDNURL and CDNEnv is decided based on these values
        /// </remarks>
        public enum CDTSEnvironments { AKAMAI, ESDCPROD, ESDCNNONPROD, ESDCQA };

        // ReSharper restore InconsistentNaming
        #endregion 

        private string twoLetterCulture;

        public Core()
        {

             _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            


           twoLetterCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            
            //Set properties
            WebTemplateVersion = Configurations.Settings.Version;
            WebTemplateTheme = Configurations.Settings.Theme;
            WebTemplateSubTheme = Configurations.Settings.SubTheme;
            Environment = Configurations.Settings.Environment;
            UseHTTPS = Configurations.Settings.useHTTPS;
            LoadJQueryFromGoogle = Configurations.Settings.LoadJQueryFromGoogle;
 
            this.SessionTimeout = new SessionTimeout();
            SessionTimeout.Enabled = Configurations.Settings.SessionTimeOut.Enabled;
            SessionTimeout.Inactivity = Configurations.Settings.SessionTimeOut.Inactivity;
            SessionTimeout.ReactionTime = Configurations.Settings.SessionTimeOut.ReactionTime;
            SessionTimeout.SessionAlive = Configurations.Settings.SessionTimeOut.Sessionalive;
            SessionTimeout.LogoutUrl = Configurations.Settings.SessionTimeOut.Logouturl;
            SessionTimeout.RefreshCallbackUrl = Configurations.Settings.SessionTimeOut.RefreshCallbackUrl;
            SessionTimeout.RefreshOnClick = Configurations.Settings.SessionTimeOut.RefreshOnClick;
            SessionTimeout.RefreshLimit = Configurations.Settings.SessionTimeOut.RefreshLimit;
            SessionTimeout.Method = Configurations.Settings.SessionTimeOut.Method;
            SessionTimeout.AdditionalData = Configurations.Settings.SessionTimeOut.AdditionalData;

            //Set Top section options        
            LanguageLink_URL = BuildLanguageLinkURL(HttpContext.Current.Request.QueryString.ToString());
            ShowPreContent = Configurations.Settings.ShowPreContent;
            ShowSearch = Configurations.Settings.ShowShearch;

            //Set preFooter section options
            ShowPostContent = Configurations.Settings.ShowPostContent;
            ShowFeedbackLink = Configurations.Settings.ShowFeedbackLink;
            FeedbackLink_URL = Configurations.Settings.FeedbackLinkurl;
            ShowLanguageLink = Configurations.Settings.ShowLanguageLink;
            ShowSharePageLink = Configurations.Settings.ShowSharePageLink;

            //Set Footer section options
            ShowFeatures = Configurations.Settings.ShowFeatures;
            LeavingSecureSiteWarning_Enabled = Configurations.Settings.leavingSecureSiteWarning.Enabled;
            LeavingSecureSiteWarning_DisplayModalWindow = Configurations.Settings.leavingSecureSiteWarning.DisplayModalWindow;
            leavingSecureSiteWarning_RedirectURL = Configurations.Settings.leavingSecureSiteWarning.RedirectURL;
            leavingSecureSiteWarning_ExcludedDomains = Configurations.Settings.leavingSecureSiteWarning.ExcludedDomains;

            this.HTMLHeaderElements = new List<string>();
            this.HTMLBodyElements = new List<string>();
            this.ContactLinks = new List<Link>();
            this.NewsLinks = new List<Link>();
            this.AboutLinks = new List<Link>();
            this.Breadcrumbs = new List<Breadcrumb>();
            this.SharePageMediaSites = new List<SocialMediaSites>();
            this.LeftMenuItems = new List<MenuSection>();
            
        }
        #region Properties

        private string cdnPath;
        private string staticFilesPath;
      
        /// <summary>
        /// property to hold the version of the template. it will be put as a comment in the html of the master pages. this will help us troubleshoot issues with clients using the template
        /// </summary>
        public string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
        /// <summary>
        /// Represents the list of links to override the About links in Footer
        /// Set by application programmatically
        /// </summary>
        public List<Link> AboutLinks { get; set; }
        /// <summary>
        /// The title that will be displayed in the header above the top menu.
        /// Set programmatically
        /// </summary>
        /// <remarks>only available for intranet themes</remarks>
        public string ApplicationTitle_Text { get; set; }
        /// <summary>
        /// The url of the title that will be displayed in the header above the top menu.
        /// Set programmatically
        /// </summary>
        /// <remarks>
        /// only available for intranet themes
        /// value is optional, if no value is supplied the theme will determine the url
        /// </remarks>
        public string ApplicationTitle_URL { get; set; }
        
        /// <summary>
        /// Represents the list of links for the Breadcrumbs
        /// Set by application programmatically
        /// </summary>
        public List<Breadcrumb> Breadcrumbs { get; set; }
        /// <summary>
        /// The environment to use (akamai, ESDCPRod, ESDCNonProd)
        /// The environment provided will determine the CDTS that will be used (url and cdnenv)
        /// Set by application via the web.config or programmatically
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// CDNEnv from the cdtsEnvironments node of the web.config, for the specified environment
        /// Set by application via web.config
        /// </summary>
        public string CDNEnvironment
        {
            get 
            {
                CDTSEnvironmentElement cdtsEnvironmentElement = Configurations.Settings.CDTSEnvironments[Environment];
                return cdtsEnvironmentElement.Env;
            }
        }
        /// <summary>
        /// The local path to be used during local testing or perfomance testing
        /// </summary>
        public string LocalPath
        {
            get
            {
                var cdtsEnvironmentElement = Configurations.Settings.CDTSEnvironments[Environment];
                return cdtsEnvironmentElement.LocalPath;
            }
        }
        /// <summary>
        /// URL from the cdtsEnvironments node of the web.config, for the specified environment
        /// Set by application via web.config
        /// </summary>
        public string CDNURL
        {
            get 
            {
                CDTSEnvironmentElement cdtsEnvironmentElement = Configurations.Settings.CDTSEnvironments[Environment];
                return cdtsEnvironmentElement.Path;
            }
        }
        /// <summary>
        /// Complete path of the CDN including http(s), theme and run or versioned
        /// Set by Core
        /// </summary>
        public string CDNPath
        {
            get 
            {
                if (string.IsNullOrEmpty(cdnPath))
                {
                    cdnPath = BuildCDNPath();
                }
                return cdnPath; 
            }
        }
        /// <summary>
        /// Represents the list of links to override the Contact links in Footer
        /// Set by application programmatically
        /// </summary>
        public List<Link> ContactLinks { get; set; }
        /// <summary>
        /// Represents the list of html elements to add to the header tag
        /// will be used to add metatags, css, js etc.
        /// Set by application programmatically
        /// </summary>
        public List<string> HTMLHeaderElements { get; set; }
        /// <summary>
        /// Represents the list of html elements to add at the end of the body tag
        /// will be used to add metatags, css, js etc.
        /// Set by application programmatically
        /// </summary>
        public List<string> HTMLBodyElements { get; set; }
        /// <summary>
        /// Represents the date modified displayed just above the footer
        /// Set by application programmatically
        /// </summary>
        public DateTime DateModified { get; set; }
        /// <summary>
        /// Determines if the features of the footer are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowFeatures { get; set; }
        /// <summary>
        /// Determines if the a warning should be displayed if the user navigates outside the secure session
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool LeavingSecureSiteWarning_Enabled { get; set; }
        /// <summary>
        /// Determines if the popup window should be displayed with the warning message if the user navigates outside the secure session
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool LeavingSecureSiteWarning_DisplayModalWindow { get; set; }
        /// <summary>
        /// URL to redirect to when sercuresitewarning is enabled and user clicked a link that leaves the secure session
        /// Set by application via web.config
        /// Can be set by application programmatically
        /// </summary>
        public string leavingSecureSiteWarning_RedirectURL { get; set; }
        /// <summary>
        /// A comma delimited list of domains that would be excluded from raising the warning
        /// Set by application via web.config
        /// Can be set by application programmatically
        /// </summary>
        public string leavingSecureSiteWarning_ExcludedDomains { get; set; }
        /// <summary>
        /// The warning message to be displayed to the user when clicking a link that leaves the secure session
        /// Set by application via web.config
        /// Can be set by application programmatically
        /// </summary>
        public string LeavingSecureSiteWarning_Message { get; set; }
        /// <summary>
        /// URL to be used for the feedback link
        /// Set by application via web.config
        /// or programmatically
        /// </summary>
        public string FeedbackLink_URL { get; set; }
        /// <summary>
        /// URL to be used for the Privacy link in transactional mode
        /// Set by application programmatically
        /// </summary>
        public string PrivacyLink_URL { get; set; }
        /// <summary>
        /// URL to be used for the Terms & Conditions link in transactional mode
        /// Set by application programmatically
        /// </summary>
        public string TermsConditionsLink_URL { get; set; }
        /// <summary>
        /// URL to be used for the language toggle
        /// Set/built by Template
        /// Can be set by application programmatically
        /// </summary>
        public string LanguageLink_URL { get; set; }
        /// <summary>
        /// Read only property, used to populate the Lang attribute of the language toggle link
        /// Value is defaulted by Template
        /// </summary>
        public string LanguageLink_Lang
        {
            get
            {
                if (string.Compare(TwoLetterCultureLanguage, Constants.ENGLISH_ACCRONYM, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return Constants.FRENCH_ACCRONYM;
                }
                else
                {
                    return Constants.ENGLISH_ACCRONYM;
                }
            }
        }
        /// <summary>
        /// Read only property, used to populate the text attribute of the language toggle link
        /// Value is defaulted by Template
        /// </summary>
        public string LanguageLink_Text
        {
            get
            {
                if (string.Compare(TwoLetterCultureLanguage, Constants.ENGLISH_ACCRONYM, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return Constants.LANGUAGE_LINK_FRENCH_TEXT;
                }
                else
                {
                    return Constants.LANGUAGE_LINK_ENGLISH_TEXT;
                }
            }
        }
        /// <summary>
        /// Represents a list of menu items
        /// </summary>
        public List<MenuSection> LeftMenuItems { get; set; }
        /// <summary>
        /// Represents the list of links to override the News links in Footer
        /// Set by application programmatically
        /// </summary>
        public List<Link> NewsLinks { get; set; }
        /// <summary>
        /// A unique string to identify a web page. Used by user to identify the screen where an issue occured.
        /// </summary>
        public string ScreenIdentifier { get; set; }
        /// <summary>
        /// Determines if the session timeout functionality are enable
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        [Obsolete("This property has been replaced with SessionTimeout.Enabled. SessionTimeout_Enabled will be removed in the next version.")]
        public bool SessionTimeout_Enabled { 
            get{ return SessionTimeout.Enabled;} 
            set{ SessionTimeout.Enabled = value;} 
        }
        /// <summary>
        /// Defines the session timeout properties
        /// The objects properties are set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public SessionTimeout SessionTimeout { get; set; }
        /// <summary>
        /// Determines if the Post Content of the footer are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowPostContent { get; set; }
        /// <summary>
        /// Determines if the FeedBack link of the footer is to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowFeedbackLink { get; set; }
        /// <summary>
        /// Determines if the language toggle link  is to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowLanguageLink { get; set; }
        /// <summary>
        /// Determines if the Share Link of the footer are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowSharePageLink { get; set; }
        /// <summary>
        /// Determines if the Search control of the header are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowSearch { get; set; }
        /// <summary>
        /// Representes the list of items to be displayed in the Share Page window
        /// Set by application programmatically
        /// </summary>
        public List<SocialMediaSites> SharePageMediaSites { get; set; }
        /// <summary>
        /// Determines the path to the location of the staticback up files
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        /// <remarks>The "theme" is concatenated to the end of this path.</remarks>
        public string StaticFilesPath 
        {
            get
            {
                if (staticFilesPath == null) staticFilesPath = string.Concat(Configurations.Settings.StaticFilesLocation, "/", WebTemplateTheme);
                return staticFilesPath;
            }
            set
            {
                staticFilesPath = value;
            }
        }
        /// <summary>
        /// Determines if the Pre Content of the header are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool ShowPreContent { get; set; }
        /// <summary>
        /// Retreive the first 2 letters of the current culture "en" or "fr"
        /// Used by generate paths, determine language etc...
        /// Set by Template
        /// </summary>
        public string TwoLetterCultureLanguage
        {
            get
            {
                return twoLetterCulture;
            }
        }
        /// <summary>
        /// title of page
        /// Set by application programmatically
        /// </summary>
        public string HeaderTitle { get; set; }
        /// <summary>
        /// version of application to be displayed instead of the date modified
        /// set by application programmatically
        /// </summary>
        public string VersionIdentifier { get; set; }
        /// <summary>
        /// Represents the Version of the CDN files to use to build the page. ex 4.0.17
        /// Set by application via web.config or programmatically
        /// </summary>
        public string WebTemplateVersion { get; set; }
        /// <summary>
        /// Represents the Theme to use to build the age. ex: GCWeb
        /// Set by application via web.config or programmatically
        /// </summary>
        public string WebTemplateTheme { get; set; }
        /// <summary>
        /// Represents the sub Theme to use to build the age. ex: esdc
        /// Set by application via web.config or programmatically
        /// </summary>
        public string WebTemplateSubTheme { get; set; }
        /// <summary>
        /// Determines if the communication between the browser and the CDTS should be encrypted
        /// Set by application via web.config or programmatically
        /// </summary>
        public bool UseHTTPS { get; set; }
        /// <summary>
        /// Determines if the jQuery files should be loaded from google or from the CDN
        /// Set by application via web.config or programmatically
        /// </summary>
        public bool LoadJQueryFromGoogle { get; set; }

        #endregion


        public bool ShowSecure { get; set; }
        public bool ShowSignInLink { get; set; }
        public bool ShowSignOutLink { get; set; }
        public Link SignInLink { get; set; }
        public Link SignOutLink { get; set; }
        public string ApplicationName { get; set; }

        #region Renderers


        public HtmlString RenderApplicationDefTop()
        {
            var defTop = new ApplicationDefTop();
            defTop.AppName = ApplicationName;
            defTop.SignIn = BuildSignInLink();
            defTop.SignOut = BuildSignOutLink();
            defTop.Secure = ShowSecure;
            defTop.CdnEnvVar = CDNEnvironment;
            defTop.SubTheme = WebTemplateSubTheme;
            defTop.Search = ShowSearch;
            defTop.LangLinks = BuildLanguageLinkList();
            defTop.ShowPreContent = ShowPreContent;
            defTop.IntranetTitle = BuildIntranetTitle();
            defTop.Breadcrumbs = Breadcrumbs;
            defTop.LocalPath = BuildLocalPath();
            defTop.SiteMenu = true;
            defTop.MenuPath = null;
            return new HtmlString(JsonConvert.SerializeObject(defTop, _settings));
        }

        private List<Link> BuildSignOutLink()
        {
            if (!ShowSignOutLink || SignOutLink == null)
            {
                return null;
            }
            if (ShowSignInLink && ShowSignOutLink)
            {
                throw new InvalidOperationException("Unable to show sign in and sign out link together");
            }
            return new List<Link> {SignOutLink};
        }

        private List<Link> BuildSignInLink()
        {
            if (!ShowSignInLink || SignInLink == null)
            {
                return null;
            }

            if (ShowSignInLink && ShowSignOutLink)
            {
                throw new InvalidOperationException("Unable to show sign in and sign out link together");
            }

            return new List<Link> {SignInLink};
        }


        private string BuildLocalPath()
        {
            if (string.IsNullOrWhiteSpace(LocalPath))
            {
                return null;
            }
            return string.Format(LocalPath, WebTemplateTheme, WebTemplateVersion);
        }


        private List<Link> BuildIntranetTitle()
        {
            if (string.IsNullOrWhiteSpace(ApplicationTitle_Text))
            {
                return null;
            }

            return new List<Link> {
                new Link
                {
                    Href = ApplicationTitle_URL,
                    Text = ApplicationTitle_Text
                }
            };
        }

        private List<LanguageLink> BuildLanguageLinkList()
        {
            if (!ShowLanguageLink)
            {
                return null;
            }

            return new List<LanguageLink>
            {
                new LanguageLink
                {
                    Href = LanguageLink_URL,
                    Text = LanguageLink_Text,
                    Lang = LanguageLink_Lang
                }
            };
        }

        public HtmlString RenderFooterLinks(bool transactionalMode)
        {
            StringBuilder sb = new StringBuilder();

            if (transactionalMode == true)
            {
                //contact, terms, privacy links
                RenderLinksList(sb, LinkTypes.contactLinks, ContactLinks);
                RenderTermsConditionsLink(sb);
                RenderPrivacyLink(sb);
            }
            else
            {
                //contact, news, about links
                RenderLinksList(sb, LinkTypes.contactLinks, ContactLinks);
                RenderLinksList(sb, LinkTypes.newsLinks, NewsLinks);
                RenderLinksList(sb, LinkTypes.aboutLinks, AboutLinks);
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to represent a list of links for:
        ///   - Contact Us
        ///   - About Us
        ///   - News
        /// The list of links is provided by the application
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="linkType"></param>
        /// <param name="links"></param>
        /// <returns>string in the format expected by the Closure Templates to generate the list of links</returns>
        /// <example>contactLinks: [{ href: '#', text: 'CLink 1' }, { href: '#', text: 'CLink 2', acronym: 'Con' }]</example>
        private void RenderLinksList(StringBuilder sb, LinkTypes linkType, List<Link> links)
        {
           
            //contactLinks: [{ href: '#', text: 'CLink 1' }, { href: '#', text: 'CLink 2', acronym: 'Con' }]
            if (links != null && links.Count > 0)
            {
                sb.Append(linkType);
                sb.Append(": [");
                // TO DO  check Linq
                foreach (Link lk in links)
                {
                    sb.Append("{href: '");
                    sb.Append(lk.Href);
                    sb.Append("', text: '");
                    sb.Append(WebUtility.HtmlEncode(lk.Text));
                    sb.Append("'},");
                }
                sb.Append("],");
            }
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to represent the application title that is displayed above the top menu
        /// The text and url for the title is provided programmatically by the consumer
        /// Will only be displayed if the text is supplied
        /// if no URL is provided the theme/subtheme will provide a default value
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to generate the application title content</returns>
        /// <example>//intranetTitle: [{href: "http://hrsdc.prv/eng/iit/index.shtml", text: "IITB"}]</example>
        public HtmlString RenderApplicationTitle()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(ApplicationTitle_Text))
            {
                //intranetTitle: [{href: "http://hrsdc.prv/eng/iit/index.shtml", text: "IITB"}],
                sb.Append("intranetTitle: [{");
                if (!string.IsNullOrEmpty(ApplicationTitle_URL))
                {
                    sb.Append(string.Concat(" href: \"", ApplicationTitle_URL, "\","));
                }
                sb.Append(string.Concat(" text: \"", ApplicationTitle_Text, "\""));
                sb.Append(" }],");
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to represent the breadcrumb links
        /// The list of breadcrumbs is provided by the application
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to generate the breadcrumb content</returns>
        /// <example>breadcrumbs: [{ title: 'Home', href: 'http://www.canada.ca/en/index.htm' }, { title: 'CDN Sample', acronym: 'Content Delivery Network Sample' }]</example>
        public HtmlString RenderBreadcrumbsList()
        {
            StringBuilder sb = new StringBuilder();

            //breadcrumbs: [{ title: 'Home', href: 'http://www.canada.ca/en/index.htm' }, { title: 'CDN Sample', acronym: 'Content Delivery Network Sample' }]

            if (Breadcrumbs != null && Breadcrumbs.Count > 0)
            {
                sb.Append("breadcrumbs: [");
                // TO DO  check Linq
                foreach (Breadcrumb lk in Breadcrumbs)
                {
                    sb.Append("{title: '");
                    sb.Append(WebUtility.HtmlEncode(lk.Title));
                    if (string.IsNullOrEmpty(lk.Href) == false)
                    {
                        sb.Append("', href: '");
                        sb.Append(lk.Href);
                    }

                    if (string.IsNullOrEmpty(lk.Acronym) == false)
                    {
                        sb.Append("', acronym: '");
                        sb.Append(WebUtility.HtmlEncode(lk.Acronym));
                    }
                    sb.Append("'},");
                }
                sb.Append("],");
            }
            return new HtmlString(sb.ToString());
        }
        
        /// <summary>
        /// Builds a string with the format required by the closure templates, to represent the feedback link
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to generate the feedback link</returns>
        /// <example>
        /// /// showFeedback: false
        /// or
        /// showFeedback: +url provided
        /// or
        /// empty string.  this will diplay the button with the default url
        /// </example>
        public HtmlString RenderFeedbackLink()
        {
            string feedbackJSon = string.Empty;

            if (ShowFeedbackLink)
            {
                if(!string.IsNullOrEmpty(FeedbackLink_URL))
                {
                    feedbackJSon = string.Concat("showFeedback: \"", FeedbackLink_URL, "\",");
                }
            }
            else
            {
                feedbackJSon= "showFeedback: false,";
            }

            return new HtmlString(feedbackJSon);
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to display the screen identifier
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to generate the screen identifier</returns>
        /// <example>
        /// screenIdentifier: "asdfasdf-asdf323"
        /// </example>
        public HtmlString RenderScreenIdentifier()
        {            
            return !string.IsNullOrEmpty(ScreenIdentifier) ?
                       new HtmlString(string.Concat("screenIdentifier: \"", ScreenIdentifier, "\",")) :
                       null;
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to display or not the search control
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to generate the search control</returns>
        /// <example>
        /// /// search: false
        /// or
        /// search: true
        /// </example>
        public string RenderSearch()
        {
            return ShowSearch == true ?
                       "search: true," :
                       "search: false,";
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to retrieve the jQuery files from google or cdts
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to retrieve the jQuery files from google or cdts
        /// jqueryEnv: 'external' gets files from Google
        /// if false, the string (including the parameter name) will be empty
        /// </returns>
        /// <example>
        /// jqueryEnv: 'external'
        /// </example>
        public HtmlString RenderjQuery()
        {
            return LoadJQueryFromGoogle == true ?
                new HtmlString("jqueryEnv: \"external\",") : null;
        }

        public HtmlString RenderLocalPath()
        {
            if (string.IsNullOrWhiteSpace(LocalPath))
            {
                return null;
            }
            return new HtmlString("localPath: '" + String.Format(LocalPath,WebTemplateTheme,WebTemplateVersion) + "'");
        }
        /// <summary>
        /// Builds a string with the format required by the closure templates, to represent the Share Page links
        /// </summary>
        /// <remarks>If the application did not supply the Share Page items, the Share Page link will not be displayed</remarks>
        /// <returns>string in the format expected by the Closure Templates to generate the Share Page link</returns>
        /// <example>
        /// /// showShare: false
        /// or
        /// showShare: ["email", "facebook", "linkedin", "twitter"]
        /// </example>
        public HtmlString RenderSharePageMediaSites()
        {
            StringBuilder sb = new StringBuilder();

            //showShare: false
            //showShare: ["email", "facebook", "linkedin", "twitter"]

            if (ShowSharePageLink == true && (SharePageMediaSites.Count > 0))
            {
                sb.Append("showShare: [");

                foreach (SocialMediaSites site in SharePageMediaSites)
                {
                    sb.Append(string.Concat(" '", site, "', "));
                }
                sb.Append("],");
            }
            else // don't display the feedback link
            {
                sb.Append("showShare: false,");
            }

            return new HtmlString(sb.ToString());
        }
        /// <summary>
        /// Builds the html of the WET Session Timeout control that provides session timeout and inactivity functionality.
        /// For more documentation: https://wet-boew.github.io/v4.0-ci/demos/session-timeout/session-timeout-en.html
        /// </summary>
        /// <returns>The html of the WET session timeout control
        /// </returns>
        public HtmlString RenderSessionTimeoutControl()
        {
            StringBuilder sb = new StringBuilder();
            //<span class='wb-sessto' data-wb-sessto='{"inactivity": 5000, "reactionTime": 30000, "sessionalive": 10000, "logouturl": "http://www.tsn.com", "refreshCallbackUrl": "http://www.cnn.com", "refreshOnClick": "33", "refreshLimit": 2, "method": "555", "additionalData": "666"}'></span>

            if (this.SessionTimeout.Enabled == true)
            {
                sb.Append("<span class='wb-sessto' data-wb-sessto='{");
                sb.Append("\"inactivity\": ");
                sb.Append(this.SessionTimeout.Inactivity);
                sb.Append(", \"reactionTime\": ");
                sb.Append(this.SessionTimeout.ReactionTime);
                sb.Append(", \"sessionalive\": ");
                sb.Append(this.SessionTimeout.SessionAlive);
                sb.Append(", \"logouturl\": \"");
                sb.Append(this.SessionTimeout.LogoutUrl);
                sb.Append("\"");               
                if (!string.IsNullOrEmpty(this.SessionTimeout.RefreshCallbackUrl))
                {
                    sb.Append(", \"refreshCallbackUrl\": \"");
                    sb.Append(this.SessionTimeout.RefreshCallbackUrl);
                    sb.Append("\"");
                }
                sb.Append(", \"refreshOnClick\": ");
                sb.Append(this.SessionTimeout.RefreshOnClick.ToString().ToLower());
                
                if (this.SessionTimeout.RefreshLimit > 0)
                {
                    sb.Append(", \"refreshLimit\": ");
                    sb.Append(this.SessionTimeout.RefreshLimit);
                }
                if (!string.IsNullOrEmpty(this.SessionTimeout.Method))
                {
                    sb.Append(", \"method\": \"");
                    sb.Append(this.SessionTimeout.Method);
                    sb.Append("\"");
                }
                if (!string.IsNullOrEmpty(this.SessionTimeout.AdditionalData))
                {
                    sb.Append(", \"additionalData\": \"");
                    sb.Append(this.SessionTimeout.AdditionalData);
                    sb.Append("\"");
                }
                sb.Append("}'></span>");
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to represent the features/activities
        /// </summary>
        /// <remarks></remarks>
        /// <returns>string in the format expected by the Closure Templates to generate the features</returns>
        /// <example>
        /// showFeatures: true
        /// </example>
        public string RenderFeatures()
        {

            //showFeatures: true,
            return ShowFeatures == true ?
                        "showFeatures: true," :
                        "showFeatures: false,";
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to display the language toggle link
        /// </summary>
        /// <remarks></remarks>
        /// <returns>string in the format expected by the Closure Templates to generate the language toggle link</returns>
        /// <example>
        /// lngLinks: [{ lang: 'fr', href: '?GoCTemplateCulture=fr-CA', text: 'Français'}],
        /// </example>
        public HtmlString RenderLanguageLink()
        {
            //lngLinks: [{ lang: 'fr', href: '?GoCTemplateCulture=fr-CA', text: 'Français'}],
            StringBuilder sb = new StringBuilder();

            if (ShowLanguageLink)
            {
                sb.Append("lngLinks: [{");
                    sb.Append(" lang: '");
                    sb.Append(this.LanguageLink_Lang);
                    sb.Append("',");

                    sb.Append(" href: '");
                    sb.Append(this.LanguageLink_URL);
                    sb.Append("',");

                    sb.Append(" text: '");
                    sb.Append(this.LanguageLink_Text);
                    sb.Append("'");
                sb.Append("}],");
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to represent the left side menu
        /// </summary>
        /// <returns>
        /// string in the format expected by the Closure Templates to generate the left menu
        /// </returns>
        public HtmlString RenderLeftMenu()
        {
            StringBuilder sb = new StringBuilder();

            // sectionName: 'Section menu', menuLinks: [{ href: '#', text: 'Link 1' }, { href: '#', text: 'Link 2' }]"

            if (this.LeftMenuItems.Count > 0)
            {
                sb.Append("sections: [");
                foreach (MenuSection menuSection in this.LeftMenuItems)
                {
                    //add section name
                    sb.Append(" {sectionName: '");
                    sb.Append(WebUtility.HtmlEncode(menuSection.Name));
                    sb.Append("',");

                    //add section link
                    if (!string.IsNullOrEmpty(menuSection.Link))
                    {
                        sb.Append(" sectionLink: '");
                        sb.Append(WebUtility.HtmlEncode(menuSection.Link));
                        sb.Append("',");
                        if (menuSection.OpenInNewWindow == true)
                        {
                            sb.Append("newWindow: true,");
                        }
                    }

                    //add menu items
                    if (menuSection.Items.Count > 0)
                    {
                        sb.Append(" menuLinks: [");
                        foreach (Link lk in menuSection.Items)
                        {
                            sb.Append("{href: '");
                            sb.Append(lk.Href);
                            sb.Append("', text: '");
                            sb.Append(WebUtility.HtmlEncode(lk.Text));
                            sb.Append("'");
                                                                                  
                            //Add 3rd level sub items, Note: Template is limiting to 3 levels even if Core allows more
                            if (lk is MenuItem)
                            {
                                MenuItem mi = (MenuItem)lk;

                                //the following if statement needs to be here for backward compatibility OpenInNewWindow is only available to MenuItems not Link
                                if (mi.OpenInNewWindow == true)
                                {
                                    sb.Append(", newWindow: true");
                                }

                                if (mi.SubItems.Count > 0)
                                {
                                    sb.Append(", subLinks: [");
                                    foreach (MenuItem sublk in mi.SubItems)
                                    {
                                        sb.Append("{subhref: '");
                                        sb.Append(sublk.Href);
                                        sb.Append("', subtext: '");
                                        sb.Append(WebUtility.HtmlEncode(sublk.Text));
                                        sb.Append("',");
                                        if(sublk.OpenInNewWindow == true)
                                        { 
                                            sb.Append(" newWindow: true");
                                        }
                                        sb.Append("},");
                                    }
                                    sb.Append("]");
                                }
                            }   
                            sb.Append("},");
                        }
                        sb.Append("]");
                    }
                    sb.Append("},");
                }
                sb.Append("]");
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to manage the leaving secure site warning
        /// </summary>
        /// <returns>
        /// string in the format expected by the Closure Templates to manage the leaving secure site warning
        /// </returns>
        public HtmlString RenderLeavingSecureSiteWarning()
        {
            StringBuilder sb = new StringBuilder();

            //exitScript: true,
            //displayModal: true,
            //exitURL: "http://www.google.ca"
            //exitMsg: "You are about to leave a secure site, do you wish to continue?"

            if (this.LeavingSecureSiteWarning_Enabled == true && !string.IsNullOrEmpty(this.leavingSecureSiteWarning_RedirectURL))
            {
                sb.Append("exitScript: true,");
                if (this.LeavingSecureSiteWarning_DisplayModalWindow == false)
                {
                    sb.Append(" displayModal: false,");
                }
                sb.Append(" exitURL: \"");
                sb.Append(this.leavingSecureSiteWarning_RedirectURL);
                sb.Append("\",");
                sb.Append(" exitMsg: \"");
                sb.Append(WebUtility.HtmlEncode(this.LeavingSecureSiteWarning_Message));
                sb.Append("\",");
                if (!string.IsNullOrEmpty(this.leavingSecureSiteWarning_ExcludedDomains))
                {
                    sb.Append("exitDomains: \"");
                    sb.Append(this.leavingSecureSiteWarning_ExcludedDomains);
                    sb.Append("\",");
                }
            }
            else
            {
                sb.Append("exitScript: false,");
            }

            return new HtmlString(sb.ToString());
        }

        public HtmlString RenderHtmlHeaderElements()
        {
            return RenderHtmlElements(this.HTMLHeaderElements);
        }
        public HtmlString RenderHtmlBodyElements()
        {
            return RenderHtmlElements(this.HTMLBodyElements);
        }
        /// <summary>
        /// Adds a string(html tag) to be included in the page
        /// This is our way off letting the developer add metatags, css and js to their pages.
        /// </summary>
        /// <remarks>we are accepting a string with no validation, therefore it is up to the developer to provide a valid string/html tag</remarks>
        /// <returns>
        /// string hopefully a valid html tag
        /// </returns>
        private HtmlString RenderHtmlElements(List<string> tags)
        {
            StringBuilder sb = new StringBuilder();

            if (tags.Count > 0)
            {
                foreach (string tag in tags)
                {
                    sb.Append(tag + "\r\n");
                }
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to manage the date modified or version identifier displayed on screen
        /// </summary>
        /// <remarks>only 1 of the 2 can be displayed, if XX is provided then xx will be displayed, ignoting the other property</remarks>
        /// <returns>
        /// string in the format expected by the Closure Templates to manage the date modified or version identifier
        /// </returns>
        public HtmlString RenderDateModifiedVersionIdentifier()
        {
            StringBuilder sb = new StringBuilder();

            if (DateTime.Compare(this.DateModified, DateTime.MinValue) == 0 && !string.IsNullOrEmpty(this.VersionIdentifier))
            {
                sb.Append("versionIdentifier: \"");
                sb.Append(this.VersionIdentifier.Trim());
                sb.Append("\",");
            }
            else
            {
                sb.Append("dateModified: \"");
                sb.Append(this.DateModified.ToString("yyyy-MM-dd"));// format for english and french are identical, else would need if.
                sb.Append("\",");
            }

            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to manage the terms and conditions link in transaction mode only
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to manage the terms and conditions link in transaction mode only</returns>
        private void RenderTermsConditionsLink(StringBuilder sb)
        {
            if(!string.IsNullOrEmpty(TermsConditionsLink_URL))
            {
                sb.Append("termsLink: \"" + TermsConditionsLink_URL + "\",");
            }
        }
            
        /// <summary>
        /// Builds a string with the format required by the closure templates, to manage the privacy notice link in transaction mode only
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to manage the privacy notice link in transaction mode only</returns>
        private void RenderPrivacyLink(StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(TermsConditionsLink_URL))
            {
                sb.Append("privacyLink: \"" + PrivacyLink_URL + "\",");
            }
        }
        #endregion

        /// <summary>
        /// Builds the path to the cdn based on the environment set in the config. The path is based on the url of the environment, theme and version
        /// </summary>
        /// <returns>String, the complete path to the cdn</returns>
        private string BuildCDNPath()
        {
            string https = string.Empty;

            if (UseHTTPS == true)
            {
                https = "s";
            }

            if (string.Compare(Environment, CDTSEnvironments.AKAMAI.ToString(), true) == 0)
            {
                if (string.IsNullOrEmpty(WebTemplateVersion))
                {
                    cdnPath = string.Format(CultureInfo.InvariantCulture, CDNURL, https, WebTemplateTheme, "rn");
                }
                else
                {
                    cdnPath = string.Format(CultureInfo.InvariantCulture, CDNURL, https, WebTemplateTheme, WebTemplateVersion);
                }
            }
            else //Using ESDC CDTS (SSL)
            {
                if (string.IsNullOrEmpty(WebTemplateVersion))
                {
                    cdnPath = string.Format(CultureInfo.InvariantCulture, CDNURL, https, "rn", WebTemplateTheme, WebTemplateVersion);
                }
                else
                {
                    //the extra "/" at the end is added after the version, since this adds a folder to the original path
                    cdnPath = string.Format(CultureInfo.InvariantCulture, CDNURL, https, "app", WebTemplateTheme, string.Concat(WebTemplateVersion, "/"));
                }
            }
            return cdnPath;
        }

        /// <summary>
        /// Builds the URL to be used by the English/francais link at the top of the page for the language toggle.
        /// The method will add or update the "GoCTemplateCulture" querystring parameter with the culture to be set
        /// The language toggle link, posts back to the same page, and the InitializedCulture method of the BasePage is responsible for setting the culture with the provided value
        /// </summary>
        /// <returns>The URL to be used for the language toggle link</returns>
        private string BuildLanguageLinkURL(string queryString)
        {
            string urlPath = string.Empty;
            System.Collections.Specialized.NameValueCollection nameValues = HttpUtility.ParseQueryString(queryString);

            //Set the value of the "GoCTemplateCulture" parameter
            if (this.TwoLetterCultureLanguage.StartsWith(Constants.ENGLISH_ACCRONYM, StringComparison.OrdinalIgnoreCase))
            { nameValues.Set(Constants.QUERYSTRING_CULTURE_KEY, Constants.FRENCH_CULTURE); }
            else
            { nameValues.Set(Constants.QUERYSTRING_CULTURE_KEY, Constants.ENGLISH_CULTURE); }

            string url = string.Concat("?", nameValues.ToString());

            return url;
        }

        /// <summary>
        /// Arbritrary object to act as a mutex to obtain a class-scope lock accros all threads.
        /// </summary>
        /// <remarks></remarks>
        private static object lockObject = new object();

        private readonly JsonSerializerSettings _settings;

        /// <summary>
        /// This method is used to get the static file content from the cache. if the cache is empty it will read the content from the file and load it into the cache.
        /// </summary>
        /// <param name="fileName">static file name to retreive</param>
        /// <returns>A string containing the content of the file.</returns>
        public HtmlString LoadStaticFile(string fileName)
        {
            string info;
            CacheDependency cacheDep;
            string filePath = string.Empty;
            string cacheKey = string.Concat(Constants.CACHE_KEY_STATICFILES_PREFIX, ".", fileName);

            // Attempt to lookup from cache
            info = (string)HttpRuntime.Cache.Get(cacheKey);
            if ((info != null))
            {
                // Object was found in cache, simply return it and get out!
                return new HtmlString(info);
            }

            // ---[ If we get here, the object was not found in the cache, we'll have to load it.
            lock (lockObject)
            {
                //---[ Attempt to get from cache again now that we are locked
                info = (string)HttpRuntime.Cache.Get(cacheKey);
                if ((info != null))
                {
                    // Once again, object was found in cache, simply return it and get out!
                    return new HtmlString(info);
                }

                //---[ If we get here, we really have to load the data
                if (StaticFilesPath.StartsWith("~"))
                {
                    filePath = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(StaticFilesPath), fileName);
                }
                else
                {
                    filePath = System.IO.Path.Combine(StaticFilesPath, fileName);
                }

                try
                {
                    info = System.IO.File.ReadAllText(filePath);
                    //---[ Now that the data is loaded, add it to the cache
                    cacheDep = new CacheDependency(filePath);

                    HttpRuntime.Cache.Insert(cacheKey, info, cacheDep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    info = string.Empty;
                }
                catch (System.IO.FileNotFoundException)
                {
                    info = string.Empty;
                }
            }
            return new HtmlString(info);
        }
    }
}
