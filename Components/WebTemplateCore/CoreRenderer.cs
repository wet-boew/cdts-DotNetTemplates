using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using GoC.WebTemplate.Components.JSONSerializationObjects;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components
{
    internal class CoreRenderer
    {
        private readonly Core _core;
        internal CoreRenderer(Core core)
        {
            _core = core;
        }

        internal HtmlString RenderAppFooter()
        {
            if (_core.CurrentEnvironment.Name != "AKAMAI" && _core.ContactLinks.Any())
            {
                throw new InvalidOperationException("Please use a CustomFooter to add a contact link in this environment");
            }
            
            return JsonSerializationHelper.SerializeToJson(new AppFooter
            {
                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.Builder.GetStringForJson(_core.WebTemplateSubTheme),
                TermsLink = _core.Builder.GetStringForJson(_core.TermsConditionsLinkURL),
                PrivacyLink = _core.Builder.GetStringForJson(_core.PrivacyLinkURL),
                ContactLink = _core.Builder.BuildContactLinks(),
                LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion),
                FooterSections = _core.Builder.BuildCustomFooterLinks
            });
        }

        internal HtmlString RenderAppTop()
        {
            if (_core.ShowSignInLink && _core.ShowSignOutLink)
                throw new InvalidOperationException("Unable to show sign in and sign out link together");
            if (_core.CustomSiteMenuURL != null && _core.MenuLinks != null && _core.MenuLinks.Any())
                throw new InvalidOperationException("Unable to have both a custom menu url and dynamically generated menu at the same time");

            //For v4.0.26.x we have to render this section differently depending on the theme, 
            //GCIntranet theme renders AppName and AppUrl seperately in GCWeb we render it as a List of Links. 
            return _core.WebTemplateTheme.ToLower() == "gcweb" ? RenderGCWebAppTop() : RenderGCIntranetApptop();
        }

        internal HtmlString RenderTransactionalTop()
        {
            return JsonSerializationHelper.SerializeToJson(new Top
            {

                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.WebTemplateSubTheme,
                IntranetTitle = _core.Builder.BuildIntranentTitleList(),
                Search = _core.ShowSearch,
                LngLinks = _core.Builder.BuildLanguageLinkList(),
                Breadcrumbs = _core.Builder.BuildBreadcrumbs(),
                ShowPreContent = false,
                LocalPath = _core.Builder.BuildLocalPath(),
                TopSecMenu = _core.LeftMenuItems.Any(),
                SiteMenu = false

            });
        }

        internal HtmlString RenderTop()
        {
            return JsonSerializationHelper.SerializeToJson(new Top
            {
                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.WebTemplateSubTheme,
                IntranetTitle = _core.Builder.BuildIntranentTitleList(),
                Search = _core.ShowSearch,
                LngLinks = _core.Builder.BuildLanguageLinkList(),
                ShowPreContent = _core.ShowPreContent,
                Breadcrumbs = _core.Builder.BuildBreadcrumbs(),
                LocalPath = _core.Builder.BuildLocalPath(),
                TopSecMenu = _core.LeftMenuItems.Any(),
                SiteMenu = true
            });
        }

        internal HtmlString RenderRefTop(bool isApplication)
        {
            return JsonSerializationHelper.SerializeToJson(new RefTop
            {
                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.WebTemplateSubTheme,
                JqueryEnv = _core.LoadJQueryFromGoogle ? "external" : null,
                LocalPath = _core.Builder.BuildLocalPath(),
                IsApplication = isApplication
            });
        }

        internal HtmlString RenderUnilingualPreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new UnilingualPreFooter
            {
                CdnEnv = _core.CDNEnvironment,
                PageDetails = false
            });
        }

        internal HtmlString RenderPreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new PreFooter
            {
                CdnEnv = _core.CDNEnvironment,
                DateModified = _core.Builder.BuildDateModified(),
                VersionIdentifier = _core.Builder.GetStringForJson(_core.VersionIdentifier),
                ShowPostContent = _core.ShowPostContent,
                ShowFeedback = new FeedbackLink
                {
                    Show = _core.ShowFeedbackLink,
                    URL = _core.TwoLetterCultureLanguage.StartsWith(Constants.FRENCH_ACCRONYM) && !string.IsNullOrEmpty(_core.FeedbackLinkUrlFr) ? _core.FeedbackLinkUrlFr : _core.FeedbackLinkUrl
                },
                ShowShare = new ShareList
                {
                    Show = _core.ShowSharePageLink,
                    Enums = _core.SharePageMediaSites
                },
                ScreenIdentifier = _core.Builder.GetStringForJson(_core.ScreenIdentifier)
            });
        }

        internal HtmlString RenderTransactionalPreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new PreFooter
            {
                CdnEnv = _core.CDNEnvironment,
                DateModified = _core.Builder.BuildDateModified(),
                VersionIdentifier = _core.Builder.GetStringForJson(_core.VersionIdentifier),
                ShowPostContent = false,
                ShowFeedback = new FeedbackLink { Show = false },
                ShowShare = new ShareList { Show = false },
                ScreenIdentifier = _core.Builder.GetStringForJson(_core.ScreenIdentifier)
            });
        }

        internal HtmlString RenderFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new Footer
            {
                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.WebTemplateSubTheme,
                ShowFooter = true,
                ContactLinks = _core.Builder.BuildContactLinks(),
                PrivacyLink = null,
                TermsLink = null

            });
        }

        internal HtmlString RenderTransactionalFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new Footer
            {
                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.WebTemplateSubTheme,
                ShowFooter = false,
                ContactLinks = _core.Builder.BuildContactLinks(),
                PrivacyLink = _core.Builder.GetStringForJson(_core.PrivacyLinkURL),
                TermsLink = _core.Builder.GetStringForJson(_core.TermsConditionsLinkURL)

            });
        }

        internal HtmlString RenderRefFooter()
        {
            if (_core.LeavingSecureSiteWarning.Enabled &&
                !string.IsNullOrEmpty(_core.LeavingSecureSiteWarning.RedirectURL))
            {
                return RenderSecureSiteWarningRefFooter();
            }
            return JsonSerializationHelper.SerializeToJson(new RefFooter
            {
                CdnEnv = _core.CDNEnvironment,
                ExitScript = false,
                JqueryEnv = _core.Builder.BuildJqueryEnv(),
                LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion)
            });
        }

        internal HtmlString RenderSessionTimeoutControl()
        {
            HtmlString jsonSessionTimeout = null;

            if (_core.SessionTimeout.Enabled)
            {
                jsonSessionTimeout = JsonSerializationHelper.SerializeToJson(_core.SessionTimeout);
                return new HtmlString($"<span class='wb-sessto' data-wb-sessto='{jsonSessionTimeout}'></span>");
            }

            return null;
        }

        internal HtmlString RenderLeftMenu()
        {
            if (!_core.LeftMenuItems.Any())
                return new HtmlString(string.Empty);

            var leftMenuForSerialization = new { sections = new List<object>() };

            // capitalization on anonymous types matters here, CDTS will reject the json objects if not done right
            foreach (var menu in _core.LeftMenuItems)
            {
                var menuForSerialization = new
                {
                    sectionName = WebUtility.HtmlEncode(menu.Name),
                    sectionLink = _core.Builder.GetStringForJson(menu.Link),
                    newWindow = menu.OpenInNewWindow ? true : (bool?)null, //so json won't render object on false
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
                            newWindow = item.OpenInNewWindow ? true : (bool?)null, //so json won't render object on false
                            subLinks = item.SubItems.Any() ? new List<object>() : null
                        };

                        foreach (var subMenuItem in item.SubItems)
                        {
                            subMenuForSerialization.subLinks.Add(new
                            {
                                subhref = subMenuItem.Href,
                                subtext = subMenuItem.Text,
                                newWindow = subMenuItem.OpenInNewWindow ? true : (bool?)null //so json won't render object on false
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
        internal HtmlString RenderHtmlElements(List<string> tags)
        {
            var sb = new StringBuilder();

            foreach (var tag in tags)
            {
                sb.AppendLine(tag);
            }
            return new HtmlString(sb.ToString());
        }

        private HtmlString RenderGCIntranetApptop()
        {
            return JsonSerializationHelper.SerializeToJson(new GCIntranetAppTop
            {
                AppName = new List<Link> { _core.ApplicationTitle },
                IntranetTitle = _core.Builder.BuildIntranentTitleList(),
                SignIn = _core.Builder.BuildHideableHrefOnlyLink(_core.SignInLinkURL, _core.ShowSignInLink),
                SignOut = _core.Builder.BuildHideableHrefOnlyLink(_core.SignOutLinkURL, _core.ShowSignOutLink),
                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.WebTemplateSubTheme,
                Search = _core.ShowSearch,
                LngLinks = _core.Builder.BuildLanguageLinkList(),
                ShowPreContent = _core.ShowPreContent,
                Breadcrumbs = _core.Builder.BuildBreadcrumbs(),
                LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion),
                AppSettings = _core.Builder.BuildHideableHrefOnlyLink(_core.AppSettingsURL, true),
                MenuPath = _core.CustomSiteMenuURL,
                CustomSearch = _core.CustomSearch,
                TopSecMenu = _core.LeftMenuItems.Any(),
                MenuLinks = _core.MenuLinks
            });
        }

        private HtmlString RenderGCWebAppTop()
        {
            return JsonSerializationHelper.SerializeToJson(new AppTop
            {
                AppName = new List<Link> { _core.ApplicationTitle },
                SignIn = _core.Builder.BuildHideableHrefOnlyLink(_core.SignInLinkURL, _core.ShowSignInLink),
                SignOut = _core.Builder.BuildHideableHrefOnlyLink(_core.SignOutLinkURL, _core.ShowSignOutLink),
                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.WebTemplateSubTheme,
                Search = _core.ShowSearch,
                LngLinks = _core.Builder.BuildLanguageLinkList(),
                ShowPreContent = _core.ShowPreContent,
                Breadcrumbs = _core.Builder.BuildBreadcrumbs(),
                LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion),
                AppSettings = _core.Builder.BuildHideableHrefOnlyLink(_core.AppSettingsURL, true),
                MenuPath = _core.CustomSiteMenuURL,
                CustomSearch = _core.CustomSearch,
                TopSecMenu = _core.LeftMenuItems.Any(),
                MenuLinks = _core.MenuLinks
            });
        }

        private HtmlString RenderSecureSiteWarningRefFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new RefFooter
            {
                CdnEnv = _core.CDNEnvironment,
                ExitScript = true,
                DisplayModal = _core.LeavingSecureSiteWarning.DisplayModalWindow,
                ExitURL = _core.LeavingSecureSiteWarning.RedirectURL,
                ExitMsg = WebUtility.HtmlEncode(_core.LeavingSecureSiteWarning.Message),
                ExitDomains = _core.Builder.GetStringForJson(_core.LeavingSecureSiteWarning.ExcludedDomains),
                JqueryEnv = _core.Builder.BuildJqueryEnv(),
                LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion)
            });

        }

        internal HtmlString RenderSplashInfo()
        {
            return JsonSerializationHelper.SerializeToJson(new 
            {
                cdnEnv = _core.CDNEnvironment,
                indexEng = _core.SplashPageInfo.EnglishHomeUrl,
                indexFra = _core.SplashPageInfo.FrenchHomeUrl,
                termsEng = _core.Builder.GetStringForJson(_core.SplashPageInfo.EnglishTermsUrl),
                termsFra = _core.Builder.GetStringForJson(_core.SplashPageInfo.FrenchTermsUrl),
                nameEng = _core.SplashPageInfo.EnglishName,
                nameFra = _core.SplashPageInfo.FrenchName
            });
        }
    }
}
