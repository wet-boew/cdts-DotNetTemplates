﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using GoC.WebTemplate.Components.Entities;
using System.Collections.Specialized;
using System.Net;
using System.Text;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Utils
{
    public class ModelBuilder
    {
        private readonly IModel _model;
        public ModelBuilder(IModel model)
        {
            _model = model;
        }

        /// <summary>
        /// Builds the "common" CDTS setup object based on parameteres (used by the render functions)
        /// </summary>
        public Setup BuildCommonSetup(bool isTransactional, bool isUnilingualError)
        {
            return new Setup
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                Mode = Mode.COMMON,
                Base = BuildSetupBase(),
                Top = BuildTop(isTransactional),
                PreFooter = BuildPreFooter(isTransactional, isUnilingualError),
                Footer = BuildFooter(isTransactional),
                SecMenu = _model.LeftMenuItems.Any() ? SectionMenuBuilder.BuildLeftMenu(_model.LeftMenuItems) : null,
                Splash = null,
                OnCDTSPageFinalized = _model.HTMLBodyElements
            };
        }

        /// <summary>
        /// Creates the `SetupBase` object needed in rendering the CDTS setup JSON
        /// </summary>
        public SetupBase BuildSetupBase()
        {
            if (_model.Settings.WebAnalytics.Active && !_model.CdtsEnvironment.CanUseWebAnalytics) throw new NotSupportedException("The WebAnalytics is not supported in this enviornment.");

            if (_model.Settings.LeavingSecureSiteWarning.Enabled)
            {
                return new SetupBase
                {
                    SubTheme = _model.CdtsEnvironment.SubTheme,
                    ExitSecureSite = _model.Builder.BuildExitSecureSite(),
                    JqueryEnv = _model.Builder.BuildJqueryEnv(),
                    WebAnalytics = _model.Settings.WebAnalytics.Active ? new List<WebAnalytics> { _model.Settings.WebAnalytics } : null,
                    SRIEnabled = _model.Settings.SRIEnabled ? (bool?)null : false
                };
            }

            return new SetupBase{
                SubTheme = _model.CdtsEnvironment.SubTheme,
                JqueryEnv = _model.Builder.BuildJqueryEnv(),
                WebAnalytics = _model.Settings.WebAnalytics.Active ? new List<WebAnalytics> { _model.Settings.WebAnalytics } : null,
                SRIEnabled = _model.Settings.SRIEnabled ? (bool?)null : false
            };
        }

        /// <summary>
        /// Builds the "Top" object needed in rendering the CDTS setup JSON
        /// </summary>
        public Top BuildTop(bool isTransactional)
        {
            if (_model.Settings.GcToolsModal && _model.CdtsEnvironment.ThemeIsGCWeb())
                throw new NotSupportedException($"The {nameof(_model.Settings.GcToolsModal)} is not supported in the {_model.CdtsEnvironment.Name} enviornment.");

            return new Top
            {
                CdnEnv = null, //no need for cdnEnv now that we're using CDTS setup function
                SubTheme = _model.CdtsEnvironment.SubTheme,
                IntranetTitle = _model.Builder.BuildIntranentTitleList(),
                Search = _model.Settings.ShowSearch,
                LngLinks = _model.Builder.BuildLanguageLinkList(),
                ShowPreContent = isTransactional ? false : _model.ShowPreContent,
                Breadcrumbs = _model.Builder.BuildBreadcrumbs(),
                LocalPath = _model.Builder.BuildLocalPath(),
                TopSecMenu = _model.LeftMenuItems.Any(),
                CustomSearch = _model.CustomSearch == null ? null : new List<CustomSearch> { _model.CustomSearch },
                SiteMenu = !isTransactional,
                GcToolsModal = _model.Settings.GcToolsModal,
                HidePlaceholderMenu = _model.HidePlaceholderMenu
            };
        }

        /// <summary>
        /// Builds the "AppTop" object needed in rendering the CDTS setup JSON
        /// </summary>
        public AppTop BuildAppTop()
        {
            if (_model.ShowSignInLink && _model.ShowSignOutLink)
                throw new InvalidOperationException("Unable to show sign in and sign out link together");
            if (_model.CustomSiteMenuURL != null && _model.MenuLinks != null && _model.MenuLinks.Any())
                throw new InvalidOperationException("Unable to have both a custom menu url and dynamically generated menu at the same time");

            //For v4.0.26.x we have to render this section differently depending on the theme, 
            //GCIntranet theme renders AppName and AppUrl seperately in GCWeb we render it as a List of Links. 
            if (_model.CdtsEnvironment.ThemeIsGCWeb())
            {
                return new AppTop
                {
                    AppName = new List<Link> { _model.ApplicationTitle },
                    SignIn = BuildHideableHrefOnlyLink(_model.Settings.SignInLinkUrl, _model.ShowSignInLink),
                    SignOut = BuildHideableHrefOnlyLink(_model.Settings.SignOutLinkUrl, _model.ShowSignOutLink),
                    CdnEnv = null, //no need for cdnEnv now that we're using CDTS setup function
                    SubTheme = _model.CdtsEnvironment.SubTheme,
                    Search = _model.Settings.ShowSearch,
                    LngLinks = _model.Builder.BuildLanguageLinkList(),
                    ShowPreContent = _model.ShowPreContent,
                    Breadcrumbs = _model.Builder.BuildBreadcrumbs(),
                    LocalPath = GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                    AppSettings = BuildHideableHrefOnlyLink(_model.AppSettingsURL, true),
                    MenuPath = _model.CustomSiteMenuURL,
                    CustomSearch = _model.CustomSearch == null ? null : new List<CustomSearch> { _model.CustomSearch },
                    TopSecMenu = _model.LeftMenuItems.Any(),
                    MenuLinks = _model.MenuLinks,
                    InfoBanner = _model.InfoBanner,
                    HeaderMenu = _model.HeaderMenu
                };
            }
            else
            {
                return new AppTopGcIntranet
                {
                    AppName = new List<Link> { _model.ApplicationTitle },
                    IntranetTitle = BuildIntranentTitleList(),
                    SignIn = BuildHideableHrefOnlyLink(_model.Settings.SignInLinkUrl, _model.ShowSignInLink),
                    SignOut = BuildHideableHrefOnlyLink(_model.Settings.SignOutLinkUrl, _model.ShowSignOutLink),
                    CdnEnv = null, //no need for cdnEnv now that we're using CDTS setup function
                    SubTheme = _model.CdtsEnvironment.SubTheme,
                    Search = _model.Settings.ShowSearch,
                    LngLinks = _model.Builder.BuildLanguageLinkList(),
                    ShowPreContent = _model.ShowPreContent,
                    Breadcrumbs = _model.Builder.BuildBreadcrumbs(),
                    LocalPath = GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                    AppSettings = BuildHideableHrefOnlyLink(_model.AppSettingsURL, true),
                    MenuPath = _model.CustomSiteMenuURL,
                    CustomSearch = _model.CustomSearch == null ? null : new List<CustomSearch> { _model.CustomSearch },
                    TopSecMenu = _model.LeftMenuItems.Any(),
                    MenuLinks = _model.MenuLinks,
                    GcToolsModal = _model.Settings.GcToolsModal
                };
            }
        }

        /// <summary>
        /// Builds the "PreFooter" object needed in rendering the CDTS setup JSON
        /// </summary>
        public IPreFooter BuildPreFooter(bool isTransactional, bool isUnilingualError)
        {
            if (!isTransactional)
            {
                if (!isUnilingualError)
                {
                    Feedback feedback = new Feedback();
                    feedback.Enabled = _model.Settings.FeedbackLink.Show;

                    var isFrenchCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.StartsWith(Constants.FRENCH_ACCRONYM, StringComparison.OrdinalIgnoreCase);

                    if (!string.IsNullOrWhiteSpace(_model.Settings.FeedbackLink.Text) || !string.IsNullOrWhiteSpace(_model.Settings.FeedbackLink.Section) || !string.IsNullOrWhiteSpace(_model.Settings.FeedbackLink.Theme))
                    {
                        feedback.Section = _model.Settings.FeedbackLink.Section;
                        feedback.Theme = _model.Settings.FeedbackLink.Theme;
                        feedback.Text = (isFrenchCulture && !string.IsNullOrEmpty(_model.Settings.FeedbackLink.TextFr)) ? _model.Settings.FeedbackLink.TextFr : _model.Settings.FeedbackLink.Text;
                        feedback.Href = (isFrenchCulture && !string.IsNullOrEmpty(_model.Settings.FeedbackLink.UrlFr)) ? _model.Settings.FeedbackLink.UrlFr : _model.Settings.FeedbackLink.Url;
                    }
                    else if (!string.IsNullOrWhiteSpace(_model.Settings.FeedbackLink.Url))
                    {
                        feedback.LegacyBtnUrl = (isFrenchCulture && !string.IsNullOrEmpty(_model.Settings.FeedbackLink.UrlFr)) ? _model.Settings.FeedbackLink.UrlFr : _model.Settings.FeedbackLink.Url;
                    }

                    return new PreFooter 
                    {
                        CdnEnv = null, //no need for cdnEnv now that we're using CDTS setup function
                        DateModified = _model.Builder.BuildDateModified(),
                        VersionIdentifier = GetStringForJson(_model.VersionIdentifier),
                        ShowPostContent = _model.Settings.ShowPostContent,
                        ShowFeedback = feedback,
                        ShowShare = new ShareList
                        {
                            Show = _model.Settings.ShowSharePageLink,
                            Enums = _model.SharePageMediaSites
                        },
                        ScreenIdentifier = GetStringForJson(_model.ScreenIdentifier),
                        Contributors = _model.Contributors
                    };
                }
                else
                { //(isUnilingualError)
                    return new UnilingualPreFooter() { CdnEnv = null, PageDetails = false};
                }
            }
            else
            { //(isTransactional)
                return new PreFooter
                {
                    CdnEnv = null, //no need for cdnEnv now that we're using CDTS setup function
                    DateModified = _model.Builder.BuildDateModified(),
                    VersionIdentifier = GetStringForJson(_model.VersionIdentifier),
                    ShowPostContent = false,
                    ShowFeedback = new Feedback { Enabled = false },
                    ShowShare = new ShareList { Show = false },
                    ScreenIdentifier = GetStringForJson(_model.ScreenIdentifier)
                };
            }
        }

        /// <summary>
        /// Builds the "Footer" object needed in rendering the CDTS setup JSON
        /// </summary>
        public Footer BuildFooter(bool isTransactional)
        {
            return new Footer
            {
                CdnEnv = null, //no need for cdnEnv now that we're using CDTS setup function
                SubTheme = _model.CdtsEnvironment.SubTheme,
                ShowFooter = !isTransactional,
                ContactLinks = _model.Builder.BuildContactLinks(),
                PrivacyLink = BuildFooterLinkContext(_model.PrivacyLink, !isTransactional),
                TermsLink = BuildFooterLinkContext(_model.TermsConditionsLink, !isTransactional),
                ContextualFooter = isTransactional ? null : _model.ContextualFooter,
                HideFooterMain = isTransactional ? false : _model.HideFooterMain,
                HideFooterCorporate = isTransactional ? false : _model.HideFooterCorporate,
            };
        }

        /// <summary>
        /// Builds the "AppFooter" object needed in rendering the CDTS setup JSON
        /// </summary>
        public AppFooter BuildAppFooter()
        {
            if (!_model.CdtsEnvironment.CanHaveContactLinkInAppTemplate && _model.ContactLinks.Any())
            {
                throw new InvalidOperationException("Please use a CustomFooter to add a contact link in this environment");
            }

            if (_model.CdtsEnvironment.ThemeIsGCWeb())
            {
                return new AppFooter
                {
                    CdnEnv = null, //no need for cdnEnv now that we're using CDTS setup function
                    SubTheme = GetStringForJson(_model.CdtsEnvironment.SubTheme),
                    TermsLink = BuildSingleFooterLink(_model.TermsConditionsLink),
                    PrivacyLink = BuildSingleFooterLink(_model.PrivacyLink),
                    ContactLink = _model.Builder.BuildContactLinks(),
                    LocalPath = GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                    FooterSections = _model.Builder.BuildContextualFooter,
                    FooterPath = GetStringForJson(_model.FooterPath)
                };
            }
            else
            {
                return new AppFooterGcIntranet
                {
                    CdnEnv = null, //no need for cdnEnv now that we're using CDTS setup function
                    SubTheme = GetStringForJson(_model.CdtsEnvironment.SubTheme),
                    TermsLink = BuildSingleFooterLink(_model.TermsConditionsLink),
                    PrivacyLink = BuildSingleFooterLink(_model.PrivacyLink),
                    ContactLink = _model.Builder.BuildContactLinks(),
                    LocalPath = GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                    FooterSections = _model.Builder.BuildCustomFooterSections,
                    FooterPath = GetStringForJson(_model.FooterPath)
                };
            }
        }

#pragma warning disable CA1055
        /// <summary>
        /// Builds the URL to be used by the English/francais link at the top of the page for the language toggle.
        /// The method will add or update the "GoCTemplateCulture" querystring parameter with the culture to be set
        /// The language toggle link, posts back to the same page, and the InitializedCulture method of the BasePage is responsible for setting the culture with the provided value
        /// </summary>
        /// <returns>The URL to be used for the language toggle link</returns>
        public static string BuildLanguageLinkURL(NameValueCollection nameValues)
        {
            if (nameValues is null) throw new ArgumentNullException(nameof(nameValues));

            //make it writeable
            // var nameValues = new NameValueCollection(queryString.ToString());

            //Set the value of the "GoCTemplateCulture" parameter
            nameValues.Set(Constants.QUERYSTRING_CULTURE_KEY,
                Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.StartsWith(Constants.ENGLISH_ACCRONYM,
                    StringComparison.OrdinalIgnoreCase)
                    ? Constants.FRENCH_CULTURE
                    : Constants.ENGLISH_CULTURE);

            StringBuilder buff = new StringBuilder(256);
            char seperator = '?';

            foreach(string key in nameValues.Keys)
            {
                buff.Append(seperator);
                buff.Append(Uri.EscapeDataString(key));
                buff.Append('=');
                buff.Append(Uri.EscapeDataString(nameValues[key]));
                seperator = '&';
            }

            return buff.ToString();
        }
#pragma warning restore CA1055

        internal List<Link> BuildContactLinks()
        {
            if (!_model.CdtsEnvironment.CanHaveMultipleContactLinks)
            {
                if (_model.ContactLinks?.Count > 1)
                    throw new InvalidOperationException("Having multiple contact links not allowed in this environment");

                if (_model.ContactLinks?.Count == 1 && !string.IsNullOrWhiteSpace(_model.ContactLinks[0]?.Text))
                    throw new InvalidOperationException("Unable to edit Contact Link text in this environment");
            }

            if (_model.ContactLinks.Any(link => string.IsNullOrWhiteSpace(link.Href)))
                throw new InvalidOperationException("Href must be specified");
            return _model.ContactLinks;

        }

        internal static List<FooterLink> BuildSingleFooterLink(FooterLink link)
        {
            return string.IsNullOrWhiteSpace(link?.Href) ? null : new List<FooterLink> { link };
        }

        internal static FooterLinkContext BuildFooterLinkContext(FooterLink link, bool showFooter)
        {
            return string.IsNullOrEmpty(link.Href) ? null : new FooterLinkContext { ShowFooter = showFooter, FooterLink = link };
        }

        internal string BuildLocalPath()
        {
            return GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version);
        }

        internal List<Breadcrumb> BuildBreadcrumbs()
        {
            if (_model.Breadcrumbs == null || !_model.Breadcrumbs.Any())
            {
                return null;
            }

            return _model.Breadcrumbs.Select(b => new Breadcrumb
            {
                Href = GetStringForJson(b.Href),
                Acronym = GetStringForJson(b.Acronym),
                Title = GetStringForJson(b.Title)
            }).ToList();
        }

        internal List<IFooterSection> BuildCustomFooterSections
        {
            get
            {
                if (!_model.FooterSections.Any() && !_model.CustomFooterLinks.Any()) return null;

                if (_model.CdtsEnvironment.FooterSectionLimit > 0)
                { // use FooterSections
                    if (!_model.FooterSections.Any())
                        throw new InvalidOperationException(
                            "The CustomFooterLinks cannot be used in this enviornment, please use the FooterSections");

                    if (_model.FooterSections.Count > _model.CdtsEnvironment.FooterSectionLimit)
                        throw new InvalidOperationException(
                            $"The maximum FooterSections allowed for this environment is {_model.CdtsEnvironment.FooterSectionLimit}");

                    _model.FooterSections.ForEach(fs => fs.CustomFooterLinks.ForEach(fl => fl.Text = GetStringForJson(fl.Text)));

                    return new List<IFooterSection>(_model.FooterSections);
                }
                else
                { // use CustomFooterLinks

                    if (!_model.CustomFooterLinks.Any())
                        throw new InvalidOperationException(
                            "The FooterSections cannot be used in this enviornment, please use the CustomFooterLinks");


                    return new List<IFooterSection>(_model.CustomFooterLinks.Select(fl => new FooterLink
                    {
                        Href = fl.Href,
                        NewWindow = fl.NewWindow,
                        Text = GetStringForJson(fl.Text)
                    }));
                }
            }
        }

        internal ContextualFooter BuildContextualFooter
        {
            get
            {
                if (_model.ContextualFooter != null) return _model.ContextualFooter;

                if (!_model.FooterSections.Any() && !_model.CustomFooterLinks.Any()) return null;

                if (_model.CdtsEnvironment.FooterSectionLimit > 0)
                { // use FooterSections
                    if (!_model.FooterSections.Any())
                        throw new InvalidOperationException(
                            "The CustomFooterLinks cannot be used in this enviornment, please use the Contextual Footer");

                    if (_model.FooterSections.Count > _model.CdtsEnvironment.FooterSectionLimit)
                        throw new InvalidOperationException(
                            $"The maximum FooterSections allowed for this environment is {_model.CdtsEnvironment.FooterSectionLimit}");

                    return new ContextualFooter()
                    {
                        Links =_model.FooterSections.SelectMany(fs => fs.CustomFooterLinks)
                    };
                }
                else
                { // use CustomFooterLinks

                    if (!_model.CustomFooterLinks.Any())
                        throw new InvalidOperationException(
                            "The FooterSections cannot be used in this enviornment, please use the CustomFooterLinks");

                    return new ContextualFooter()
                    {
                        Links = _model.CustomFooterLinks
                    };
                }
            }
        }

        internal static List<Link> BuildHideableHrefOnlyLink(string href, bool showLink)
        {
            if (!showLink || string.IsNullOrWhiteSpace(href))
                return null;
            return new List<Link> { new Link { Href = href, Text = null } };
        }
        internal string BuildJqueryEnv() => _model.Settings.LoadScriptsFromGoogle ? "external" : null;

        internal string BuildDateModified()
        {

            if (DateTime.Compare(_model.DateModified, DateTime.MinValue) == 0)
            {
                return null;
            }
            return _model.DateModified.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
        }

        internal List<IntranetTitle> BuildIntranentTitleList()
        {
            if (_model.IntranetTitle == null) return null;

            return new List<IntranetTitle> { _model.IntranetTitle };
        }

        internal List<LanguageLink> BuildLanguageLinkList()
        {
            if (!_model.Settings.ShowLanguageLink)
            {
                return null;
            }

            return new List<LanguageLink> {
                new LanguageLink {
                    Href = _model.LanguageLink.Href
                }
            };
        }

        internal ExitSecureSite BuildExitSecureSite()
        {
            return new ExitSecureSite
            {
                ExitScript = true,
                DisplayModal = _model.Settings.LeavingSecureSiteWarning.DisplayModalWindow,
                MsgBoxHeader = GetStringForJson(_model.Settings.LeavingSecureSiteWarning.MsgBoxHeader),
                ExitURL = _model.Settings.LeavingSecureSiteWarning.RedirectUrl,
                ExitMsg = WebUtility.HtmlEncode(_model.Settings.LeavingSecureSiteWarning.Message),
                CancelMsg = GetStringForJson(_model.Settings.LeavingSecureSiteWarning.CancelMessage),
                YesMsg = GetStringForJson(_model.Settings.LeavingSecureSiteWarning.YesMessage),
                ExitDomains = GetStringForJson(_model.Settings.LeavingSecureSiteWarning.ExcludedDomains),
                TargetWarning = GetStringForJson(_model.Settings.LeavingSecureSiteWarning.TargetWarning),
                DisplayModalForNewWindow = _model.Settings.LeavingSecureSiteWarning.DisplayModalForNewWindow
            };
        }

        /// <summary>
        /// Builds the path to the cdn based on the environment set in the config. The path is based on the url of the environment, theme and version
        /// </summary>
        /// <returns>String, the complete path to the cdn</returns>
        internal string BuildCDNPath()
        {
            if (!_model.CdtsEnvironment.IsEncryptionModifiable && _model.Settings.UseHttps.HasValue)
            {
                throw new InvalidOperationException($"{_model.Settings.Environment} does not allow useHTTPS to be toggled");
            }

            if (_model.CdtsEnvironment.IsEncryptionModifiable && !_model.Settings.UseHttps.HasValue)
            {
                throw new InvalidOperationException($"{_model.Settings.Environment} requires useHTTPS to be true or false not null.");
            }

            var https = string.Empty;
            if (_model.CdtsEnvironment.IsEncryptionModifiable)
            {
                //We've already checked to see if this is null before here so ignore this in resharper
                // ReSharper disable once PossibleInvalidOperationException
                https = _model.Settings.UseHttps.Value ? "s" : string.Empty;
            }

            var run = string.Empty;
            var version = string.Empty;
            if (string.IsNullOrWhiteSpace(_model.Settings.Version))
            {
                if (_model.CdtsEnvironment.IsVersionRNCombined)
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
                version = _model.Settings.Version + "/";
                run = "app";
            }

            return string.Format(CultureInfo.InvariantCulture, _model.CdtsEnvironment.Path, https, run, _model.CdtsEnvironment.Theme, version);
        }

        private string BuildPathAttributes(string pathAttributeName, string fileName)
        {
            StringBuilder buf = new StringBuilder(384);

            buf.Append(pathAttributeName);
            buf.Append("=\"");
            buf.Append(BuildCDNPath());
            buf.Append(fileName);
            buf.Append("\"");
            if (_model.Settings.SRIEnabled)
            {
                string hash = _model.CurrentSRIHashes.TryGetValue(fileName, out hash) ? hash : null;
                if (!string.IsNullOrEmpty(hash))
                { //if not found, simply don't issue SRI attributes
                    buf.Append(" integrity=\"");
                    buf.Append(hash);
                    buf.Append("\" crossorigin=\"anonymous\"");
                }
            }

            return buf.ToString();
        }

        public string BuildWetJsPathAttributes(string lang)
        {
            return BuildPathAttributes("src", $"compiled/wet-{lang}.js");
        }

        /// <summary>
        /// Builds the href/integrity/crossorigin attribute path to the cdts-styles.css file.
        /// </summary>
        /// <returns>String, the complete path to the cdn</returns>
        public string BuildCSSPathAttributes()
        {
            //---[ Figure out CSS file name
            string fileName = "cdts-styles.css";

            if (!_model.CdtsEnvironment.ThemeIsGCWeb())
            {
                string subTheme = _model.CdtsEnvironment.SubTheme;
                if (!string.IsNullOrEmpty(subTheme))
                {
                    //subTheme = subTheme.ToLower();
                    //...limit to supported subthemes
                    if (subTheme.Equals("esdc", StringComparison.OrdinalIgnoreCase) || subTheme.Equals("eccc", StringComparison.OrdinalIgnoreCase))
                    {
                        fileName = $"cdts-{subTheme}-styles.css";
                    }
                }
            }

            //(if we get here, we're gcweb or gcintranet with no subtheme value)
            //return $"{BuildCDNPath()}cdts-styles.css";
            return BuildPathAttributes("href", fileName);
        }

        /// <summary>
        ///Builds the href/integrity/crossorigin attribute path to the cdts-[subtheme]-styles.css file.
        /// </summary>
        public string BuildAppCSSPathAttributes()
        {
            if (_model.CdtsEnvironment.ThemeIsGCWeb())
            {
                return BuildPathAttributes("href", "cdts-app-styles.css");
            }
            else
            {
                return BuildCSSPathAttributes();
            }
        }

        /// <summary>
        /// Builds the href/integrity/crossorigin path to the cdts-splash-styles.css file.
        /// </summary>
        public string BuildSplashCSSPathAttributes()
        {
            return BuildPathAttributes("href", "cdts-splash-styles.css");
        }

        #region GetJson

        internal static string GetStringForJson(string str) => string.IsNullOrWhiteSpace(str) ? null : str;

        internal static string GetFormattedJsonString(string formatStr, params object[] strs) => string.IsNullOrWhiteSpace(formatStr) ? null : string.Format(CultureInfo.CurrentCulture, formatStr, strs);

        #endregion
    }
}
