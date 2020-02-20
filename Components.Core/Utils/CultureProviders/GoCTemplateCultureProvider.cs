using GoC.WebTemplate.Components.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace GoC.WebTemplate.Components.Core.Utils.CultureProviders
{
    /// <summary>
    /// Custom <see cref="RequestCultureProvider"/> created for making the GoCTemplateCulture switch language persistant without impacting the current .NET core culture providers beheviors.
    /// The <see cref="GocTemplateCultureProvider"/> will put the requested culture into the cookies to make it persistent for the next http requests.
    /// The cookies must be enabled if we want to set the culture properly.
    /// </summary>
    public class GocTemplateCultureProvider : RequestCultureProvider
    {
        /// <summary>
        /// Gets or sets the GoCTemplate culture key from the query string.
        /// </summary>
        public string GoCTemplateCulture { get; set; } = Constants.QUERYSTRING_CULTURE_KEY;

        /// <summary>
        /// That method will set the culture persistent inside the cookies.
        /// </summary>
        /// <param name="httpContext">Current injected instance of the <see cref="HttpContext"/>.</param>
        /// <returns>Returns the <see cref="ProviderCultureResult"/> instance. 
        /// If the GoCTemplate culture has been provided inside the URL, it set the culture inside the cookie.
        /// If it's not provided, it send a null <see cref="ProviderCultureResult"/> so it stay as it is currently.
        /// </returns>
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var locOptions = httpContext.RequestServices.GetService<IOptions<RequestLocalizationOptions>>();
            if (locOptions == null) throw new ArgumentNullException(nameof(locOptions));

            var cultureQuery = httpContext.Request.Query[GoCTemplateCulture];

            if (!string.IsNullOrEmpty(cultureQuery))
            {
                var requestedCultureName = cultureQuery.ToString();

                // Check if the request culture is part of the authorized one.
                var requestedCulture = locOptions.Value.SupportedCultures.FirstOrDefault(x => x.Name.ToUpper() == requestedCultureName.ToUpper());

                if (requestedCulture == null)
                {
                    requestedCultureName = locOptions.Value.DefaultRequestCulture.Culture.Name.ToString();
                }

                httpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(requestedCultureName)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

                return Task.FromResult(new ProviderCultureResult(requestedCultureName));
            }

            return Task.FromResult((ProviderCultureResult)null);
        }
    }
}
