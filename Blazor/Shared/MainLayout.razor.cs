
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Utils.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Specialized;
using System.Web;

namespace Blazor.Shared
{
    public partial class MainLayout
    {        /// <summary>
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

#pragma warning disable SYSLIB0013 // Type or member is obsolete
#pragma warning disable CS8604 // Possible null reference argument.
            string url = string.Concat("?", Uri.EscapeUriString(nameValues.ToString()));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore SYSLIB0013 // Type or member is obsolete

            return url;
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            /*topObj.LngLinks = new List<LanguageLink>
                {
                    new LanguageLink(){Href=BuildLanguageLinkURL(HttpUtility.ParseQueryString(""))}
                };*/

            //topObj.LngLinks[0].Href = "google";
        }

        public void AddLanguageLink()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            topObj.Search = true;
            topObj.LngLinks = new List<LanguageLink>
            {
                new LanguageLink(){Href=BuildLanguageLinkURL(HttpUtility.ParseQueryString(uri.Query))}
            };
        }

        private void RequestCultureChange()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var query = BuildLanguageLinkURL(HttpUtility.ParseQueryString(uri.Query));

            Navigation.NavigateTo("/Culture/SetCuture" + query, forceLoad: true);
        }

    }
}
