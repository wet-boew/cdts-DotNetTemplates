using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using GoC.WebTemplate.Components.JSONSerializationObjects;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components
{
    public class CoreBuilder
    {
        private readonly Core _core;
        public CoreBuilder(Core core)
        {
            _core = core;
        }
        /// <summary>
        /// Builds the URL to be used by the English/francais link at the top of the page for the language toggle.
        /// The method will add or update the "GoCTemplateCulture" querystring parameter with the culture to be set
        /// The language toggle link, posts back to the same page, and the InitializedCulture method of the BasePage is responsible for setting the culture with the provided value
        /// </summary>
        /// <returns>The URL to be used for the language toggle link</returns>
        public static string BuildLanguageLinkURL(string queryString)
        {
            System.Collections.Specialized.NameValueCollection nameValues = HttpUtility.ParseQueryString(queryString);

            //Set the value of the "GoCTemplateCulture" parameter
            nameValues.Set(Constants.QUERYSTRING_CULTURE_KEY,
                Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.StartsWith(Constants.ENGLISH_ACCRONYM,
                    StringComparison.OrdinalIgnoreCase)
                    ? Constants.FRENCH_CULTURE
                    : Constants.ENGLISH_CULTURE);

            string url = string.Concat("?", nameValues.ToString());

            return url;
        }

        internal List<Link> BuildContactLinks()
        {
            if (!_core.CurrentEnvironment.CanHaveMultipleContactLinks)
            {
                if (_core.ContactLinks?.Count > 1)
                    throw new InvalidOperationException("Having multiple contact links not allowed in this environment");

                if (_core.ContactLinks?.Count == 1 && !string.IsNullOrWhiteSpace(_core.ContactLinks[0]?.Text))
                    throw new InvalidOperationException("Unable to edit Contact Link text in this environment");
            }

            if (_core.ContactLinks.Any(link => string.IsNullOrWhiteSpace(link.Href)))
                throw new InvalidOperationException("Href must be specified");
            return _core.ContactLinks;

        }

        internal List<FooterLink> BuildSingleFooterLink(FooterLink link)
        {
            return string.IsNullOrWhiteSpace(link?.Href) ? null : new List<FooterLink> { link };
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

            return _core.Breadcrumbs.Select(b => new Breadcrumb
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
                if (!_core.FooterSections.Any() && !_core.CustomFooterLinks.Any()) return null;

                if (_core.CurrentEnvironment.FooterSectionLimit > 0)
                { // use FooterSections
                    if (!_core.FooterSections.Any())
                        throw new InvalidOperationException(
                            "The CustomFooterLinks cannot be used in this enviornment, please use the FooterSections");

                    if (_core.FooterSections.Count > _core.CurrentEnvironment.FooterSectionLimit)
                        throw new InvalidOperationException(
                            $"The maximum FooterSections allowed for this environment is {_core.CurrentEnvironment.FooterSectionLimit}");

                    _core.FooterSections.ForEach(fs => fs.CustomFooterLinks.ForEach(fl => fl.Text = GetStringForJson(fl.Text)));

                    return new List<IFooterSection>(_core.FooterSections);
                }
                else
                { // use CustomFooterLinks

                    if (!_core.CustomFooterLinks.Any())
                        throw new InvalidOperationException(
                            "The FooterSections cannot be used in this enviornment, please use the CustomFooterLinks");


                    return new List<IFooterSection>(_core.CustomFooterLinks.Select(fl => new FooterLink
                    {
                        Href = fl.Href,
                        NewWindow = fl.NewWindow,
                        Text = GetStringForJson(fl.Text)
                    }));
                }
            }
        }

        internal List<Link> BuildHideableHrefOnlyLink(string href, bool showLink)
        {
            if (!showLink || string.IsNullOrWhiteSpace(href))
                return null;
            return new List<Link> { new Link { Href = href, Text = null } };
        }
        internal string BuildJqueryEnv() => _core.LoadJQueryFromGoogle ? "external" : null;

        internal string BuildDateModified()
        {

            if (DateTime.Compare(_core.DateModified, DateTime.MinValue) == 0)
            {
                return null;
            }
            return _core.DateModified.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
        }

        internal List<Link> BuildIntranentTitleList() => _core.IntranetTitle == null ? null : new List<Link> { _core.IntranetTitle };

        internal List<LanguageLink> BuildLanguageLinkList()
        {
            if (!_core.ShowLanguageLink)
            {
                return null;
            }

            return new List<LanguageLink> {
                new LanguageLink {
                    Href = _core.LanguageLink.Href
                }
            };
        }

        /// <summary>
        /// Builds the path to the cdn based on the environment set in the config. The path is based on the url of the environment, theme and version
        /// </summary>
        /// <returns>String, the complete path to the cdn</returns>
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

            var https = string.Empty;
            if (_core.CurrentEnvironment.IsEncryptionModifiable)
            {
                //We've already checked to see if this is null before here so ignore this in resharper
                // ReSharper disable once PossibleInvalidOperationException
                https = _core.UseHTTPS.Value ? "s" : string.Empty;
            }

            var run = string.Empty;
            var version = string.Empty;
            if (string.IsNullOrWhiteSpace(_core.WebTemplateVersion))
            {
                if (_core.CurrentEnvironment.IsVersionRNCombined)
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
                version = _core.WebTemplateVersion + "/";
                run = "app";
            }

            return string.Format(CultureInfo.InvariantCulture, _core.CurrentEnvironment.Path, https, run, _core.WebTemplateTheme, version);
        }

        #region GetJson

        internal string GetStringForJson(string str) => string.IsNullOrWhiteSpace(str) ? null : str;

        internal string GetFormattedJsonString(string formatStr, params object[] strs) => string.IsNullOrWhiteSpace(formatStr) ? null : string.Format(CultureInfo.CurrentCulture, formatStr, strs);

        #endregion
    }
}
