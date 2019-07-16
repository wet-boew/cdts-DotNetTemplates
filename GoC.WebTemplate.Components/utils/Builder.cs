using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Entities;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components
{
    public class CoreBuilder
    {
        private readonly Unit _unit;
        public CoreBuilder(Unit unit)
        {
            _unit = unit;
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
            if (!_unit.CurrentEnvironment.CanHaveMultiContactLinks)
            {
                if (_unit.ContactLinks?.Count > 1)
                    throw new InvalidOperationException("Having multiple contact links not allowed in this environment");

                if (_unit.ContactLinks?.Count == 1 && !string.IsNullOrWhiteSpace(_unit.ContactLinks[0]?.Text))
                    throw new InvalidOperationException("Unable to edit Contact Link text in this environment");
            }

            if (_unit.ContactLinks.Any(link => string.IsNullOrWhiteSpace(link.Href)))
                throw new InvalidOperationException("Href must be specified");
            return _unit.ContactLinks;

        }

        internal string BuildLocalPath()
        {
            return GetFormattedJsonString(_unit.LocalPath, _unit.WebTemplateTheme, _unit.WebTemplateVersion);
        }

        internal List<Breadcrumb> BuildBreadcrumbs()
        {
            if (_unit.Breadcrumbs == null || !_unit.Breadcrumbs.Any())
            {
                return null;
            }

            return _unit.Breadcrumbs.Select(b => new Breadcrumb
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
                if (!_unit.FooterSections.Any() && !_unit.CustomFooterLinks.Any()) return null;

                if (_unit.CurrentEnvironment.FooterSectionLimit > 0)
                { // use FooterSections
                    if (!_unit.FooterSections.Any())
                        throw new InvalidOperationException(
                            "The CustomFooterLinks cannot be used in this enviornment, please use the FooterSections");

                    if (_unit.FooterSections.Count > _unit.CurrentEnvironment.FooterSectionLimit)
                        throw new InvalidOperationException(
                            $"The maximum FooterSections allowed for this environment is {_unit.CurrentEnvironment.FooterSectionLimit}");

                    _unit.FooterSections.ForEach(fs => fs.CustomFooterLinks.ForEach(fl => fl.Text = GetStringForJson(fl.Text)));

                    return new List<IFooterSection>(_unit.FooterSections);
                }
                else
                { // use CustomFooterLinks

                    if (!_unit.CustomFooterLinks.Any())
                        throw new InvalidOperationException(
                            "The FooterSections cannot be used in this enviornment, please use the CustomFooterLinks");


                    return new List<IFooterSection>(_unit.CustomFooterLinks.Select(fl => new FooterLink
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
        internal string BuildJqueryEnv() => _unit.LoadJQueryFromGoogle ? "external" : null;

        internal string BuildDateModified()
        {

            if (DateTime.Compare(_unit.DateModified, DateTime.MinValue) == 0)
            {
                return null;
            }
            return _unit.DateModified.ToString("yyyy-MM-dd");
        }

        internal List<Link> BuildIntranentTitleList() => _unit.IntranetTitle == null ? null : new List<Link> { _unit.IntranetTitle };

        internal List<LanguageLink> BuildLanguageLinkList()
        {
            if (!_unit.ShowLanguageLink)
            {
                return null;
            }

            return new List<LanguageLink> {
                new LanguageLink {
                    Href = _unit.LanguageLink.Href
                }
            };
        }

        /// <summary>
        /// Builds the path to the cdn based on the environment set in the config. The path is based on the url of the environment, theme and version
        /// </summary>
        /// <returns>String, the complete path to the cdn</returns>
        internal string BuildCDNPath()
        {
            if (!_unit.CurrentEnvironment.IsEncryptionModifiable && _unit.UseHTTPS.HasValue)
            {
                throw new InvalidOperationException($"{_unit.Environment} does not allow useHTTPS to be toggled");
            }

            if (_unit.CurrentEnvironment.IsEncryptionModifiable && !_unit.UseHTTPS.HasValue)
            {
                throw new InvalidOperationException($"{_unit.Environment} requires useHTTPS to be true or false not null.");
            }

            var https = string.Empty;
            if (_unit.CurrentEnvironment.IsEncryptionModifiable)
            {
                //We've already checked to see if this is null before here so ignore this in resharper
                // ReSharper disable once PossibleInvalidOperationException
                https = _unit.UseHTTPS.Value ? "s" : string.Empty;
            }

            var run = string.Empty;
            var version = string.Empty;
            if (string.IsNullOrWhiteSpace(_unit.WebTemplateVersion))
            {
                if (_unit.CurrentEnvironment.IsVersionRNCombined)
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
                version = _unit.WebTemplateVersion + "/";
                run = "app";
            }

            return string.Format(CultureInfo.InvariantCulture, _unit.CurrentEnvironment.Path, https, run, _unit.WebTemplateTheme, version);
        }

        #region GetJson

        internal string GetStringForJson(string str) => string.IsNullOrWhiteSpace(str) ? null : str;

        internal string GetFormattedJsonString(string formatStr, params object[] strs) => string.IsNullOrWhiteSpace(formatStr) ? null : string.Format(formatStr, strs);

        #endregion
    }
}
