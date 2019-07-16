using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Html;
using GoC.WebTemplate.Components.Entities;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Utils
{
    internal class CoreRenderer
    {
        private readonly Model _unit;
        internal CoreRenderer(Model unit)
        {
            _unit = unit;
        }

        internal HtmlString RenderAppFooter()
        {
            if (!_unit.CurrentEnvironment.CanHaveContactLinkInAppTemplate && _unit.ContactLinks.Any())
            {
                throw new InvalidOperationException("Please use a CustomFooter to add a contact link in this environment");
            }


            return JsonSerializationHelper.SerializeToJson(new AppFooter
            {
                CdnEnv = _unit.CDNEnvironment,
                SubTheme = _unit.Builder.GetStringForJson(_unit.WebTemplateSubTheme),
                TermsLink = _unit.Builder.GetStringForJson(_unit.TermsConditionsLinkURL),
                PrivacyLink = _unit.Builder.GetStringForJson(_unit.PrivacyLinkURL),
                ContactLink = _unit.Builder.BuildContactLinks(),
                LocalPath = _unit.Builder.GetFormattedJsonString(_unit.LocalPath, _unit.WebTemplateTheme, _unit.WebTemplateVersion),
                FooterSections = _unit.Builder.BuildCustomFooterSections
            });
        }

        internal HtmlString RenderAppTop()
        {
            if (_unit.ShowSignInLink && _unit.ShowSignOutLink)
                throw new InvalidOperationException("Unable to show sign in and sign out link together");
            if (_unit.CustomSiteMenuURL != null && _unit.MenuLinks != null && _unit.MenuLinks.Any())
                throw new InvalidOperationException("Unable to have both a custom menu url and dynamically generated menu at the same time");

            //For v4.0.26.x we have to render this section differently depending on the theme, 
            //GCIntranet theme renders AppName and AppUrl seperately in GCWeb we render it as a List of Links. 
            return _unit.WebTemplateTheme.ToLower() == "gcweb" ? RenderGCWebAppTop() : RenderGCIntranetApptop();
        }

        internal HtmlString RenderTransactionalTop()
        {
            return JsonSerializationHelper.SerializeToJson(new Top
            {

                CdnEnv = _unit.CDNEnvironment,
                SubTheme = _unit.WebTemplateSubTheme,
                IntranetTitle = _unit.Builder.BuildIntranentTitleList(),
                Search = _unit.ShowSearch,
                LngLinks = _unit.Builder.BuildLanguageLinkList(),
                Breadcrumbs = _unit.Builder.BuildBreadcrumbs(),
                ShowPreContent = false,
                LocalPath = _unit.Builder.BuildLocalPath(),
                TopSecMenu = _unit.LeftMenuItems.Any(),
                SiteMenu = false

            });
        }

        internal HtmlString RenderTop()
        {
            return JsonSerializationHelper.SerializeToJson(new Top
            {
                CdnEnv = _unit.CDNEnvironment,
                SubTheme = _unit.WebTemplateSubTheme,
                IntranetTitle = _unit.Builder.BuildIntranentTitleList(),
                Search = _unit.ShowSearch,
                LngLinks = _unit.Builder.BuildLanguageLinkList(),
                ShowPreContent = _unit.ShowPreContent,
                Breadcrumbs = _unit.Builder.BuildBreadcrumbs(),
                LocalPath = _unit.Builder.BuildLocalPath(),
                TopSecMenu = _unit.LeftMenuItems.Any(),
                SiteMenu = true
            });
        }

        internal HtmlString RenderRefTop(bool isApplication)
        {
            return JsonSerializationHelper.SerializeToJson(new RefTop
            {
                CdnEnv = _unit.CDNEnvironment,
                SubTheme = _unit.WebTemplateSubTheme,
                JqueryEnv = _unit.LoadJQueryFromGoogle ? "external" : null,
                LocalPath = _unit.Builder.BuildLocalPath(),
                IsApplication = isApplication
            });
        }

        internal HtmlString RenderUnilingualPreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new UnilingualPreFooter
            {
                CdnEnv = _unit.CDNEnvironment,
                PageDetails = false
            });
        }

        internal HtmlString RenderPreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new PreFooter
            {
                CdnEnv = _unit.CDNEnvironment,
                DateModified = _unit.Builder.BuildDateModified(),
                VersionIdentifier = _unit.Builder.GetStringForJson(_unit.VersionIdentifier),
                ShowPostContent = _unit.ShowPostContent,
                ShowFeedback = new FeedbackLink
                {
                    Show = _unit.ShowFeedbackLink,
                    URL = _unit.TwoLetterCultureLanguage.StartsWith(Constants.FRENCH_ACCRONYM) && !string.IsNullOrEmpty(_unit.FeedbackLinkUrlFr) ? _unit.FeedbackLinkUrlFr : _unit.FeedbackLinkUrl
                },
                ShowShare = new ShareList
                {
                    Show = _unit.ShowSharePageLink,
                    Enums = _unit.SharePageMediaSites
                },
                ScreenIdentifier = _unit.Builder.GetStringForJson(_unit.ScreenIdentifier)
            });
        }

        internal HtmlString RenderTransactionalPreFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new PreFooter
            {
                CdnEnv = _unit.CDNEnvironment,
                DateModified = _unit.Builder.BuildDateModified(),
                VersionIdentifier = _unit.Builder.GetStringForJson(_unit.VersionIdentifier),
                ShowPostContent = false,
                ShowFeedback = new FeedbackLink { Show = false },
                ShowShare = new ShareList { Show = false },
                ScreenIdentifier = _unit.Builder.GetStringForJson(_unit.ScreenIdentifier)
            });
        }

        internal HtmlString RenderFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new Footer
            {
                CdnEnv = _unit.CDNEnvironment,
                SubTheme = _unit.WebTemplateSubTheme,
                ShowFooter = true,
                ContactLinks = _unit.Builder.BuildContactLinks(),
                PrivacyLink = null,
                TermsLink = null

            });
        }

        internal HtmlString RenderTransactionalFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new Footer
            {
                CdnEnv = _unit.CDNEnvironment,
                SubTheme = _unit.WebTemplateSubTheme,
                ShowFooter = false,
                ContactLinks = _unit.Builder.BuildContactLinks(),
                PrivacyLink = _unit.Builder.GetStringForJson(_unit.PrivacyLinkURL),
                TermsLink = _unit.Builder.GetStringForJson(_unit.TermsConditionsLinkURL)

            });
        }

        internal HtmlString RenderRefFooter()
        {
            if (_unit.LeavingSecureSiteWarning.Enabled &&
                !string.IsNullOrEmpty(_unit.LeavingSecureSiteWarning.RedirectURL))
            {
                return RenderSecureSiteWarningRefFooter();
            }
            return JsonSerializationHelper.SerializeToJson(new RefFooter
            {
                CdnEnv = _unit.CDNEnvironment,
                ExitScript = false,
                JqueryEnv = _unit.Builder.BuildJqueryEnv(),
                LocalPath = _unit.Builder.GetFormattedJsonString(_unit.LocalPath, _unit.WebTemplateTheme, _unit.WebTemplateVersion)
            });
        }

        internal HtmlString RenderSessionTimeoutControl()
        {
            HtmlString jsonSessionTimeout = null;

            if (_unit.SessionTimeout.Enabled)
            {
                jsonSessionTimeout = JsonSerializationHelper.SerializeToJson(_unit.SessionTimeout);
                return new HtmlString($"<span class='wb-sessto' data-wb-sessto='{jsonSessionTimeout}'></span>");
            }

            return null;
        }

        internal HtmlString RenderLeftMenu()
        {
            if (!_unit.LeftMenuItems.Any())
                return new HtmlString(string.Empty);

            var leftMenuForSerialization = new { sections = new List<object>() };

            // capitalization on anonymous types matters here, CDTS will reject the json objects if not done right
            foreach (var menu in _unit.LeftMenuItems)
            {
                var menuForSerialization = new
                {
                    sectionName = WebUtility.HtmlEncode(menu.Name),
                    sectionLink = _unit.Builder.GetStringForJson(menu.Link),
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
            return JsonSerializationHelper.SerializeToJson(new AppTopGcIntranet
            {
                AppName = new List<Link> { _unit.ApplicationTitle },
                IntranetTitle = _unit.Builder.BuildIntranentTitleList(),
                SignIn = _unit.Builder.BuildHideableHrefOnlyLink(_unit.SignInLinkURL, _unit.ShowSignInLink),
                SignOut = _unit.Builder.BuildHideableHrefOnlyLink(_unit.SignOutLinkURL, _unit.ShowSignOutLink),
                CdnEnv = _unit.CDNEnvironment,
                SubTheme = _unit.WebTemplateSubTheme,
                Search = _unit.ShowSearch,
                LngLinks = _unit.Builder.BuildLanguageLinkList(),
                ShowPreContent = _unit.ShowPreContent,
                Breadcrumbs = _unit.Builder.BuildBreadcrumbs(),
                LocalPath = _unit.Builder.GetFormattedJsonString(_unit.LocalPath, _unit.WebTemplateTheme, _unit.WebTemplateVersion),
                AppSettings = _unit.Builder.BuildHideableHrefOnlyLink(_unit.AppSettingsURL, true),
                MenuPath = _unit.CustomSiteMenuURL,
                CustomSearch = _unit.CustomSearch,
                TopSecMenu = _unit.LeftMenuItems.Any(),
                MenuLinks = _unit.MenuLinks
            });
        }

        private HtmlString RenderGCWebAppTop()
        {
            return JsonSerializationHelper.SerializeToJson(new AppTop
            {
                AppName = new List<Link> { _unit.ApplicationTitle },
                SignIn = _unit.Builder.BuildHideableHrefOnlyLink(_unit.SignInLinkURL, _unit.ShowSignInLink),
                SignOut = _unit.Builder.BuildHideableHrefOnlyLink(_unit.SignOutLinkURL, _unit.ShowSignOutLink),
                CdnEnv = _unit.CDNEnvironment,
                SubTheme = _unit.WebTemplateSubTheme,
                Search = _unit.ShowSearch,
                LngLinks = _unit.Builder.BuildLanguageLinkList(),
                ShowPreContent = _unit.ShowPreContent,
                Breadcrumbs = _unit.Builder.BuildBreadcrumbs(),
                LocalPath = _unit.Builder.GetFormattedJsonString(_unit.LocalPath, _unit.WebTemplateTheme, _unit.WebTemplateVersion),
                AppSettings = _unit.Builder.BuildHideableHrefOnlyLink(_unit.AppSettingsURL, true),
                MenuPath = _unit.CustomSiteMenuURL,
                CustomSearch = _unit.CustomSearch,
                TopSecMenu = _unit.LeftMenuItems.Any(),
                MenuLinks = _unit.MenuLinks
            });
        }

        private HtmlString RenderSecureSiteWarningRefFooter()
        {
            return JsonSerializationHelper.SerializeToJson(new RefFooter
            {
                CdnEnv = _unit.CDNEnvironment,
                ExitScript = true,
                DisplayModal = _unit.LeavingSecureSiteWarning.DisplayModalWindow,
                ExitURL = _unit.LeavingSecureSiteWarning.RedirectURL,
                ExitMsg = WebUtility.HtmlEncode(_unit.LeavingSecureSiteWarning.Message),
                ExitDomains = _unit.Builder.GetStringForJson(_unit.LeavingSecureSiteWarning.ExcludedDomains),
                JqueryEnv = _unit.Builder.BuildJqueryEnv(),
                LocalPath = _unit.Builder.GetFormattedJsonString(_unit.LocalPath, _unit.WebTemplateTheme, _unit.WebTemplateVersion)
            });

        }

        internal HtmlString RenderSplashInfo()
        {
            return JsonSerializationHelper.SerializeToJson(new 
            {
                cdnEnv = _unit.CDNEnvironment,
                indexEng = _unit.SplashPageInfo.EnglishHomeUrl,
                indexFra = _unit.SplashPageInfo.FrenchHomeUrl,
                termsEng = _unit.Builder.GetStringForJson(_unit.SplashPageInfo.EnglishTermsUrl),
                termsFra = _unit.Builder.GetStringForJson(_unit.SplashPageInfo.FrenchTermsUrl),
                nameEng = _unit.SplashPageInfo.EnglishName,
                nameFra = _unit.SplashPageInfo.FrenchName
            });
        }
    }
}
