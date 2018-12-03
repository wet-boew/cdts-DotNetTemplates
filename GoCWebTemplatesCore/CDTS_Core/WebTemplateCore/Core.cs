using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using CDTS_Core.WebTemplate;
using CDTS_Core.WebTemplateCore.JsonSerializationObjects;
using CDTS_Core.WebTemplateCore.Proxies;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CDTS_Core.WebTemplateCore
{
    public class Core
    {
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
        }

        private readonly ICacheProxy _cacheProxy;

        private readonly GCConfigurations _config;

        private readonly IDictionary<string, ICDTSEnvironment> _cdtsEnvironments;

        private CoreBuilder _builder;

        private CoreRenderer _renderer;

        private string _staticFilesPath;

        private string _headerTitle;

        private static readonly object LockObject = new object();

        internal CoreBuilder Builder => _builder ?? (_builder = new CoreBuilder(this));

        private CoreRenderer Render => _renderer ?? (_renderer = new CoreRenderer(this));

        public LeavingSecureSiteWarning LeavingSecureSiteWarning
        {
            get;
            set;
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public Link ApplicationTitle
        {
            get;
        } = new Link();


        public List<Breadcrumb> Breadcrumbs
        {
            get;
            set;
        } = new List<Breadcrumb>();


        public string Environment
        {
            get;
            set;
        }

        public ICDTSEnvironment CurrentEnvironment => _cdtsEnvironments[Environment];

        public string CDNEnvironment => CurrentEnvironment.CDN;

        public string LocalPath => CurrentEnvironment.LocalPath;

        public string CDNURL => CurrentEnvironment.Path;

        public string WebTemplateTheme => CurrentEnvironment.Theme;

        public string CDNPath => Builder.BuildCDNPath();

        public string AppendToTitle => CurrentEnvironment.AppendToTitle;

        /// <summary>        
        /// Used to override the Contact link in Footer, AppFooter and TransacationalFooter        
        /// Set by application programmatically        
        /// </summary>        
        public List<Link> ContactLinks { get; set; } = new List<Link>();

        /// <summary>        
        /// Info for Spash page        
        /// Only applicable to Splash Layout/Master        
        /// </summary>        
        public SplashPageInfo SplashPageInfo { get; set; } = new SplashPageInfo();

        public List<string> HTMLHeaderElements
        {
            get;
            set;
        } = new List<string>();


        public List<string> HTMLBodyElements
        {
            get;
            set;
        } = new List<string>();


        public DateTime DateModified
        {
            get;
            set;
        }

        public bool ShowFeatures
        {
            get;
            set;
        }

        public string FeedbackLinkURL
        {
            get;
            set;
        }

        public string PrivacyLinkURL
        {
            get;
            set;
        }

        public string TermsConditionsLinkURL
        {
            get;
            set;
        }

        public LanguageLink LanguageLink
        {
            get;
            set;
        }

        public List<MenuSection> LeftMenuItems
        {
            get;
            set;
        } = new List<MenuSection>();


        public string ScreenIdentifier
        {
            get;
            set;
        }

        public SessionTimeout SessionTimeout
        {
            get;
            set;
        }

        public bool ShowPostContent
        {
            get;
            set;
        }

        public bool ShowFeedbackLink
        {
            get;
            set;
        }

        public bool ShowLanguageLink
        {
            get;
            set;
        }

        public bool ShowSharePageLink
        {
            get;
            set;
        }

        public bool ShowSearch
        {
            get;
            set;
        }

        public List<SocialMediaSites> SharePageMediaSites
        {
            get;
            set;
        } = new List<SocialMediaSites>();


        public string StaticFilesPath
        {
            get
            {
                return _staticFilesPath ?? (_staticFilesPath = _config.StaticFilesLocation + "/" + WebTemplateTheme);
            }
            set
            {
                _staticFilesPath = value;
            }
        }

        public bool ShowPreContent
        {
            get;
            set;
        }

        public string TwoLetterCultureLanguage => Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

        public string HeaderTitle
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AppendToTitle))
                {
                    return _headerTitle;
                }
                if (string.IsNullOrWhiteSpace(_headerTitle))
                {
                    return AppendToTitle;
                }
                if (_headerTitle.EndsWith(AppendToTitle))
                {
                    return _headerTitle;
                }
                return _headerTitle + AppendToTitle;
            }
            set
            {
                _headerTitle = value;
            }
        }

        public string VersionIdentifier
        {
            get;
            set;
        }

        public string WebTemplateVersion
        {
            get;
            set;
        }

        public string WebTemplateSubTheme => _cdtsEnvironments[Environment].SubTheme;

        public bool? UseHTTPS
        {
            get;
            set;
        }

        public bool LoadJQueryFromGoogle
        {
            get;
            set;
        }

        public string CustomSearch
        {
            get;
            set;
        }

        public string CustomSiteMenuURL
        {
            get;
            set;
        }

        public string AppSettingsURL
        {
            get;
            set;
        }

        public string SignInLinkURL
        {
            get;
            set;
        }

        public string SignOutLinkURL
        {
            get;
            set;
        }

        public bool ShowSignInLink
        {
            get;
            set;
        }

        public bool ShowSignOutLink
        {
            get;
            set;
        }

        public List<FooterLink> CustomFooterLinks
        {
            get;
            set;
        } = new List<FooterLink>();


        public List<MenuLink> MenuLinks
        {
            get;
            set;
        }

        public Link IntranetTitle
        {
            get;
            set;
        }

        public Core(ICurrentRequestProxy currentRequest, ICacheProxy cacheProxy, IOptions<GCConfigurations> config, IDictionary<string, ICDTSEnvironment> cdtsEnvironments)
        {
            _cacheProxy = cacheProxy;
            _config = config.Value;
            _cdtsEnvironments = cdtsEnvironments;
            SetDefaultValues(currentRequest);
        }

        private void SetDefaultValues(ICurrentRequestProxy currentRequest)
        {
            WebTemplateVersion = _config.Version;
            UseHTTPS = _config.UseHttps;
            Environment = _config.Environment.ToUpper();
            LoadJQueryFromGoogle = _config.LoadJQueryFromGoogle;
            SessionTimeout = new SessionTimeout
            {
                Enabled = _config.SessionTimeOut.Enabled,
                Inactivity = _config.SessionTimeOut.Inactivity,
                ReactionTime = _config.SessionTimeOut.ReactionTime,
                SessionAlive = _config.SessionTimeOut.Sessionalive,
                LogoutUrl = _config.SessionTimeOut.Logouturl,
                RefreshCallbackUrl = _config.SessionTimeOut.RefreshCallbackUrl,
                RefreshOnClick = _config.SessionTimeOut.RefreshOnClick,
                RefreshLimit = _config.SessionTimeOut.RefreshLimit,
                Method = _config.SessionTimeOut.Method,
                AdditionalData = _config.SessionTimeOut.AdditionalData
            };
            SessionTimeout.CheckWithServerSessionTimout(currentRequest.Session);
            LanguageLink = new LanguageLink
            {
                Href = CoreBuilder.BuildLanguageLinkURL(currentRequest.QueryString)
            };
            ShowPreContent = _config.ShowPreContent;
            ShowSearch = _config.ShowShearch;
            ShowPostContent = _config.ShowPostContent;
            ShowFeedbackLink = _config.ShowFeedbackLink;
            FeedbackLinkURL = _config.FeedbackLinkurl;
            ShowLanguageLink = _config.ShowLanguageLink;
            ShowSharePageLink = _config.ShowSharePageLink;
            ShowFeatures = _config.ShowFeatures;
            LeavingSecureSiteWarning = new LeavingSecureSiteWarning
            {
                Enabled = _config.LeavingSecureSiteWarning.Enabled,
                DisplayModalWindow = _config.LeavingSecureSiteWarning.DisplayModalWindow,
                RedirectURL = _config.LeavingSecureSiteWarning.RedirectURL,
                ExcludedDomains = _config.LeavingSecureSiteWarning.ExcludedDomains
            };
            SignOutLinkURL = _config.SignOutLinkURL;
            SignInLinkURL = _config.SignInLinkURL;
            CustomSearch = _config.CustomSearch;
        }

        public HtmlString RenderHeaderTitle()
        {
            return new HtmlString(HeaderTitle);
        }

        public HtmlString RenderAppFooter()
        {
            return Render.RenderAppFooter();
        }

        public HtmlString RenderAppTop()
        {
            return Render.RenderAppTop();
        }

        public HtmlString RenderTransactionalTop()
        {
            return Render.RenderTransactionalTop();
        }

        public HtmlString RenderTop()
        {
            return Render.RenderTop();
        }

        public HtmlString RenderRefTop(bool isApplication)
        {
            return Render.RenderRefTop(isApplication);
        }

        public HtmlString RenderUnilingualPreFooter()
        {
            return Render.RenderUnilingualPreFooter();
        }

        public HtmlString RenderPreFooter()
        {
            return Render.RenderPreFooter();
        }

        public HtmlString RenderTransactionalPreFooter()
        {
            return Render.RenderTransactionalPreFooter();
        }

        public HtmlString RenderFooter()
        {
            return Render.RenderFooter();
        }

        public HtmlString RenderTransactionalFooter()
        {
            return Render.RenderTransactionalFooter();
        }

        public HtmlString RenderRefFooter()
        {
            return Render.RenderRefFooter();
        }

        private HtmlString RenderCDNEnvOnly()
        {
            return JsonSerializationHelper.SerializeToJson(new CDNEnvOnly
            {
                CdnEnv = CDNEnvironment
            });
        }

        public HtmlString RenderServerTop()
        {
            return RenderCDNEnvOnly();
        }

        public HtmlString RenderServerBottom()
        {
            return RenderCDNEnvOnly();
        }

        public HtmlString RenderServerRefTop()
        {
            return RenderCDNEnvOnly();
        }

        public HtmlString RenderServerRefFooter()
        {
            return RenderCDNEnvOnly();
        }

        public HtmlString RenderSessionTimeoutControl()
        {
            return Render.RenderSessionTimeoutControl();
        }

        public HtmlString RenderLeftMenu()
        {
            return Render.RenderLeftMenu();
        }

        public HtmlString RenderHtmlHeaderElements()
        {
            return Render.RenderHtmlElements(HTMLHeaderElements);
        }

        public HtmlString RenderHtmlBodyElements()
        {
            return Render.RenderHtmlElements(HTMLBodyElements);
        }

        public HtmlString LoadStaticFile(string fileName)
        {
            string key = "GoC.Template.CacheKey" + "." + fileName;
            string text = _cacheProxy.GetFromCache<string>(key);
            if (text != null)
            {
                return new HtmlString(text);
            }
            lock (LockObject)
            {
                text = _cacheProxy.GetFromCache<string>(key);
                if (text != null)
                {
                    return new HtmlString(text);
                }

                string serverPath = ""; //HttpContext.Current.Server.MapPath(StaticFilesPath); //TODO: Fix by injecting IHostingEnvironment
                string text2 = (!StaticFilesPath.StartsWith("~")) ? Path.Combine(StaticFilesPath, fileName) : Path.Combine(serverPath, fileName);
                try
                {
                    text = File.ReadAllText(text2);
                    _cacheProxy.SaveToCache(key, text2, text);
                }
                catch (DirectoryNotFoundException)
                {
                    text = string.Empty;
                }
                catch (FileNotFoundException)
                {
                    text = string.Empty;
                }
            }
            return new HtmlString(text);
        }
    }
}
