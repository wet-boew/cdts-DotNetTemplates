using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using GoC.WebTemplate.Components.Entities;
using Microsoft.AspNetCore.Html;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Utils
{
    public class ModelRenderer
    {
        private readonly Model _model;
        internal ModelRenderer(Model model)
        {
            _model = model;
        }

        public HtmlString AppFooter()
        {
            if (!_model.CdtsEnvironment.CanHaveContactLinkInAppTemplate && _model.ContactLinks.Any())
            {
                throw new InvalidOperationException("Please use a CustomFooter to add a contact link in this environment");
            }


            return JsonSerializationHelper.SerializeToJson(new AppFooter
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                SubTheme = _model.Builder.GetStringForJson(_model.CdtsEnvironment.SubTheme),
                TermsLink = _model.Builder.BuildSingleFooterLink(_model.TermsConditionsLink),
                PrivacyLink = _model.Builder.BuildSingleFooterLink(_model.PrivacyLink),
                ContactLink = _model.Builder.BuildContactLinks(),
                LocalPath = _model.Builder.GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                FooterSections = _model.Builder.BuildCustomFooterSections
            });
        }

        public HtmlString AppTop()
        {
            if (_model.ShowSignInLink && _model.ShowSignOutLink)
                throw new InvalidOperationException("Unable to show sign in and sign out link together");
            if (_model.CustomSiteMenuURL != null && _model.MenuLinks != null && _model.MenuLinks.Any())
                throw new InvalidOperationException("Unable to have both a custom menu url and dynamically generated menu at the same time");

            //For v4.0.26.x we have to render this section differently depending on the theme, 
            //GCIntranet theme renders AppName and AppUrl seperately in GCWeb we render it as a List of Links. 
            return _model.CdtsEnvironment.ThemeIsGCWeb() ? GCWebAppTop() : GCIntranetApptop();
        }

        public HtmlString TransactionalTop()
        {
            if (_model.Settings.GcToolsModal && _model.CdtsEnvironment.ThemeIsGCWeb())
                throw new NotSupportedException(string.Format("The {0} is not supported in the {1} enviornment.", nameof(_model.Settings.GcToolsModal), _model.CdtsEnvironment.Name));

            return JsonSerializationHelper.SerializeToJson(new Top
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                SubTheme = _model.CdtsEnvironment.SubTheme,
                IntranetTitle = _model.Builder.BuildIntranentTitleList(),
                Search = _model.Settings.ShowSearch,
                LngLinks = _model.Builder.BuildLanguageLinkList(),
                Breadcrumbs = _model.Builder.BuildBreadcrumbs(),
                ShowPreContent = false,
                LocalPath = _model.Builder.BuildLocalPath(),
                TopSecMenu = _model.LeftMenuItems.Any(),
                CustomSearch = _model.CustomSearch == null ? null : new List<CustomSearch> { _model.CustomSearch },
                SiteMenu = false,
                GcToolsModal = _model.Settings.GcToolsModal
            });
        }

        public HtmlString Top()
        {
            if (_model.Settings.GcToolsModal && _model.CdtsEnvironment.ThemeIsGCWeb())
                throw new NotSupportedException(string.Format("The {0} is not supported in the {1} enviornment.", nameof(_model.Settings.GcToolsModal), _model.CdtsEnvironment.Name));

            return JsonSerializationHelper.SerializeToJson(new Top
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                SubTheme = _model.CdtsEnvironment.SubTheme,
                IntranetTitle = _model.Builder.BuildIntranentTitleList(),
                Search = _model.Settings.ShowSearch,
                LngLinks = _model.Builder.BuildLanguageLinkList(),
                ShowPreContent = _model.ShowPreContent,
                Breadcrumbs = _model.Builder.BuildBreadcrumbs(),
                LocalPath = _model.Builder.BuildLocalPath(),
                TopSecMenu = _model.LeftMenuItems.Any(),
                CustomSearch = _model.CustomSearch == null ? null : new List<CustomSearch> { _model.CustomSearch },
                SiteMenu = true,
                GcToolsModal = _model.Settings.GcToolsModal
            });
        }

        public HtmlString RefTop(bool isApplication)
        {
            return JsonSerializationHelper.SerializeToJson(new RefTop
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                SubTheme = _model.CdtsEnvironment.SubTheme,
                JqueryEnv = _model.Settings.LoadScriptsFromGoogle ? "external" : null,
                LocalPath = _model.Builder.BuildLocalPath(),
                IsApplication = isApplication,
                WebAnalytics = _model.Settings.WebAnalytics.Active ? new List<WebAnalytics> { _model.Settings.WebAnalytics } : null

            });
        }

        public HtmlString UnilingualPreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new UnilingualPreFooter
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                PageDetails = false
            });
        }

        public HtmlString PreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new PreFooter
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                DateModified = _model.Builder.BuildDateModified(),
                VersionIdentifier = _model.Builder.GetStringForJson(_model.VersionIdentifier),
                ShowPostContent = _model.Settings.ShowPostContent,
                ShowFeedback = _model.Settings.FeedbackLink,
                ShowShare = new ShareList
                {
                    Show = _model.Settings.ShowSharePageLink,
                    Enums = _model.SharePageMediaSites
                },
                ScreenIdentifier = _model.Builder.GetStringForJson(_model.ScreenIdentifier)
            });
        }

        public HtmlString TransactionalPreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new PreFooter
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                DateModified = _model.Builder.BuildDateModified(),
                VersionIdentifier = _model.Builder.GetStringForJson(_model.VersionIdentifier),
                ShowPostContent = false,
                ShowFeedback = new FeedbackLink { Show = false },
                ShowShare = new ShareList { Show = false },
                ScreenIdentifier = _model.Builder.GetStringForJson(_model.ScreenIdentifier)
            });
        }

        public HtmlString Footer()
        {
            return JsonSerializationHelper.SerializeToJson(new Footer
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                SubTheme = _model.CdtsEnvironment.SubTheme,
                ShowFooter = true,
                ContactLinks = _model.Builder.BuildContactLinks(),
                PrivacyLink = null,
                TermsLink = null

            });
        }

        public HtmlString TransactionalFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new Footer
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                SubTheme = _model.CdtsEnvironment.SubTheme,
                ShowFooter = false,
                ContactLinks = _model.Builder.BuildContactLinks(),
                PrivacyLink = _model.Builder.BuildSingleFooterLink(_model.PrivacyLink),
                TermsLink = _model.Builder.BuildSingleFooterLink(_model.TermsConditionsLink)

            });
        }

        public HtmlString RefFooter()
        {
            if (_model.Settings.WebAnalytics.Active && !_model.CdtsEnvironment.CanUseWebAnalytics) throw new NotSupportedException("The WebAnalytics is not supported in this enviornment.");

            if (_model.Settings.LeavingSecureSiteWarning.Enabled &&
                !string.IsNullOrEmpty(_model.Settings.LeavingSecureSiteWarning.RedirectUrl))
            {
                return SecureSiteWarningRefFooter();
            }
            return JsonSerializationHelper.SerializeToJson(new RefFooter
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                ExitScript = false,
                JqueryEnv = _model.Builder.BuildJqueryEnv(),
                LocalPath = _model.Builder.GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                WebAnalytics = _model.Settings.WebAnalytics.Active
            });
        }

        public HtmlString SessionTimeoutControl()
        {
            if (_model.Settings.SessionTimeout.Enabled)
            {
                HtmlString jsonSessionTimeout = JsonSerializationHelper.SerializeToJson(_model.Settings.SessionTimeout);
                return new HtmlString($"<span class='wb-sessto' data-wb-sessto='{jsonSessionTimeout}'></span>");
            }

            return null;
        }

        public HtmlString LeftMenu()
        {
            if (!_model.LeftMenuItems.Any())
                return new HtmlString(string.Empty);

            var leftMenuForSerialization = new { sections = new List<object>() };

            // capitalization on anonymous types matters here, CDTS will reject the json objects if not done right
            foreach (var menu in _model.LeftMenuItems)
            {
                var menuForSerialization = new
                {
                    sectionName = WebUtility.HtmlEncode(menu.Text),
                    sectionLink = _model.Builder.GetStringForJson(menu.Href),
                    newWindow = menu.NewWindow,
                    menuLinks = new List<object>() //can't be null
                };

                foreach (var menuItem in menu.Items)
                {
                    var item = menuItem as MenuItem;
                    if (item == null)
                    {
                        menuForSerialization.menuLinks.Add(new
                        {
                            href = menuItem.Href,
                            text = menuItem.Text
                        });
                    }
                    else
                    {
                        var subMenuForSerialization = new
                        {
                            href = item.Href,
                            text = item.Text,
                            newWindow = item.NewWindow,
                            subLinks = item.SubItems.Any() ? new List<object>() : null
                        };

                        foreach (var subMenuItem in item.SubItems)
                        {
                            subMenuForSerialization.subLinks.Add(new
                            {
                                subhref = subMenuItem.Href,
                                subtext = subMenuItem.Text,
                                newWindow = subMenuItem.NewWindow
                            });
                        }

                        menuForSerialization.menuLinks.Add(subMenuForSerialization);
                    }
                }

                leftMenuForSerialization.sections.Add(menuForSerialization);
            }

            return JsonSerializationHelper.SerializeToJson(leftMenuForSerialization);
        }

        /// <summary>
        /// Adds a string(html tag) to be included in the page
        /// This is our way off letting the developer add metatags, css and js to their pages.
        /// </summary>
        /// <remarks>we are accepting a string with no validation, therefore it is up to the developer to provide a valid string/html tag</remarks>
        /// <returns>
        /// string hopefully a valid html tag
        /// </returns>
        public static HtmlString HtmlElements(List<string> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));

            var sb = new StringBuilder();
            foreach (var tag in tags)
            {
                sb.AppendLine(tag);
            }
            return new HtmlString(sb.ToString());
        }

        public HtmlString HtmlBodyElements() => HtmlElements(_model.HTMLBodyElements);

        public HtmlString HtmlHeaderElements() => HtmlElements(_model.HTMLHeaderElements);

        public HtmlString HeaderTitle() => new HtmlString(_model.HeaderTitle);

        private HtmlString CdnEnvOnly() => JsonSerializationHelper.SerializeToJson(new CDNEnvOnly { CdnEnv = _model.CdtsEnvironment.CDN });

        public HtmlString ServerTop() => CdnEnvOnly();
        public HtmlString ServerBottom() => CdnEnvOnly();
        public HtmlString ServerRefTop() => CdnEnvOnly();
        public HtmlString ServerRefFooter() => CdnEnvOnly();
        public HtmlString SplashTop() => CdnEnvOnly();

        private HtmlString GCIntranetApptop()
        {
            return JsonSerializationHelper.SerializeToJson(new AppTopGcIntranet
            {
                AppName = new List<Link> { _model.ApplicationTitle },
                IntranetTitle = _model.Builder.BuildIntranentTitleList(),
                SignIn = _model.Builder.BuildHideableHrefOnlyLink(_model.Settings.SignInLinkUrl, _model.ShowSignInLink),
                SignOut = _model.Builder.BuildHideableHrefOnlyLink(_model.Settings.SignOutLinkUrl, _model.ShowSignOutLink),
                CdnEnv = _model.CdtsEnvironment.CDN,
                SubTheme = _model.CdtsEnvironment.SubTheme,
                Search = _model.Settings.ShowSearch,
                LngLinks = _model.Builder.BuildLanguageLinkList(),
                ShowPreContent = _model.ShowPreContent,
                Breadcrumbs = _model.Builder.BuildBreadcrumbs(),
                LocalPath = _model.Builder.GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                AppSettings = _model.Builder.BuildHideableHrefOnlyLink(_model.AppSettingsURL, true),
                MenuPath = _model.CustomSiteMenuURL,
                CustomSearch = _model.CustomSearch == null ? null : new List<CustomSearch> { _model.CustomSearch },
                TopSecMenu = _model.LeftMenuItems.Any(),
                MenuLinks = _model.MenuLinks,
                GcToolsModal = _model.Settings.GcToolsModal
            });
        }

        private HtmlString GCWebAppTop()
        {
            return JsonSerializationHelper.SerializeToJson(new AppTop
            {
                AppName = new List<Link> { _model.ApplicationTitle },
                SignIn = _model.Builder.BuildHideableHrefOnlyLink(_model.Settings.SignInLinkUrl, _model.ShowSignInLink),
                SignOut = _model.Builder.BuildHideableHrefOnlyLink(_model.Settings.SignOutLinkUrl, _model.ShowSignOutLink),
                CdnEnv = _model.CdtsEnvironment.CDN,
                SubTheme = _model.CdtsEnvironment.SubTheme,
                Search = _model.Settings.ShowSearch,
                LngLinks = _model.Builder.BuildLanguageLinkList(),
                ShowPreContent = _model.ShowPreContent,
                Breadcrumbs = _model.Builder.BuildBreadcrumbs(),
                LocalPath = _model.Builder.GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                AppSettings = _model.Builder.BuildHideableHrefOnlyLink(_model.AppSettingsURL, true),
                MenuPath = _model.CustomSiteMenuURL,
                CustomSearch = _model.CustomSearch == null ? null : new List<CustomSearch> { _model.CustomSearch },
                TopSecMenu = _model.LeftMenuItems.Any(),
                MenuLinks = _model.MenuLinks
            });
        }

        private HtmlString SecureSiteWarningRefFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new RefFooter
            {
                CdnEnv = _model.CdtsEnvironment.CDN,
                ExitScript = true,
                DisplayModal = _model.Settings.LeavingSecureSiteWarning.DisplayModalWindow,
                ExitURL = _model.Settings.LeavingSecureSiteWarning.RedirectUrl,
                ExitMsg = WebUtility.HtmlEncode(_model.Settings.LeavingSecureSiteWarning.Message),
                ExitDomains = _model.Builder.GetStringForJson(_model.Settings.LeavingSecureSiteWarning.ExcludedDomains),
                CancelMsg = _model.Builder.GetStringForJson(_model.Settings.LeavingSecureSiteWarning.CancelMessage),
                YesMsg = _model.Builder.GetStringForJson(_model.Settings.LeavingSecureSiteWarning.YesMessage),
                JqueryEnv = _model.Builder.BuildJqueryEnv(),
                LocalPath = _model.Builder.GetFormattedJsonString(_model.CdtsEnvironment.LocalPath, _model.CdtsEnvironment.Theme, _model.Settings.Version),
                WebAnalytics = _model.Settings.WebAnalytics.Active,
                TargetWarning = _model.Builder.GetStringForJson(_model.Settings.LeavingSecureSiteWarning.TargetWarning),
                DisplayModalForNewWindow = _model.Settings.LeavingSecureSiteWarning.DisplayModalForNewWindow
            });

        }

        public HtmlString SplashInfo()
        {
            return JsonSerializationHelper.SerializeToJson(new 
            {
                cdnEnv = _model.CdtsEnvironment.CDN,
                indexEng = _model.SplashPageInfo.EnglishHomeUrl,
                indexFra = _model.SplashPageInfo.FrenchHomeUrl,
                termsEng = _model.Builder.GetStringForJson(_model.SplashPageInfo.EnglishTermsUrl),
                termsFra = _model.Builder.GetStringForJson(_model.SplashPageInfo.FrenchTermsUrl),
                nameEng = _model.SplashPageInfo.EnglishName,
                nameFra = _model.SplashPageInfo.FrenchName
            });
        }
    }
}
