using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using System;
using System.Collections.Generic;
#if NETCOREAPP
    using Microsoft.AspNetCore.Html;
#elif NETFRAMEWORK
using System.Web;
#endif

namespace GoC.WebTemplate.Components
{
    public interface IModel
    {
        /// <summary>
        /// Represents the Application Title setting information
        /// Set Programmatically
        /// </summary>
        /// <remarks>Usable in Intranet Themes and Application Template</remarks>
        Link ApplicationTitle { get; }

        /// <summary>
        /// The link to use for the App Settings in the AppTop
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        string AppSettingsURL { get; set; }

        /// <summary>
        /// property to hold the version of the template. it will be put as a comment in the html of the master pages. this will help us troubleshoot issues with clients using the template
        /// </summary>
        string AssemblyVersion { get; }

        /// <summary>
        /// Represents the list of links for the Breadcrumbs
        /// Set by application programmatically
        /// </summary>
        List<Breadcrumb> Breadcrumbs { get; set; }

        /// <summary>
        /// Complete path of the CDN including http(s), theme and run or versioned
        /// Set by Core
        /// </summary>
        string CDNPath { get; }

        ICdtsEnvironment CdtsEnvironment { get; }

        /// <summary>
        /// Used to override the Contact link in Footer, AppFooter and TransacationalFooter
        /// Set by application programmatically
        /// </summary>
        List<Link> ContactLinks { get; set; }

        /// <summary>
        /// Used to add a contextual band above the main footer that can display up to 3 links
        /// Set by application programmatically
        /// </summary>
        ContextualFooter ContextualFooter { get; set; }

        /// <summary>
        /// Custom links if null uses standard links if not null overrides the existing footer links
        /// Set by application programmatically
        /// Only available in the Application Template in GCWeb enviornment
        /// </summary>
        List<FooterLink> CustomFooterLinks { get; set; }

        /// <summary>
        /// Allows for a custom search to be used in the application.
        /// </summary>
        CustomSearch CustomSearch { get; set; }

        /// <summary>
        /// A custom site menu to be used in place of the standard canada.ca site menu
        /// This defaults to null (use standard menu)
        /// Set by application programmatically or in the Web.Config
        /// Only available in the Application Template
        /// </summary>
        string CustomSiteMenuURL { get; set; }

        /// <summary>
        /// Represents the date modified displayed just above the footer
        /// Set by application programmatically
        /// </summary>
        DateTime DateModified { get; set; }

        /// <summary>
        /// The path for a custom footer
        /// Set by application programmatically
        /// </summary>
        string FooterPath { get; set; }

        /// <summary>
        /// Custom links if null uses standard links if not null overrides the existing footer links in sections with headers
        /// Set by application programmatically
        /// Only available in the Application Template when not in GCWEB enviornment
        /// </summary>
        List<FooterSection> FooterSections { get; set; }

        /// <summary>
        /// title of page, will automatically add '- Canada.ca' to all pages implementing GCWeb theme as per
        /// Set by application programmatically
        /// </summary>
        string HeaderTitle { get; set; }

        /// <summary>
        /// Used to hide the placeholder menu while a custom menu is being loaded
        /// Set by application programmatically
        /// Only applicable to the Intranet theme
        /// </summary>
        bool HidePlaceholderMenu { get; set; }

        /// <summary>

        /// Used to determine if the corportate footer links will be displayed 
        /// Set by application programmatically
        /// </summary>
        bool HideFooterCorporate { get; set; }

        /// <summary>
        /// Used to hide the main footer
        /// Set by application programmatically
        /// </summary>
        bool HideFooterMain { get; set; }

        /// <summary>
        /// Represents the list of html elements to add at the end of the body tag
        /// will be used to add metatags, css, js etc.
        /// Set by application programmatically
        /// </summary>
        List<string> HTMLBodyElements { get; set; }

        /// <summary>
        /// Represents the list of html elements to add to the header tag
        /// will be used to add metatags, css, js etc.
        /// Set by application programmatically
        /// </summary>
        List<string> HTMLHeaderElements { get; set; }

        /// <summary>
        /// Used to display an information banner on top of the page
        /// Set by application programmatically
        /// </summary>
        InfoBanner InfoBanner { get; set; }

        /// <summary>
        /// Used to display a custom intranet title
        /// </summary>
        IntranetTitle IntranetTitle { get; set; }

        /// <summary>
        /// Used to override the langauge link
        /// </summary>
        LanguageLink LanguageLink { get; set; }

        // ReSharper restore InconsistentNaming
        /// <summary>
        /// Represents a list of menu items
        /// </summary>
        List<MenuSection> LeftMenuItems { get; set; }

        /// <summary>
        /// Custom Links for the top Menu added for MSCAs (Currently) use only. 
        /// Set by application programmatically
        /// Only available in the Application Template
        /// </summary>
        List<MenuLink> MenuLinks { get; set; }

        /// <summary>
        /// Configures the Privacy Link
        /// Set by application programmatically
        /// </summary>
        FooterLink PrivacyLink { get; set; }

        ModelBuilder Builder { get; }

        ModelRenderer Render { get; }

        /// <summary>
        /// A unique string to identify a web page. Used by user to identify the screen where an issue occured.
        /// </summary>
        string ScreenIdentifier { get; set; }

        IWebTemplateSettings Settings { get; }

        /// <summary>
        /// Representes the list of items to be displayed in the Share Page window
        /// Set by application programmatically
        /// </summary>
        List<SocialMediaSites> SharePageMediaSites { get; set; }

        /// <summary>
        /// Determines if the Pre Content of the header are to be displayed
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        bool ShowPreContent { get; set; }

        /// <summary>
        /// Displays the sign in link set.
        /// <see cref="SignInLinkURL"/> must not be null or whitespace
        /// <see cref="ShowSignOutLink"/> must not be set at the same time.
        /// Set by application programmatically
        /// Only available in the Application Template
        /// </summary>
        bool ShowSignInLink { get; set; }

        /// <summary>
        /// Displays the signout link set.
        /// <see cref="SignOutLinkURL"/> must not be null or whitespace
        /// <see cref="ShowSignInLink"/> must not be set at the same time.
        /// Set by application programmatically
        /// Only available in the Application Template
        /// </summary>
        bool ShowSignOutLink { get; set; }

        /// <summary>
        /// Info for Spash page
        /// Only applicable to Splash Layout/Master
        /// </summary>
        SplashPageInfo SplashPageInfo { get; set; }

        /// <summary>
        /// Determines the path to the location of the staticback up files
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        /// <remarks>The "theme" is concatenated to the end of this path.</remarks>
        string StaticFilesPath { get; set; }

        /// <summary>
        /// Configures the Terms and Conditions Link
        /// Set by application programmatically
        /// </summary>
        FooterLink TermsConditionsLink { get; set; }

        /// <summary>
        /// Retreive the first 2 letters of the current culture "en" or "fr"
        /// Used by generate paths, determine language etc...
        /// Set by Template
        string TwoLetterCultureLanguage { get; }

        /// <summary>
        /// version of application to be displayed instead of the date modified
        /// set by application programmatically
        /// </summary>
        string VersionIdentifier { get; set; }

        /// <summary>
        /// This method is used to get the static file content from the cache. if the cache is empty it will read the content from the file and load it into the cache.
        /// </summary>
        /// <param name="fileName">static file name to retreive</param>
        /// <returns>A string containing the content of the file.</returns>
        HtmlString LoadStaticFile(string fileName);
    }
}