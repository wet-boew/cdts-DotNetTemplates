using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Net;
using System.Web;
using System.Globalization;
using System.Linq;
using System.Web.Caching;
using GoC.WebTemplate.ConfigSections;
using GoC.WebTemplate.Proxies;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


// ReSharper disable once CheckNamespace

namespace GoC.WebTemplate
{
    public class Core
    {
        private readonly IConfigurationProxy _configProxy;

        #region Enums

        /// <summary>
        /// Enum that represents the context of the list of links
        /// </summary>
        /// <remarks>The enum item name must match the parameter name expected by the Closure Template for the template to work.  The enum item name is used by the template as the paramter name when calling the Closure Template.</remarks>
        // ReSharper disable InconsistentNaming
        public enum LinkTypes
        {
            contactLinks,
            newsLinks,
            aboutLinks
        };

        /// <summary>
        /// Enum that represents the social sites to be displayed when the user clicks the "Share this Page" link.
        /// The list of accepted sites can be found here: http://wet-boew.github.io/v4.0-ci/docs/ref/share/share-en.html
        /// </summary>
        /// <remarks>The enum item name must match the name expected by the Closure Template for the template to work.  The enum item name is used by the Closure Template to retreive the url, image, text for that social site</remarks>
        public enum SocialMediaSites
        {
            bitly,
            blogger,
            delicious,
            digg,
            diigo,
            email,
            facebook,
            gmail,
            googleplus,
            linkedin,
            myspace,
            pinterest,
            reddit,
            stumbleupon,
            tumblr,
            twitter,
            yahoomail
        }; //NOTE: The item names must match the parameter names expected by the Closure Template for this to work

        /// <summary>
        /// Enum that represents the environments available with the CDTSs
        /// </summary>
        /// <remarks>
        /// Must be uppercase for logic to work
        /// The CDNURL and CDNEnv is decided based on these values
        /// </remarks>
        public enum CDTSEnvironments
        {
            AKAMAI,
            ESDCPROD,
            ESDCNNONPROD,
            ESDCQA
        };

        // ReSharper restore InconsistentNaming

        #endregion


        public Core(ICurrentRequestProxy currentRequest, IConfigurationProxy configProxy)
        {
            _configProxy = configProxy;
            _currentEnvironment = _configProxy.CurrentEnvironment;
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            //Set properties
            WebTemplateVersion = _configProxy.Version;
            WebTemplateTheme = _configProxy.Theme;
            WebTemplateSubTheme = _configProxy.SubTheme;

            Environment = _configProxy.Environment;
            UseHTTPS = _configProxy.UseHTTPS;
            LoadJQueryFromGoogle = _configProxy.LoadJQueryFromGoogle;

            SessionTimeout = new SessionTimeout
            {
                Enabled = _configProxy.SessionTimeOut.Enabled,
                Inactivity = _configProxy.SessionTimeOut.Inactivity,
                ReactionTime = _configProxy.SessionTimeOut.ReactionTime,
                SessionAlive = _configProxy.SessionTimeOut.Sessionalive,
                LogoutUrl = _configProxy.SessionTimeOut.Logouturl,
                RefreshCallbackUrl = _configProxy.SessionTimeOut.RefreshCallbackUrl,
                RefreshOnClick = _configProxy.SessionTimeOut.RefreshOnClick,
                RefreshLimit = _configProxy.SessionTimeOut.RefreshLimit,
                Method = _configProxy.SessionTimeOut.Method,
                AdditionalData = _configProxy.SessionTimeOut.AdditionalData
            };

            //Set Top section options        
            LanguageLink = new LanguageLink
            {
                Href = BuildLanguageLinkURL(currentRequest.QueryString)
            };
            ShowPreContent = _configProxy.ShowPreContent;
            ShowSearch = _configProxy.ShowShearch;

            //Set preFooter section options
            ShowPostContent = _configProxy.ShowPostContent;
            ShowFeedbackLink = _configProxy.ShowFeedbackLink;
            FeedbackLinkURL = _configProxy.FeedbackLinkurl;
            ShowLanguageLink = _configProxy.ShowLanguageLink;
            ShowSharePageLink = _configProxy.ShowSharePageLink;

            //Set Footer section options
            ShowFeatures = _configProxy.ShowFeatures;

            LeavingSecureSiteWarning = new LeavingSecureSiteWarning
            {
                Enabled = _configProxy.LeavingSecureSiteWarning.Enabled,
                DisplayModalWindow = _configProxy.LeavingSecureSiteWarning.DisplayModalWindow,
                RedirectURL = _configProxy.LeavingSecureSiteWarning.RedirectURL,
                ExcludedDomains = _configProxy.LeavingSecureSiteWarning.ExcludedDomains
            };

            HTMLHeaderElements = new List<string>();
            HTMLBodyElements = new List<string>();
            ContactLinks = new List<Link>();
            NewsLinks = new List<Link>();
            AboutLinks = new List<Link>();
            Breadcrumbs = new List<Breadcrumb>();
            SharePageMediaSites = new List<SocialMediaSites>();
            LeftMenuItems = new List<MenuSection>();

            //Set Application Template Specific Sections
            SignOutLinkURL = _configProxy.SignOutLinkURL;
            SignInLinkURL = _configProxy.SignInLinkURL;
            ShowSiteMenu = _configProxy.ShowSiteMenu;
            ShowGlobalNav = _configProxy.ShowGlobalNav;
            CustomSearch = _configProxy.CustomSearch;



        }

        public LeavingSecureSiteWarning LeavingSecureSiteWarning { get; set; }

        #region Properties

        private string _cdnPath;
        private string _staticFilesPath;

        /// <summary>
        /// property to hold the version of the template. it will be put as a comment in the html of the master pages. this will help us troubleshoot issues with clients using the template
        /// </summary>
        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Represents the list of links to override the About links in Footer
        /// Set by application programmatically
        /// </summary>
        public List<Link> AboutLinks { get; set;  }

        /// <summary>
        /// The title that will be displayed in the header above the top menu.
        /// Set programmatically.
        /// Being replaced with <see cref="GoC.WebTemplate.ApplicationTitle.Text"/>
        /// </summary>
        /// <remarks>only available for intranet themes and Application Template Themes</remarks>
        [Obsolete("Please use ApplicationTitle.Text this will dissapear in v4.0.25")]
        public string ApplicationTitle_Text
        {
            get { return ApplicationTitle.Text; }
            set { ApplicationTitle.Text = value; }
        }

        /// <summary>
        /// The url of the title that will be displayed in the header above the top menu.
        /// Set programmatically
        /// </summary>
        /// <remarks>
        /// only available for intranet themes
        /// value is optional, if no value is supplied the theme will determine the url
        /// Being replaced with <see cref="GoC.WebTemplate.ApplicationTitle.URL"/>
        /// </remarks>
        [Obsolete("Please use ApplicationTitle.URL this will dissapear in v4.0.25")]
        public string ApplicationTitle_URL
        {
            get { return ApplicationTitle.URL; }
            set { ApplicationTitle.URL = value; }
        }

        /// <summary>
        /// Represents the Application Title setting information
        /// Set Programmatically
        /// </summary>
        /// <remarks>Usable in Intranet Themes and Application Template</remarks>
        public ApplicationTitle ApplicationTitle { get; } = new ApplicationTitle();

        /// <summary>
        /// Represents the list of links for the Breadcrumbs
        /// Set by application programmatically
        /// </summary>
        public List<Breadcrumb> Breadcrumbs { get; set; }

        public List<Breadcrumb> EncodedBreadcrumbs => Breadcrumbs?.Select(b => new Breadcrumb {
            Href = HttpUtility.UrlEncode(b.Href),
            Acronym = b.Acronym,
            Title = GetStringForJson(b.Title)
        }).ToList();

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
        public string CDNEnvironment => _currentEnvironment.Env;

        /// <summary>
        /// The local path to be used during local testing or perfomance testing
        /// </summary>
        public string LocalPath => _currentEnvironment.LocalPath;

        /// <summary>
        /// URL from the cdtsEnvironments node of the web.config, for the specified environment
        /// Set by application via web.config
        /// </summary>
        public string CDNURL => _currentEnvironment.Path;

        /// <summary>
        /// Complete path of the CDN including http(s), theme and run or versioned
        /// Set by Core
        /// </summary>
        public string CDNPath
        {
            get
            {
                if (string.IsNullOrEmpty(_cdnPath))
                {
                    _cdnPath = BuildCDNPath();
                }
                return _cdnPath;
            }
        }

        /// <summary>
        /// Represents the list of links to override the Contact links in Footer
        /// Set by application programmatically
        /// </summary>
        public List<Link> ContactLinks { get; set; }

        public List<Link> EncodedContactLinks => ContactLinks?.Select(l => new Link {
            Href = HttpUtility.UrlEncode(l.Href),
            Text = GetStringForJson(l.Text)
        }).ToList();

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
        /// Being replaced with <see cref="GoC.WebTemplate.LeavingSecureSiteWarning.Enabled"/>
        /// </summary>
        [Obsolete("Please use LeavingSecureSiteWarning.Enabled this will dissapear in v4.0.25")]
        public bool LeavingSecureSiteWarning_Enabled
        {
            get { return LeavingSecureSiteWarning.Enabled; }
            set { LeavingSecureSiteWarning.Enabled = value; }
        }


        /// <summary>
        /// Determines if the popup window should be displayed with the warning message if the user navigates outside the secure session
        /// Set by application via web.config
        /// or Set by application programmatically
        /// Being replaced with <see cref="GoC.WebTemplate.LeavingSecureSiteWarning.DisplayModalWindow"/>
        /// </summary>
        [Obsolete("Please use LeavingSecureSiteWarning.DisplayModalWindow this will dissapear in v4.0.25")]
        // ReSharper disable once InconsistentNaming
        public bool LeavingSecureSiteWarning_DisplayModalWindow
        {
            get { return LeavingSecureSiteWarning.DisplayModalWindow; }
            set { LeavingSecureSiteWarning.DisplayModalWindow = value; }
        }

        /// <summary>
        /// URL to redirect to when sercuresitewarning is enabled and user clicked a link that leaves the secure session
        /// Set by application via web.config
        /// Can be set by application programmatically
        /// Being replaced with <see cref="GoC.WebTemplate.LeavingSecureSiteWarning.RedirectURL"/>
        /// </summary>
        [Obsolete("Please use LeavingSecureSiteWarning.RedirectURL this will dissapear in v4.0.25")]
        // ReSharper disable once InconsistentNaming
        public string leavingSecureSiteWarning_RedirectURL
        {
            get { return LeavingSecureSiteWarning.RedirectURL; }
            set { LeavingSecureSiteWarning.RedirectURL = value; }
        }

        /// <summary>
        /// A comma delimited list of domains that would be excluded from raising the warning
        /// Set by application via web.config
        /// Can be set by application programmatically
        /// Being replaced with <see cref="GoC.WebTemplate.LeavingSecureSiteWarning.ExcludedDomains"/>
        /// </summary>
        [Obsolete("Please use LeavingSecureSiteWarning.ExcludedDomains this will dissapear in v4.0.25")]
        // ReSharper disable once InconsistentNaming
        public string leavingSecureSiteWarning_ExcludedDomains
        {
            get { return LeavingSecureSiteWarning.ExcludedDomains; }
            set { LeavingSecureSiteWarning.ExcludedDomains = value; }
        }

        /// <summary>
        /// The warning message to be displayed to the user when clicking a link that leaves the secure session
        /// Set by application programmatically
        /// Being replaced with <see cref="GoC.WebTemplate.LeavingSecureSiteWarning.Message"/>
        /// </summary>
        [Obsolete("Please use LeavingSecureSiteWarning.Message this will dissapear in v4.0.25")]
        public string LeavingSecureSiteWarning_Message
        {
            get { return LeavingSecureSiteWarning.Message; }
            set { LeavingSecureSiteWarning.Message = value; }
        }

        /// <summary>
        /// URL to be used for the feedback link
        /// Set by application via web.config
        /// or programmatically
        /// Being replaced with <see cref="FeedbackLinkURL"/>
        /// </summary>
        [Obsolete("Please use FeedbackLink.URL this will dissapear in v4.0.25")]
        public string FeedbackLink_URL
        {
            get { return FeedbackLinkURL; }
            set { FeedbackLinkURL = value; }
        }

        /// <summary>
        /// URL to be used for the feedback link
        /// Set by application via web.config
        /// or programmatically
        /// </summary>
        public string FeedbackLinkURL { get; set; }

        /// <summary>
        /// URL to be used for the Privacy link in transactional mode
        /// Set by application programmatically
        /// Being replaced with <see cref="PrivacyLinkURL"/>
        /// </summary>
        [Obsolete("Please use PrivacyLink.URL this will dissapear in v4.0.25")]
        // ReSharper disable once InconsistentNaming
        public string PrivacyLink_URL
        {
            get { return PrivacyLinkURL; }
            set { PrivacyLinkURL = value; }
        }

        /// <summary>
        /// URL to be used for the Privacy link in transactional mode
        /// Set by application programmatically
        /// </summary>
        public string PrivacyLinkURL { get; set; }

        /// <summary>
        /// URL to be used for the Terms & Conditions link in transactional mode
        /// Set by application programmatically
        /// Being replaced with <see cref="TermsConditionsLinkURL"/>
        /// </summary>
        [Obsolete("Please use TermsConditionsLink.URL this will dissapear in v4.0.25")]
        // ReSharper disable once InconsistentNaming
        public string TermsConditionsLink_URL
        {
            get { return TermsConditionsLinkURL; }
            set { TermsConditionsLinkURL = value; }
        }

        /// <summary>
        /// URL to be used for the Terms & Conditions link in transactional mode
        /// Set by application programmatically
        /// </summary>
        public string TermsConditionsLinkURL { get; set; }

        public LanguageLink LanguageLink { get; set; }

        /// <summary>
        /// URL to be used for the language toggle
        /// Set/built by Template
        /// Can be set by application programmatically
        /// Being replaced with <see cref="GoC.WebTemplate.LanguageLink.URL"/>
        /// </summary>
        [Obsolete("Please use LanguageLink.URL this will dissapear in v4.0.25")]
        public string LanguageLink_URL
        {
            get { return LanguageLink.Href; }
            set { LanguageLink.Href = value; }
        }

        /// <summary>
        /// Read only property, used to populate the Lang attribute of the language toggle link
        /// Value is defaulted by Template
        /// Being replaced with <see cref="GoC.WebTemplate.LanguageLink.Lang"/>
        /// </summary>
        [Obsolete("Please use LanguageLink.Lang this will dissapear in v4.0.25")]
        public string LanguageLink_Lang => LanguageLink.Lang;

        /// <summary>
        /// Read only property, used to populate the text attribute of the language toggle link
        /// Value is defaulted by Template
        /// Being replaced with <see cref="GoC.WebTemplate.LanguageLink.Text"/>
        /// </summary>
        [Obsolete("Please use LanguageLink.Text this will dissapear in v4.0.25")]
        public string LanguageLink_Text => LanguageLink.Text;

        // ReSharper restore InconsistentNaming
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
                return _staticFilesPath ??
                       (_staticFilesPath = string.Concat(_configProxy.StaticFilesLocation, "/", WebTemplateTheme));
            }
            set { _staticFilesPath = value; }
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
        public string TwoLetterCultureLanguage => Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

        private string _headerTitle;
        /// <summary>
        /// title of page, will automatically add '- Canada.ca' to all pages implementing GCWeb theme as per 
        /// Set by application programmatically
        /// </summary>
        public string HeaderTitle
        {
            get
            {
                if (WebTemplateTheme.Equals("gcweb", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(_headerTitle))
                    {
                        return "- Canada.ca";
                    }
                    if (_headerTitle.EndsWith(" - Canada.ca"))
                    {
                        return _headerTitle;
                    }
                    return _headerTitle + " - Canada.ca";
                }
                return _headerTitle;
            }
            set
            {
                _headerTitle = value;
            }
        }

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



        /// <summary>
        /// Allows for a custom search to be used in the application, you must contact CDTS to have one created.
        /// </summary>
        public string CustomSearch { get; set; }

        /// <summary>
        /// Determines if the Global Nav bar in the footer is to be displayed.
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public bool ShowGlobalNav { get; set; }

        /// <summary>
        /// Determines if the Site Menu is to appear at the top of the page. 
        /// If set to false only a blue band will be seen.
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public bool ShowSiteMenu { get; set; }

        /// <summary>
        /// A custom site menu to be used in place of the standard canada.ca site menu
        /// This defaults to null (use standard menu)
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public string CustomSiteMenuURL { get; set; }

        /// <summary>
        /// The link to use for the sign in button, will only appear if <see cref="ShowSignInLink"/> is set to true
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public string SignInLinkURL { get; set; }

        /// <summary>
        /// The link to use for the sign out button, will only appear if <see cref="ShowSignOutLink"/> is set to true
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public string SignOutLinkURL { get; set; }


        /// <summary>
        /// Displays the secure icon next to the applicaiton name in the header.
        /// Set by application programmatically
        /// Only available in the Application Template
        /// </summary>
        public bool ShowSecure { get; set; }

        /// <summary>
        /// Displays the sign in link set.
        /// <see cref="SignInLinkURL"/> must not be null or whitespace
        /// <see cref="ShowSignOutLink"/> must not be set at the same time.
        /// Set by application programmatically
        /// Only available in the Application Template
        /// </summary>
        public bool ShowSignInLink { get; set; }

        /// <summary>
        /// Displays the signout link set.
        /// <see cref="SignOutLinkURL"/> must not be null or whitespace
        /// <see cref="ShowSignInLink"/> must not be set at the same time.
        /// Set by application programmatically
        /// Only available in the Application Template
        /// </summary>
        public bool ShowSignOutLink { get; set; }

        /// <summary>
        /// Custom links if null uses standard links if not null overrides the existing footer links
        /// Set by application programmatically
        /// Only available in the Application Template
        /// </summary>
        public List<FooterLink> CustomFooterLinks { get; set; }
        
        public List<FooterLink> EncodedCustomFooterLinks => CustomFooterLinks?.Select(fl => new FooterLink
        {
            Href = HttpUtility.UrlEncode(fl.Href),
            NewWindow = fl.NewWindow,
            Text = GetStringForJson(fl.Text)
        }).ToList();


        #endregion




        private string GetStringForJson(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }

        private string GetFormattedJsonString(string formatStr, params object[] strs)
        {
            return string.IsNullOrWhiteSpace(formatStr) ? null : string.Format(formatStr, strs);
        }

        private List<Link> BuildHideableHrefOnlyLink(string href, bool showLink)
        {

            if (!showLink || string.IsNullOrWhiteSpace(href))
            {
                return null;
            }
            return new List<Link> {new Link {Href = HttpUtility.UrlEncode(href), Text = null}};
        }

        private void CheckIfBothSignInAndSignOutAreSet()
        {
            if (ShowSignInLink && ShowSignOutLink)
            {
                throw new InvalidOperationException("Unable to show sign in and sign out link together");
            }
        }

        #region Renderers

        public HtmlString RenderHeaderTitle() => new HtmlString(HeaderTitle);

        public HtmlString RenderAppFooter()
        {

            var appFooter = new AppFooter
            {
                CdnEnv = CDNEnvironment,
                SubTheme = GetStringForJson(WebTemplateSubTheme),
                ShowFeatures = ShowFeatures,
                TermsLink = HttpUtility.UrlEncode(GetStringForJson(TermsConditionsLinkURL)),
                PrivacyLink = HttpUtility.UrlEncode(GetStringForJson(PrivacyLinkURL)),
                ContactLinks = EncodedContactLinks,
                LocalPath = HttpUtility.UrlEncode(GetFormattedJsonString(LocalPath, WebTemplateTheme, WebTemplateVersion)),
                GlobalNav = ShowGlobalNav,
                FooterSections = EncodedCustomFooterLinks
            };

            return new HtmlString(JsonConvert.SerializeObject(appFooter, _settings));
        }


        public HtmlString RenderAppTop()
        {
            CheckIfBothSignInAndSignOutAreSet();

            var appTop = new AppTop
            {
                AppName = ApplicationTitle.Text,
                SignIn = BuildHideableHrefOnlyLink(SignInLinkURL, ShowSignInLink),
                SignOut = BuildHideableHrefOnlyLink(SignOutLinkURL, ShowSignOutLink),
                Secure = ShowSecure,
                CdnEnv = CDNEnvironment,
                SubTheme = WebTemplateSubTheme,
                Search = ShowSearch,
                LngLinks = BuildLanguageLinkList(),
                ShowPreContent = ShowPreContent,
                Breadcrumbs = EncodedBreadcrumbs,
                LocalPath = HttpUtility.UrlEncode(GetFormattedJsonString(LocalPath, WebTemplateTheme, WebTemplateVersion)),
                SiteMenu = ShowSiteMenu,
                MenuPath = HttpUtility.UrlEncode(CustomSiteMenuURL)
            };

            return new HtmlString(JsonConvert.SerializeObject(appTop, _settings));
        }



        private List<LanguageLink> BuildLanguageLinkList()
        {
            if (!ShowLanguageLink)
            {
                return null;
            }

            return new List<LanguageLink> {
                new LanguageLink {
                    Href = HttpUtility.UrlEncode(LanguageLink.Href)
                }
            };
        }

        public HtmlString RenderFooterLinks(bool transactionalMode)
        {
            StringBuilder sb = new StringBuilder();

            if (transactionalMode)
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

            if (!string.IsNullOrEmpty(ApplicationTitle.Text))
            {
                //intranetTitle: [{href: "http://hrsdc.prv/eng/iit/index.shtml", text: "IITB"}],
                sb.Append("intranetTitle: [{");
                if (!string.IsNullOrEmpty(ApplicationTitle.URL))
                {
                    sb.Append(string.Concat(" href: \"", ApplicationTitle.URL, "\","));
                }
                sb.Append(string.Concat(" text: \"", ApplicationTitle.Text, "\""));
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
                if (!string.IsNullOrEmpty(FeedbackLinkURL))
                {
                    feedbackJSon = string.Concat("showFeedback: \"", FeedbackLinkURL, "\",");
                }
            }
            else
            {
                feedbackJSon = "showFeedback: false,";
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
            return !string.IsNullOrEmpty(ScreenIdentifier)
                ? new HtmlString(string.Concat("screenIdentifier: \"", ScreenIdentifier, "\","))
                : null;
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
            return ShowSearch
                ? "search: true,"
                : "search: false,";
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
            return LoadJQueryFromGoogle
                ? new HtmlString("jqueryEnv: \"external\",")
                : null;
        }

        public HtmlString RenderLocalPath()
        {
            if (string.IsNullOrWhiteSpace(LocalPath))
            {
                return null;
            }
            return new HtmlString("localPath: '" + String.Format(LocalPath, WebTemplateTheme, WebTemplateVersion) + "'");
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

            if (this.SessionTimeout.Enabled)
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
            return ShowFeatures
                ? "showFeatures: true,"
                : "showFeatures: false,";
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
                sb.Append(LanguageLink.Lang);
                sb.Append("',");

                sb.Append(" href: '");
                sb.Append(LanguageLink.Href);
                sb.Append("',");

                sb.Append(" text: '");
                sb.Append(LanguageLink.Text);
                sb.Append("'");
                sb.Append("}],");
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to represent the left side menu
        /// </summary>
        // ReSharper restore InconsistentNaming
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
                        if (menuSection.OpenInNewWindow)
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
                                MenuItem mi = (MenuItem) lk;

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
                                        if (sublk.OpenInNewWindow)
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

            if (LeavingSecureSiteWarning.Enabled && !string.IsNullOrEmpty(LeavingSecureSiteWarning.RedirectURL))
            {
                sb.Append("exitScript: true,");
                if (LeavingSecureSiteWarning.DisplayModalWindow == false)
                {
                    sb.Append(" displayModal: false,");
                }
                sb.Append(" exitURL: \"");
                sb.Append(LeavingSecureSiteWarning.RedirectURL);
                sb.Append("\",");
                sb.Append(" exitMsg: \"");
                sb.Append(WebUtility.HtmlEncode(LeavingSecureSiteWarning.Message));
                sb.Append("\",");
                if (!string.IsNullOrEmpty(LeavingSecureSiteWarning.ExcludedDomains))
                {
                    sb.Append("exitDomains: \"");
                    sb.Append(LeavingSecureSiteWarning.ExcludedDomains);
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

            if (DateTime.Compare(this.DateModified, DateTime.MinValue) == 0 &&
                !string.IsNullOrEmpty(this.VersionIdentifier))
            {
                sb.Append("versionIdentifier: \"");
                sb.Append(this.VersionIdentifier.Trim());
                sb.Append("\",");
            }
            else
            {
                sb.Append("dateModified: \"");
                sb.Append(this.DateModified.ToString("yyyy-MM-dd"));
                    // format for english and french are identical, else would need if.
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
            if (!string.IsNullOrEmpty(TermsConditionsLinkURL))
            {
                sb.Append("termsLink: \"" + TermsConditionsLinkURL + "\",");
            }
        }

        /// <summary>
        /// Builds a string with the format required by the closure templates, to manage the privacy notice link in transaction mode only
        /// </summary>
        /// <returns>string in the format expected by the Closure Templates to manage the privacy notice link in transaction mode only</returns>
        private void RenderPrivacyLink(StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(TermsConditionsLinkURL))
            {
                sb.Append("privacyLink: \"" + PrivacyLinkURL + "\",");
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
                    _cdnPath = string.Format(CultureInfo.InvariantCulture, CDNURL, https, WebTemplateTheme, "rn");
                }
                else
                {
                    _cdnPath = string.Format(CultureInfo.InvariantCulture, CDNURL, https, WebTemplateTheme,
                        WebTemplateVersion);
                }
            }
            else //Using ESDC CDTS (SSL)
            {
                if (string.IsNullOrEmpty(WebTemplateVersion))
                {
                    _cdnPath = string.Format(CultureInfo.InvariantCulture, CDNURL, https, "rn", WebTemplateTheme,
                        WebTemplateVersion);
                }
                else
                {
                    //the extra "/" at the end is added after the version, since this adds a folder to the original path
                    _cdnPath = string.Format(CultureInfo.InvariantCulture, CDNURL, https, "app", WebTemplateTheme,
                        string.Concat(WebTemplateVersion, "/"));
                }
            }
            return _cdnPath;
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
            {
                nameValues.Set(Constants.QUERYSTRING_CULTURE_KEY, Constants.FRENCH_CULTURE);
            }
            else
            {
                nameValues.Set(Constants.QUERYSTRING_CULTURE_KEY, Constants.ENGLISH_CULTURE);
            }

            string url = string.Concat("?", nameValues.ToString());

            return url;
        }

        /// <summary>
        /// Arbritrary object to act as a mutex to obtain a class-scope lock accros all threads.
        /// </summary>
        /// <remarks></remarks>
        private static readonly object lockObject = new object();

        private readonly JsonSerializerSettings _settings;
        private readonly ICDTSEnvironmentElementProxy _currentEnvironment;

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
            info = (string) HttpRuntime.Cache.Get(cacheKey);
            if ((info != null))
            {
                // Object was found in cache, simply return it and get out!
                return new HtmlString(info);
            }

            // ---[ If we get here, the object was not found in the cache, we'll have to load it.
            lock (lockObject)
            {
                //---[ Attempt to get from cache again now that we are locked
                info = (string) HttpRuntime.Cache.Get(cacheKey);
                if ((info != null))
                {
                    // Once again, object was found in cache, simply return it and get out!
                    return new HtmlString(info);
                }

                //---[ If we get here, we really have to load the data
                if (StaticFilesPath.StartsWith("~"))
                {
                    filePath = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(StaticFilesPath),
                        fileName);
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

                    HttpRuntime.Cache.Insert(cacheKey, info, cacheDep, Cache.NoAbsoluteExpiration,
                        Cache.NoSlidingExpiration);
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

