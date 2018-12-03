using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace CDTS_Core.WebTemplateCore
{
    public class CoreBuilder
    {
        private readonly Core _core;

        internal List<FooterLink> BuildCustomFooterLinks => _core.CustomFooterLinks?.Select((FooterLink fl) => new FooterLink
        {
            Href = fl.Href,
            NewWindow = fl.NewWindow,
            Text = GetStringForJson(fl.Text)
        }).ToList();

        public CoreBuilder(Core core)
        {
            _core = core;
        }

        public static string BuildLanguageLinkURL(string queryString)
        {
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(queryString);
            nameValueCollection.Set("GoCTemplateCulture", Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.StartsWith("en", StringComparison.OrdinalIgnoreCase) ? "fr-CA" : "en-CA");
            return "?" + nameValueCollection.ToString();
        }

        internal List<Link> BuildContactLinks()
        {
            if (!string.IsNullOrWhiteSpace(_core.ContactLink?.Href))
            {
                return new List<Link>
            {
                _core.ContactLink
            };
            }
            return null;
        }

        internal string BuildLocalPath()
        {
            return GetFormattedJsonString(_core.LocalPath, _core.WebTemplateTheme, _core.WebTemplateVersion);
        }

        internal List<Breadcrumb> BuildBreadcrumbs()
        {
            if (_core.Breadcrumbs == null || !_core.Breadcrumbs.Any())
            {
                return null;
            }
            return (from b in _core.Breadcrumbs
                    select new Breadcrumb
                    {
                        Href = GetStringForJson(b.Href),
                        Acronym = GetStringForJson(b.Acronym),
                        Title = GetStringForJson(b.Title)
                    }).ToList();
        }

        internal List<Link> BuildHideableHrefOnlyLink(string href, bool showLink)
        {
            if (!showLink || string.IsNullOrWhiteSpace(href))
            {
                return null;
            }
            return new List<Link>
        {
            new Link
            {
                Href = href,
                Text = null
            }
        };
        }

        internal string BuildJqueryEnv()
        {
            if (!_core.LoadJQueryFromGoogle)
            {
                return null;
            }
            return "external";
        }

        internal string BuildDateModified()
        {
            if (DateTime.Compare(_core.DateModified, DateTime.MinValue) == 0)
            {
                return null;
            }
            return _core.DateModified.ToString("yyyy-MM-dd");
        }

        internal List<Link> BuildIntranentTitleList()
        {
            if (_core.IntranetTitle != null)
            {
                return new List<Link>
            {
                _core.IntranetTitle
            };
            }
            return null;
        }

        internal List<LanguageLink> BuildLanguageLinkList()
        {
            if (!_core.ShowLanguageLink)
            {
                return null;
            }
            return new List<LanguageLink>
        {
            new LanguageLink
            {
                Href = _core.LanguageLink.Href
            }
        };
        }

        internal string BuildCDNPath()
        {
            if (!_core.CurrentEnvironment.IsEncryptionModifiable && _core.UseHTTPS.HasValue)
            {
                throw new InvalidOperationException($"{_core.Environment} does not allow useHTTPS to be toggled");
            }
            if (_core.CurrentEnvironment.IsEncryptionModifiable && !_core.UseHTTPS.HasValue)
            {
                throw new InvalidOperationException($"{_core.Environment} requires useHTTPS to be true or false not null.");
            }
            string text = string.Empty;
            if (_core.CurrentEnvironment.IsEncryptionModifiable)
            {
                text = (_core.UseHTTPS.Value ? "s" : string.Empty);
            }
            string text2 = string.Empty;
            string text3 = string.Empty;
            if (string.IsNullOrWhiteSpace(_core.WebTemplateVersion))
            {
                if (_core.CurrentEnvironment.IsVersionRNCombined)
                {
                    text3 = "rn/";
                }
                else
                {
                    text2 = "rn";
                }
            }
            else
            {
                text3 = _core.WebTemplateVersion + "/";
                text2 = "app";
            }
            return string.Format(CultureInfo.InvariantCulture, _core.CurrentEnvironment.Path, text, text2, _core.WebTemplateTheme, text3);
        }

        internal string GetStringForJson(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            return null;
        }

        internal string GetFormattedJsonString(string formatStr, params object[] strs)
        {
            if (!string.IsNullOrWhiteSpace(formatStr))
            {
                return string.Format(formatStr, strs);
            }
            return null;
        }
    }

}
