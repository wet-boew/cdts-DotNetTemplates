using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Core.Utils.CultureProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace GoC.WebTemplate.Components.Core.Services
{
    public static class CultureConfiguration
    {
        public static List<CultureInfo> SupportedCultures { get; set; } = new List<CultureInfo>
        {
            new CultureInfo(Constants.ENGLISH_CULTURE),
            new CultureInfo(Constants.FRENCH_CULTURE)
        };

        public static RequestCulture DefaultRequestCulture { get; set; } = new RequestCulture(Constants.ENGLISH_CULTURE);

        /// <summary>
        /// Generate a default instance of <see cref="RequestLocalizationOptions"/> ready for the GoC Template.
        /// </summary>
        /// <returns></returns>
        [System.Obsolete("This function is outdated, please implement the extention 'ConfigureGoCTemplateRequestLocalization' in 'ConfigureServices' instead")]
         public static RequestLocalizationOptions GetLocalizationOptions()
        {
            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = DefaultRequestCulture,
                // Formatting numbers, dates, etc.
                SupportedCultures = SupportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = SupportedCultures
            };

            // We put the custom GocTemplateCultureProvider before the existing 3 defaults .NET core culture provider. 
            // We just want to put the custom one first.
            options.RequestCultureProviders.Insert(0, new GocTemplateCultureProvider());

            return options;
        }


          /// <summary>
        /// Extension for configuring the <see cref="RequestLocalizationOptions"> based on the GoC Template.
        /// </summary>
        /// <param name="services">Current IServiceCollection instance.</param>
        public static void ConfigureGoCTemplateRequestLocalization(this IServiceCollection services)
        {
            //Options bound and configured by a delegate
            services.Configure<RequestLocalizationOptions>(options => 
            {
                options.DefaultRequestCulture = DefaultRequestCulture;
                // Formatting numbers, dates, etc.
                options.SupportedCultures = SupportedCultures;
                // UI strings that we have localized.
                options.SupportedUICultures = SupportedCultures;
                // Append our custom request localization provider.
                options.RequestCultureProviders.Insert(0, new GocTemplateCultureProvider());
            });
        }


    }
}
