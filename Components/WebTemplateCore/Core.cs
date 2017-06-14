using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Text;
using System.Net;
using System.Web;
using System.Linq;
using GoC.WebTemplate.Proxies;
using Newtonsoft.Json;
using WebTemplateCore.JSONSerializationObjects;
using WebTemplateCore.Proxies;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{
    public class Core
    {

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

        private readonly ICacheProxy _cacheProxy;
        private readonly IConfigurationProxy _configProxy;
        private readonly IDictionary<string,ICDTSEnvironment> _cdtsEnvironments;

        public HtmlString RenderTransactionalTop()
        {
            return JsonSerializationHelper.SerializeToJson(new Top
            {

                CdnEnv = CDNEnvironment,
                SubTheme = WebTemplateSubTheme,
                IntranetTitle = new List<ApplicationTitle> {ApplicationTitle},
                Search = ShowSearch,
                LngLinks = BuildLanguageLinkList(),
                SiteMenu = false,
                Breadcrumbs = BuildBreadcrumbs(),
                ShowPreContent = false,
                LocalPath = BuildLocalPath()

            });
        }
        public HtmlString RenderTop()
        {
            return JsonSerializationHelper.SerializeToJson(new Top
            {
                CdnEnv = CDNEnvironment,
                SubTheme = WebTemplateSubTheme,
                IntranetTitle = new List<ApplicationTitle> {ApplicationTitle},
                Search = ShowSearch,
                LngLinks = BuildLanguageLinkList(),
                SiteMenu = true,
                ShowPreContent = ShowPreContent,
                Breadcrumbs = BuildBreadcrumbs(),
                LocalPath = BuildLocalPath()
            });
        }

        public HtmlString RenderRefTop()
        {
            return JsonSerializationHelper.SerializeToJson(new RefTop
            {
                CdnEnv = CDNEnvironment,
                SubTheme = WebTemplateSubTheme,
                JqueryEnv =  LoadJQueryFromGoogle ? "external" : null,
                LocalPath = BuildLocalPath()
            });
        }

        private string BuildLocalPath()
        {
            return GetFormattedJsonString(LocalPath, WebTemplateTheme,WebTemplateVersion);
        }

        #region Enums

        /// <summary>
        /// Enum that represents the context of the list of links
        /// </summary>
        /// <remarks>The enum item name must match the parameter name expected by the Closure Template for the template to work.  The enum item name is used by the template as the paramter name when calling the Closure Template.</remarks>
        // ReSharper disable InconsistentNaming
        public enum LinkTypes
        {
            contactLinks
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

        // ReSharper restore InconsistentNaming

        #endregion


        public Core(ICurrentRequestProxy currentRequest,
            ICacheProxy cacheProxy,
            IConfigurationProxy configProxy,
            IDictionary<string,ICDTSEnvironment> cdtsEnvironments)
        {
            _cacheProxy = cacheProxy;
            _configProxy = configProxy;
            _cdtsEnvironments = cdtsEnvironments;


            //Set properties
            WebTemplateVersion = _configProxy.Version;
            WebTemplateTheme = _configProxy.Theme;
            WebTemplateSubTheme = _configProxy.SubTheme;

            UseHTTPS = _configProxy.UseHttps;
            Environment = _configProxy.Environment;
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

            //Set Application Template Specific Sections
            SignOutLinkURL = _configProxy.SignOutLinkURL;
            SignInLinkURL = _configProxy.SignInLinkURL;
            ShowSiteMenu = _configProxy.ShowSiteMenu;
            ShowGlobalNav = _configProxy.ShowGlobalNav;
            CustomSearch = _configProxy.CustomSearch;

        }

        public LeavingSecureSiteWarning LeavingSecureSiteWarning { get; set; }

        private string _staticFilesPath;

        /// <summary>
        /// property to hold the version of the template. it will be put as a comment in the html of the master pages. this will help us troubleshoot issues with clients using the template
        /// </summary>
        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

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
        public List<Breadcrumb> Breadcrumbs { get; set; } = new List<Breadcrumb>();

        public List<Breadcrumb> BuildBreadcrumbs()
        {
            if (Breadcrumbs == null || !Breadcrumbs.Any())
            {
                return null;
            }

            return Breadcrumbs.Select(b => new Breadcrumb
            {
                Href = b.Href,
                Acronym = b.Acronym,
                Title = GetStringForJson(b.Title)
            }).ToList();
        }

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
         public string CDNEnvironment => _cdtsEnvironments[Environment].CDN;

        /// <summary>
        /// The local path to be used during local testing or perfomance testing
        /// Set by application via web.config
        /// </summary>
        public string LocalPath => _cdtsEnvironments[Environment].LocalPath;

        /// <summary>
        /// URL from the cdtsEnvironments node of the web.config, for the specified environment
        /// Set by application via web.config
        /// </summary>
        public string CDNURL => _cdtsEnvironments[Environment].Path;

        /// <summary>
        /// Complete path of the CDN including http(s), theme and run or versioned
        /// Set by Core
        /// </summary>
        public string CDNPath => BuildCDNPath();

        /// <summary>
        /// Used to override the Contact links in Footer
        /// Set by application programmatically
        /// </summary>
        public string ContactLinkURL { get; set; }

        private List<Link> BuildContactLinks() => new List<Link> {new Link {Href = ContactLinkURL}};


        /// <summary>
        /// Represents the list of html elements to add to the header tag
        /// will be used to add metatags, css, js etc.
        /// Set by application programmatically
        /// </summary>
        public List<string> HTMLHeaderElements { get; set; } = new List<string>();

        /// <summary>
        /// Represents the list of html elements to add at the end of the body tag
        /// will be used to add metatags, css, js etc.
        /// Set by application programmatically
        /// </summary>
        public List<string> HTMLBodyElements { get; set; } = new List<string>();

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
        /// URL to be used for the feedback link
        /// Set by application via web.config
        /// or programmatically
        /// </summary>
        public string FeedbackLinkURL { get; set; }

        /// <summary>
        /// URL to be used for the Privacy link in transactional mode
        /// Set by application programmatically
        /// </summary>
        public string PrivacyLinkURL { get; set; }

        /// <summary>
        /// URL to be used for the Terms & Conditions link in transactional mode
        /// Set by application programmatically
        /// </summary>
        public string TermsConditionsLinkURL { get; set; }

        /// <summary>
        /// Used to override the langauge link
        /// </summary>
        public LanguageLink LanguageLink { get; set; }

        // ReSharper restore InconsistentNaming
        /// <summary>
        /// Represents a list of menu items
        /// </summary>
        public List<MenuSection> LeftMenuItems { get; set; } = new List<MenuSection>();

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
        public List<SocialMediaSites> SharePageMediaSites { get; set; } = new List<SocialMediaSites>();

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
        public bool? UseHTTPS { get; set; }

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

        public List<FooterLink> BuildCustomFooterLinks
        {
            get
            {
                return CustomFooterLinks?.Select(fl => new FooterLink
                {
                    Href = fl.Href,
                    NewWindow = fl.NewWindow,
                    Text = GetStringForJson(fl.Text)
                }).ToList();
            }
        }


        private string GetStringForJson(string str) => string.IsNullOrWhiteSpace(str) ? null : str;

        private string GetFormattedJsonString(string formatStr, params object[] strs) => string.IsNullOrWhiteSpace(formatStr) ? null : string.Format(formatStr, strs);

        private List<Link> BuildHideableHrefOnlyLink(string href, bool showLink)
        {

            if (!showLink || string.IsNullOrWhiteSpace(href))
            {
                return null;
            }
            return new List<Link> {new Link {Href = href, Text = null}};
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
            return JsonSerializationHelper.SerializeToJson(new AppFooter
            {
                CdnEnv = CDNEnvironment,
                SubTheme = GetStringForJson(WebTemplateSubTheme),
                ShowFeatures = ShowFeatures,
                TermsLink = GetStringForJson(TermsConditionsLinkURL),
                PrivacyLink = GetStringForJson(PrivacyLinkURL),
                ContactLinks = BuildContactLinks(),
                LocalPath = GetFormattedJsonString(LocalPath, WebTemplateTheme, WebTemplateVersion),
                GlobalNav = ShowGlobalNav,
                FooterSections = BuildCustomFooterLinks
            });
        }


        public HtmlString RenderAppTop()
        {
            CheckIfBothSignInAndSignOutAreSet();

            return JsonSerializationHelper.SerializeToJson(new AppTop
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
                Breadcrumbs = BuildBreadcrumbs(),
                LocalPath = GetFormattedJsonString(LocalPath, WebTemplateTheme, WebTemplateVersion),
                SiteMenu = ShowSiteMenu,
                MenuPath = CustomSiteMenuURL,
                CustomSearch = CustomSearch
            });
        }



        private List<LanguageLink> BuildLanguageLinkList()
        {
            if (!ShowLanguageLink)
            {
                return null;
            }

            return new List<LanguageLink> {
                new LanguageLink {
                    Href = LanguageLink.Href
                }
            };
        }

        public HtmlString RenderFooterLinks(bool transactionalMode)
        {
            StringBuilder sb = new StringBuilder();

            if (transactionalMode)
            {
                //contact, terms, privacy links
                RenderLinksList(sb, LinkTypes.contactLinks, BuildContactLinks());
                RenderTermsConditionsLink(sb);
                RenderPrivacyLink(sb);
            }
            else
            {
                //contact, news, about links
                RenderLinksList(sb, LinkTypes.contactLinks, BuildContactLinks());
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

            if (ShowSharePageLink && (SharePageMediaSites.Count > 0))
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

            if (SessionTimeout.Enabled)
            {
                sb.Append("<span class='wb-sessto' data-wb-sessto='{");
                sb.Append("\"inactivity\": ");
                sb.Append(SessionTimeout.Inactivity);
                sb.Append(", \"reactionTime\": ");
                sb.Append(SessionTimeout.ReactionTime);
                sb.Append(", \"sessionalive\": ");
                sb.Append(SessionTimeout.SessionAlive);
                sb.Append(", \"logouturl\": \"");
                sb.Append(SessionTimeout.LogoutUrl);
                sb.Append("\"");
                if (!string.IsNullOrEmpty(SessionTimeout.RefreshCallbackUrl))
                {
                    sb.Append(", \"refreshCallbackUrl\": \"");
                    sb.Append(SessionTimeout.RefreshCallbackUrl);
                    sb.Append("\"");
                }
                sb.Append(", \"refreshOnClick\": ");
                sb.Append(SessionTimeout.RefreshOnClick.ToString().ToLower());

                if (SessionTimeout.RefreshLimit > 0)
                {
                    sb.Append(", \"refreshLimit\": ");
                    sb.Append(SessionTimeout.RefreshLimit);
                }
                if (!string.IsNullOrEmpty(SessionTimeout.Method))
                {
                    sb.Append(", \"method\": \"");
                    sb.Append(SessionTimeout.Method);
                    sb.Append("\"");
                }
                if (!string.IsNullOrEmpty(SessionTimeout.AdditionalData))
                {
                    sb.Append(", \"additionalData\": \"");
                    sb.Append(SessionTimeout.AdditionalData);
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

            if (LeftMenuItems.Count > 0)
            {
                sb.Append("sections: [");
                foreach (MenuSection menuSection in LeftMenuItems)
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
                                if (mi.OpenInNewWindow)
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
            return RenderHtmlElements(HTMLHeaderElements);
        }

        public HtmlString RenderHtmlBodyElements()
        {
            return RenderHtmlElements(HTMLBodyElements);
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

            if (DateTime.Compare(DateModified, DateTime.MinValue) == 0 &&
                !string.IsNullOrEmpty(VersionIdentifier))
            {
                sb.Append("versionIdentifier: \"");
                sb.Append(VersionIdentifier.Trim());
                sb.Append("\",");
            }
            else
            {
                sb.Append("dateModified: \"");
                sb.Append(DateModified.ToString("yyyy-MM-dd"));
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

            var currentEnv = _cdtsEnvironments[Environment];


            if (!currentEnv.IsThemeModifiable && !string.IsNullOrWhiteSpace(WebTemplateTheme))
            {
                throw new InvalidOperationException($"{Environment} does not allow a theme to be set");
            }

            if (currentEnv.IsThemeModifiable && string.IsNullOrWhiteSpace(WebTemplateTheme))
            {
                throw new InvalidOperationException($"{Environment} requires a theme to be set");
            }

            if (!currentEnv.IsSSLModifiable && UseHTTPS.HasValue)
            {
                throw new InvalidOperationException($"{Environment} does not allow useHTTPS to be toggled");
            }

            if (currentEnv.IsSSLModifiable && !UseHTTPS.HasValue)
            {
                throw new InvalidOperationException($"{Environment} requires useHTTPS to be true or false not null.");
            }

            var https = string.Empty;
            if (currentEnv.IsSSLModifiable)
            {
                https = UseHTTPS.Value ? "s" : string.Empty;
            }

            var theme = string.Empty;
            if (currentEnv.IsThemeModifiable)
            {
                theme = WebTemplateTheme;
            }

            var run = string.Empty;
            var version = string.Empty;
            if (string.IsNullOrWhiteSpace(WebTemplateVersion))
            {
                if (currentEnv.IsVersionRNCombined)
                {
                    version = "rn/";
                }
                else
                {
                    run = "rn";
                }
            }
            else
            {
                version = WebTemplateVersion + "/";
                run = "app";
            }


            return string.Format(CultureInfo.InvariantCulture, currentEnv.Path, https, run, theme, version);
        }

        /// <summary>
        /// Builds the URL to be used by the English/francais link at the top of the page for the language toggle.
        /// The method will add or update the "GoCTemplateCulture" querystring parameter with the culture to be set
        /// The language toggle link, posts back to the same page, and the InitializedCulture method of the BasePage is responsible for setting the culture with the provided value
        /// </summary>
        /// <returns>The URL to be used for the language toggle link</returns>
        private string BuildLanguageLinkURL(string queryString)
        {
            System.Collections.Specialized.NameValueCollection nameValues = HttpUtility.ParseQueryString(queryString);

            //Set the value of the "GoCTemplateCulture" parameter
            if (TwoLetterCultureLanguage.StartsWith(Constants.ENGLISH_ACCRONYM, StringComparison.OrdinalIgnoreCase))
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

        /// <summary>
        /// This method is used to get the static file content from the cache. if the cache is empty it will read the content from the file and load it into the cache.
        /// </summary>
        /// <param name="fileName">static file name to retreive</param>
        /// <returns>A string containing the content of the file.</returns>
        public HtmlString LoadStaticFile(string fileName)
        {
            var cacheKey = string.Concat(Constants.CACHE_KEY_STATICFILES_PREFIX, ".", fileName);

            // Attempt to lookup from cache
            Debug.Assert(_cacheProxy != null, "Cache proxy cannot be null");
            var info = _cacheProxy.GetFromCache<string>(cacheKey);
            if (info != null)
            {
                // Object was found in cache, simply return it and get out!
                return new HtmlString(info);
            }

            // ---[ If we get here, the object was not found in the cache, we'll have to load it.
            lock (lockObject)
            {
                //---[ Attempt to get from cache again now that we are locked
                info = _cacheProxy.GetFromCache<string>(cacheKey);
                if (info != null)
                {
                    // Once again, object was found in cache, simply return it and get out!
                    return new HtmlString(info);
                }

                //---[ If we get here, we really have to load the data
                string filePath;
                if (StaticFilesPath.StartsWith("~"))
                {
                    filePath = Path.Combine(HttpContext.Current.Server.MapPath(StaticFilesPath),
                        fileName);
                }
                else
                {
                    filePath = Path.Combine(StaticFilesPath, fileName);
                }

                try
                {
                    info = File.ReadAllText(filePath);
                    //---[ Now that the data is loaded, add it to the cache
                    _cacheProxy.SaveToCache(cacheKey, filePath, info);
                }
                catch (DirectoryNotFoundException)
                {
                    info = string.Empty;
                }
                catch (FileNotFoundException)
                {
                    info = string.Empty;
                }
            }
            return new HtmlString(info);
        }


    }

    public class CDTSEnvironmentLoader
    {
        private readonly ICacheProxy _cacheProxy;

        public CDTSEnvironmentLoader(ICacheProxy cacheProxy)
        {
            _cacheProxy = cacheProxy;
        }
        private static readonly object EnvironmentsLockObject = new object();
        /// <summary>
        /// Loads the CDTSEnvironments either from file or from the HTTPruntime.Cache 
        /// </summary>
        /// <param name="filename">The filename to use, we are using CDTSEnvironments.json</param>
        /// <returns>A dictionary of environments with the ICDTSEnvironment.Name being the key.</returns>
        public IDictionary<string, ICDTSEnvironment> LoadCDTSEnvironments(string filename)
        {
            Debug.Assert(_cacheProxy != null, "CacheProxy Cannot be null");
            var environments = _cacheProxy.GetFromCache<IDictionary<string,ICDTSEnvironment>>(Constants.CACHE_KEY_ENVIRONMENTS);
            if (environments != null)
            {
                return environments;
            }

            lock (EnvironmentsLockObject)
            {
                environments = _cacheProxy.GetFromCache<IDictionary<string,ICDTSEnvironment>>(Constants.CACHE_KEY_ENVIRONMENTS);
                if (environments != null)
                {
                    return environments;
                }

                //If the path is relative we need to map it.
                if (filename.StartsWith("~"))
                {
                    //We might want to decouple this.
                    filename = HttpContext.Current.Server.MapPath(filename);
                }

                //We don't catch exceptions because this file needs to exist. 
                //So we want the app to crash if it isn't.
                environments = JsonSerializationHelper.DeserializeEnvironments(filename);
                _cacheProxy.SaveToCache(Constants.CACHE_KEY_ENVIRONMENTS,filename, environments);
            }
            return environments;
        }
        
    }
}

