using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using GoC.WebTemplate.Components.Entities;
using System.Collections.Specialized;

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
        /// Builds the URL to be used by the English/francais link at the top of the page for the language toggle.
        /// The method will add or update the "GoCTemplateCulture" querystring parameter with the culture to be set
        /// The language toggle link, posts back to the same page, and the InitializedCulture method of the BasePage is responsible for setting the culture with the provided value
        /// </summary>
        /// <returns>The URL to be used for the language toggle link</returns>
        public static string BuildLanguageLinkURL(NameValueCollection nameValues)
        {
            //make it writeable
           // var nameValues = new NameValueCollection(queryString.ToString());

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

        internal List<FooterLink> BuildSingleFooterLink(FooterLink link)
        {
            return string.IsNullOrWhiteSpace(link?.Href) ? null : new List<FooterLink> { link };
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

        internal List<Link> BuildHideableHrefOnlyLink(string href, bool showLink)
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

        #region GetJson

        internal string GetStringForJson(string str) => string.IsNullOrWhiteSpace(str) ? null : str;

        internal string GetFormattedJsonString(string formatStr, params object[] strs) => string.IsNullOrWhiteSpace(formatStr) ? null : string.Format(CultureInfo.CurrentCulture, formatStr, strs);

        #endregion
    }
}
