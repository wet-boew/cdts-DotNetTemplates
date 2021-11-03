using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Utils.Caching;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
#if NETCOREAPP
    using Microsoft.AspNetCore.Html;
#elif NETFRAMEWORK
    using System.Web;
#endif

namespace GoC.WebTemplate.Components
{
    public class Model : IModel
    {
        private readonly FileContentCache _fileContentCache;
        private readonly IDictionary<string, ICdtsEnvironment> _cdtsEnvironments;

        private string _headerTitle;
        private string _staticFilesPath;
        private ModelBuilder _builder;
        private ModelRenderer _renderer;

        public ModelBuilder Builder => _builder ?? (_builder = new ModelBuilder(this));
        public ModelRenderer Render => _renderer ?? (_renderer = new ModelRenderer(this));

        public WebTemplateSettings Settings { get; }

        public Model(IFileContentCacheProvider fileContentCacheProvider,
            WebTemplateSettings settings,
            ICdtsCacheProvider cdtsCacheProvider)
        {
            if (fileContentCacheProvider == null) throw new ArgumentNullException(nameof(fileContentCacheProvider));
            if (cdtsCacheProvider == null) throw new ArgumentNullException(nameof(cdtsCacheProvider));
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _fileContentCache = new FileContentCache(fileContentCacheProvider);
            _cdtsEnvironments = new CdtsEnvironmentCache(cdtsCacheProvider).GetContent();
        }

        /// <summary>
        /// property to hold the version of the template. it will be put as a comment in the html of the master pages. this will help us troubleshoot issues with clients using the template
        /// </summary>
        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Represents the Application Title setting information
        /// Set Programmatically
        /// </summary>
        /// <remarks>Usable in Intranet Themes and Application Template</remarks>
        public Link ApplicationTitle { get; } = new Link();

        /// <summary>
        /// Represents the list of links for the Breadcrumbs
        /// Set by application programmatically
        /// </summary>
        public List<Breadcrumb> Breadcrumbs { get; set; } = new List<Breadcrumb>();

        public ICdtsEnvironment CdtsEnvironment => _cdtsEnvironments[Settings.Environment];

        /// <summary>
        /// Complete path of the CDN including http(s), theme and run or versioned
        /// Set by Core
        /// </summary>
        public string CDNPath => Builder.BuildCDNPath();

        /// <summary>
        /// Used to override the Contact link in Footer, AppFooter and TransacationalFooter
        /// Set by application programmatically
        /// </summary>
        public List<Link> ContactLinks { get; set; } = new List<Link>();

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
        /// Configures the Privacy Link
        /// Set by application programmatically
        /// </summary>
        public FooterLink PrivacyLink { get; set; } = new FooterLink();

        /// <summary>
        /// Configures the Terms and Conditions Link
        /// Set by application programmatically
        /// </summary>
        public FooterLink TermsConditionsLink { get; set; } = new FooterLink();


        /// <summary>
        /// Used to override the langauge link
        /// </summary>
        public LanguageLink LanguageLink { get; set; } = new LanguageLink();

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
                       (_staticFilesPath = string.Concat(Settings.StaticFilesLocation, "/", CdtsEnvironment.Theme));
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

        /// <summary>
        /// title of page, will automatically add '- Canada.ca' to all pages implementing GCWeb theme as per
        /// Set by application programmatically
        /// </summary>
        public string HeaderTitle
        {
            get
            {

                if (string.IsNullOrWhiteSpace(CdtsEnvironment.AppendToTitle))
                {
                    return _headerTitle;
                }

                if (string.IsNullOrWhiteSpace(_headerTitle))
                {
                    return CdtsEnvironment.AppendToTitle;
                }

                if (_headerTitle.EndsWith(CdtsEnvironment.AppendToTitle, StringComparison.CurrentCulture))
                {
                    return _headerTitle;
                }

                return _headerTitle + CdtsEnvironment.AppendToTitle;
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
        /// Allows for a custom search to be used in the application.
        /// </summary>
        public CustomSearch CustomSearch { get; set; }

        /// <summary>
        /// A custom site menu to be used in place of the standard canada.ca site menu
        /// This defaults to null (use standard menu)
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public string CustomSiteMenuURL { get; set; }

        /// <summary>
        /// The link to use for the App Settings in the AppTop
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        public string AppSettingsURL { get; set; }

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
        /// Info for Spash page
        /// Only applicable to Splash Layout/Master
        /// </summary>
        public SplashPageInfo SplashPageInfo { get; set; } = new SplashPageInfo();

        /// <summary>
        /// Custom links if null uses standard links if not null overrides the existing footer links
        /// Set by application programmatically
        /// Only available in the Application Template in GCWeb enviornment
        /// </summary>
        public List<FooterLink> CustomFooterLinks { get; set; } = new List<FooterLink>();

        /// <summary>
        /// Custom links if null uses standard links if not null overrides the existing footer links in sections with headers
        /// Set by application programmatically
        /// Only available in the Application Template when not in GCWEB enviornment
        /// </summary>
        public List<FooterSection> FooterSections { get; set; } = new List<FooterSection>();

        /// <summary>
        /// Custom Links for the top Menu added for MSCAs (Currently) use only. 
        /// Set by application programmatically
        /// Only available in the Application Template
        /// </summary>
        public List<MenuLink> MenuLinks { get; set; }

        public IntranetTitle IntranetTitle { get; set; }

        /// <summary>
        /// This method is used to get the static file content from the cache. if the cache is empty it will read the content from the file and load it into the cache.
        /// </summary>
        /// <param name="fileName">static file name to retreive</param>
        /// <returns>A string containing the content of the file.</returns>
        public HtmlString LoadStaticFile(string fileName)
        {
            var content = _fileContentCache.GetContent(fileName, StaticFilesPath);
            return new HtmlString(content);
        }

        /// <summary>
        /// The path for a custom footer
        /// Set by application programmatically
        /// </summary>
        public string FooterPath { get; set; }
    }
}
