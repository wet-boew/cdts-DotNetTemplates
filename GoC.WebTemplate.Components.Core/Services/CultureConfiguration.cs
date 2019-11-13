using GoC.WebTemplate.Components.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GoC.WebTemplate.Components.Core.Services
{
    public static class CultureConfiguration
    {
        public static List<CultureInfo> SupportedCultures { get; set; } = new List<CultureInfo>
        {
            new CultureInfo(Constants.ENGLISH_CULTURE),
            new CultureInfo(Constants.FRENCH_CULTURE)
        };

        public static RequestLocalizationOptions GetLocalizationOptions()
        {
            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(Constants.ENGLISH_CULTURE),
                // Formatting numbers, dates, etc.
                SupportedCultures = SupportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = SupportedCultures
            };

            var qsrcp = (options.RequestCultureProviders.First(rcp => rcp is QueryStringRequestCultureProvider) as QueryStringRequestCultureProvider);
            if (qsrcp != null)
            {
                qsrcp.QueryStringKey = Constants.QUERYSTRING_CULTURE_KEY;
                qsrcp.UIQueryStringKey = Constants.QUERYSTRING_CULTURE_KEY;
            }

            return options;
        }
    }
}
