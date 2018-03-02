using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using WebTemplateCore.JSONSerializationObjects;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
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
            return JsonSerializationHelper.SerializeToJson(new AppFooter
            {
                CdnEnv = _core.CDNEnvironment,
                SubTheme = _core.Builder.GetStringForJson(_core.WebTemplateSubTheme),
                ShowFeatures = _core.ShowFeatures,
                TermsLink = _core.Builder.GetStringForJson(_core.TermsConditionsLinkURL),
                PrivacyLink = _core.Builder.GetStringForJson(_core.PrivacyLinkURL),
                ContactLink = _core.Builder.GetStringForJson(_core.ContactLink?.Href),
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
                    URL = _core.FeedbackLinkURL
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
                ShowFeatures = _core.ShowFeatures,
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
                ShowFeatures = _core.ShowFeatures,
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
            StringBuilder sb = new StringBuilder();
            //<span class='wb-sessto' data-wb-sessto='{"inactivity": 5000, "reactionTime": 30000, "sessionalive": 10000, "logouturl": "http://www.tsn.com", "refreshCallbackUrl": "http://www.cnn.com", "refreshOnClick": "33", "refreshLimit": 2, "method": "555", "additionalData": "666"}'></span>

            if (_core.SessionTimeout.Enabled)
            {
                sb.Append("<span class='wb-sessto' data-wb-sessto='{");
                sb.Append("\"inactivity\": ");
                sb.Append(_core.SessionTimeout.Inactivity);
                sb.Append(", \"reactionTime\": ");
                sb.Append(_core.SessionTimeout.ReactionTime);
                sb.Append(", \"sessionalive\": ");
                sb.Append(_core.SessionTimeout.SessionAlive);
                sb.Append(", \"logouturl\": \"");
                sb.Append(_core.SessionTimeout.LogoutUrl);
                sb.Append("\"");
                if (!string.IsNullOrEmpty(_core.SessionTimeout.RefreshCallbackUrl))
                {
                    sb.Append(", \"refreshCallbackUrl\": \"");
                    sb.Append(_core.SessionTimeout.RefreshCallbackUrl);
                    sb.Append("\"");
                }
                sb.Append(", \"refreshOnClick\": ");
                sb.Append(_core.SessionTimeout.RefreshOnClick.ToString().ToLower());

                if (_core.SessionTimeout.RefreshLimit > 0)
                {
                    sb.Append(", \"refreshLimit\": ");
                    sb.Append(_core.SessionTimeout.RefreshLimit);
                }
                if (!string.IsNullOrEmpty(_core.SessionTimeout.Method))
                {
                    sb.Append(", \"method\": \"");
                    sb.Append(_core.SessionTimeout.Method);
                    sb.Append("\"");
                }
                if (!string.IsNullOrEmpty(_core.SessionTimeout.AdditionalData))
                {
                    sb.Append(", \"additionalData\": \"");
                    sb.Append(_core.SessionTimeout.AdditionalData);
                    sb.Append("\"");
                }
                sb.Append("}'></span>");
            }
            return new HtmlString(sb.ToString());
        }

        internal HtmlString RenderLeftMenu()
        {
            StringBuilder sb = new StringBuilder();

            // sectionName: 'Section menu', menuLinks: [{ href: '#', text: 'Link 1' }, { href: '#', text: 'Link 2' }]"

            if (_core.LeftMenuItems.Count > 0)
            {
                sb.Append("sections: [");
                foreach (MenuSection menuSection in _core.LeftMenuItems)
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
                                MenuItem mi = (MenuItem)lk;

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
    }
}
