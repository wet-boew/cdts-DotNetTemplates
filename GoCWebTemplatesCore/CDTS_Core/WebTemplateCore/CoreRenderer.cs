using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CDTS_Core.WebTemplateCore.JsonSerializationObjects;
using Microsoft.AspNetCore.Html;

namespace CDTS_Core.WebTemplateCore
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
            AppFooter appFooter = new AppFooter();
            appFooter.CdnEnv = _core.CDNEnvironment;
            appFooter.SubTheme = _core.Builder.GetStringForJson(_core.WebTemplateSubTheme);
            appFooter.ShowFeatures = _core.ShowFeatures;
            appFooter.TermsLink = _core.Builder.GetStringForJson(_core.TermsConditionsLinkURL);
            appFooter.PrivacyLink = _core.Builder.GetStringForJson(_core.PrivacyLinkURL);
            appFooter.ContactLink = _core.Builder.GetStringForJson(_core.ContactLink?.Href);
            appFooter.LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion);
            appFooter.FooterSections = _core.Builder.BuildCustomFooterLinks;
            return JsonSerializationHelper.SerializeToJson(appFooter);
        }

        internal HtmlString RenderAppTop()
        {
            if (_core.ShowSignInLink && _core.ShowSignOutLink)
            {
                throw new InvalidOperationException("Unable to show sign in and sign out link together");
            }
            if (_core.CustomSiteMenuURL != null && _core.MenuLinks != null && _core.MenuLinks.Any())
            {
                throw new InvalidOperationException("Unable to have both a custom menu url and dynamically generated menu at the same time");
            }
            if (!(_core.WebTemplateTheme.ToLower() == "gcweb"))
            {
                return RenderGCIntranetApptop();
            }
            return RenderGCWebAppTop();
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
                JqueryEnv = (_core.LoadJQueryFromGoogle ? "external" : null),
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
                ShowFeedback = new FeedbackLink
                {
                    Show = false
                },
                ShowShare = new ShareList
                {
                    Show = false
                },
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
            if (_core.LeavingSecureSiteWarning.Enabled && !string.IsNullOrEmpty(_core.LeavingSecureSiteWarning.RedirectURL))
            {
                return RenderSecureSiteWarningRefFooter();
            }
            RefFooter refFooter = new RefFooter();
            refFooter.CdnEnv = _core.CDNEnvironment;
            refFooter.ExitScript = false;
            refFooter.JqueryEnv = _core.Builder.BuildJqueryEnv();
            refFooter.LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion);
            return JsonSerializationHelper.SerializeToJson(refFooter);
        }

        internal HtmlString RenderSessionTimeoutControl()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (_core.SessionTimeout.Enabled)
            {
                stringBuilder.Append("<span class='wb-sessto' data-wb-sessto='{");
                stringBuilder.Append("\"inactivity\": ");
                stringBuilder.Append(_core.SessionTimeout.Inactivity);
                stringBuilder.Append(", \"reactionTime\": ");
                stringBuilder.Append(_core.SessionTimeout.ReactionTime);
                stringBuilder.Append(", \"sessionalive\": ");
                stringBuilder.Append(_core.SessionTimeout.SessionAlive);
                stringBuilder.Append(", \"logouturl\": \"");
                stringBuilder.Append(_core.SessionTimeout.LogoutUrl);
                stringBuilder.Append("\"");
                if (!string.IsNullOrEmpty(_core.SessionTimeout.RefreshCallbackUrl))
                {
                    stringBuilder.Append(", \"refreshCallbackUrl\": \"");
                    stringBuilder.Append(_core.SessionTimeout.RefreshCallbackUrl);
                    stringBuilder.Append("\"");
                }
                stringBuilder.Append(", \"refreshOnClick\": ");
                stringBuilder.Append(_core.SessionTimeout.RefreshOnClick.ToString().ToLower());
                if (_core.SessionTimeout.RefreshLimit > 0)
                {
                    stringBuilder.Append(", \"refreshLimit\": ");
                    stringBuilder.Append(_core.SessionTimeout.RefreshLimit);
                }
                if (!string.IsNullOrEmpty(_core.SessionTimeout.Method))
                {
                    stringBuilder.Append(", \"method\": \"");
                    stringBuilder.Append(_core.SessionTimeout.Method);
                    stringBuilder.Append("\"");
                }
                if (!string.IsNullOrEmpty(_core.SessionTimeout.AdditionalData))
                {
                    stringBuilder.Append(", \"additionalData\": \"");
                    stringBuilder.Append(_core.SessionTimeout.AdditionalData);
                    stringBuilder.Append("\"");
                }
                stringBuilder.Append("}'></span>");
            }
            return new HtmlString(stringBuilder.ToString());
        }

        internal HtmlString RenderLeftMenu()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (_core.LeftMenuItems.Count > 0)
            {
                stringBuilder.Append("sections: [");
                foreach (MenuSection leftMenuItem in _core.LeftMenuItems)
                {
                    stringBuilder.Append(" {sectionName: '");
                    stringBuilder.Append(WebUtility.HtmlEncode(leftMenuItem.Name));
                    stringBuilder.Append("',");
                    if (!string.IsNullOrEmpty(leftMenuItem.Link))
                    {
                        stringBuilder.Append(" sectionLink: '");
                        stringBuilder.Append(WebUtility.HtmlEncode(leftMenuItem.Link));
                        stringBuilder.Append("',");
                        if (leftMenuItem.OpenInNewWindow)
                        {
                            stringBuilder.Append("newWindow: true,");
                        }
                    }
                    if (leftMenuItem.Items.Count > 0)
                    {
                        stringBuilder.Append(" menuLinks: [");
                        foreach (Link item in leftMenuItem.Items)
                        {
                            stringBuilder.Append("{href: '");
                            stringBuilder.Append(item.Href);
                            stringBuilder.Append("', text: '");
                            stringBuilder.Append(WebUtility.HtmlEncode(item.Text));
                            stringBuilder.Append("'");
                            if (item is MenuItem)
                            {
                                MenuItem menuItem = (MenuItem)item;
                                if (menuItem.OpenInNewWindow)
                                {
                                    stringBuilder.Append(", newWindow: true");
                                }
                                if (menuItem.SubItems.Count > 0)
                                {
                                    stringBuilder.Append(", subLinks: [");
                                    foreach (MenuItem subItem in menuItem.SubItems)
                                    {
                                        stringBuilder.Append("{subhref: '");
                                        stringBuilder.Append(subItem.Href);
                                        stringBuilder.Append("', subtext: '");
                                        stringBuilder.Append(WebUtility.HtmlEncode(subItem.Text));
                                        stringBuilder.Append("',");
                                        if (subItem.OpenInNewWindow)
                                        {
                                            stringBuilder.Append(" newWindow: true");
                                        }
                                        stringBuilder.Append("},");
                                    }
                                    stringBuilder.Append("]");
                                }
                            }
                            stringBuilder.Append("},");
                        }
                        stringBuilder.Append("]");
                    }
                    stringBuilder.Append("},");
                }
                stringBuilder.Append("]");
            }
            return new HtmlString(stringBuilder.ToString());
        }

        internal HtmlString RenderHtmlElements(List<string> tags)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string tag in tags)
            {
                stringBuilder.AppendLine(tag);
            }
            return new HtmlString(stringBuilder.ToString());
        }

        private HtmlString RenderGCIntranetApptop()
        {
            GCIntranetAppTop gCIntranetAppTop = new GCIntranetAppTop();
            gCIntranetAppTop.AppName = new List<Link>
        {
            _core.ApplicationTitle
        };
            gCIntranetAppTop.IntranetTitle = _core.Builder.BuildIntranentTitleList();
            gCIntranetAppTop.SignIn = _core.Builder.BuildHideableHrefOnlyLink(_core.SignInLinkURL, _core.ShowSignInLink);
            gCIntranetAppTop.SignOut = _core.Builder.BuildHideableHrefOnlyLink(_core.SignOutLinkURL, _core.ShowSignOutLink);
            gCIntranetAppTop.CdnEnv = _core.CDNEnvironment;
            gCIntranetAppTop.SubTheme = _core.WebTemplateSubTheme;
            gCIntranetAppTop.Search = _core.ShowSearch;
            gCIntranetAppTop.LngLinks = _core.Builder.BuildLanguageLinkList();
            gCIntranetAppTop.ShowPreContent = _core.ShowPreContent;
            gCIntranetAppTop.Breadcrumbs = _core.Builder.BuildBreadcrumbs();
            gCIntranetAppTop.LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion);
            gCIntranetAppTop.AppSettings = _core.Builder.BuildHideableHrefOnlyLink(_core.AppSettingsURL, showLink: true);
            gCIntranetAppTop.MenuPath = _core.CustomSiteMenuURL;
            gCIntranetAppTop.CustomSearch = _core.CustomSearch;
            gCIntranetAppTop.TopSecMenu = _core.LeftMenuItems.Any();
            gCIntranetAppTop.MenuLinks = _core.MenuLinks;
            return JsonSerializationHelper.SerializeToJson(gCIntranetAppTop);
        }

        private HtmlString RenderGCWebAppTop()
        {
            AppTop appTop = new AppTop();
            appTop.AppName = new List<Link>
        {
            _core.ApplicationTitle
        };
            appTop.SignIn = _core.Builder.BuildHideableHrefOnlyLink(_core.SignInLinkURL, _core.ShowSignInLink);
            appTop.SignOut = _core.Builder.BuildHideableHrefOnlyLink(_core.SignOutLinkURL, _core.ShowSignOutLink);
            appTop.CdnEnv = _core.CDNEnvironment;
            appTop.SubTheme = _core.WebTemplateSubTheme;
            appTop.Search = _core.ShowSearch;
            appTop.LngLinks = _core.Builder.BuildLanguageLinkList();
            appTop.ShowPreContent = _core.ShowPreContent;
            appTop.Breadcrumbs = _core.Builder.BuildBreadcrumbs();
            appTop.LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion);
            appTop.AppSettings = _core.Builder.BuildHideableHrefOnlyLink(_core.AppSettingsURL, showLink: true);
            appTop.MenuPath = _core.CustomSiteMenuURL;
            appTop.CustomSearch = _core.CustomSearch;
            appTop.TopSecMenu = _core.LeftMenuItems.Any();
            appTop.MenuLinks = _core.MenuLinks;
            return JsonSerializationHelper.SerializeToJson(appTop);
        }

        private HtmlString RenderSecureSiteWarningRefFooter()
        {
            RefFooter refFooter = new RefFooter();
            refFooter.CdnEnv = _core.CDNEnvironment;
            refFooter.ExitScript = true;
            refFooter.DisplayModal = _core.LeavingSecureSiteWarning.DisplayModalWindow;
            refFooter.ExitURL = _core.LeavingSecureSiteWarning.RedirectURL;
            refFooter.ExitMsg = WebUtility.HtmlEncode(_core.LeavingSecureSiteWarning.Message);
            refFooter.ExitDomains = _core.Builder.GetStringForJson(_core.LeavingSecureSiteWarning.ExcludedDomains);
            refFooter.JqueryEnv = _core.Builder.BuildJqueryEnv();
            refFooter.LocalPath = _core.Builder.GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion);
            return JsonSerializationHelper.SerializeToJson(refFooter);
        }
    }
}
