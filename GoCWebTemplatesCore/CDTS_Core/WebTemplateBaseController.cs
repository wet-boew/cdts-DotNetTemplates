using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using CDTS_Core.WebTemplate;
using CDTS_Core.WebTemplateCore;
using CDTS_Core.WebTemplateCore.JsonSerializationObjects;
using CDTS_Core.WebTemplateCore.Proxies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CDTS_Core
{
    public class WebTemplateBaseController : Controller
    {
        protected Core WebTemplateCore { get; set; }

        public WebTemplateBaseController(IMemoryCache memCache, IHostingEnvironment env, IOptions<GCConfigurations> config, HttpRequest req)
            : this(new CurrentRequestProxy(req), new CacheProxy(memCache), config,
                new CDTSEnvironmentLoader(new CacheProxy(memCache)).LoadCDTSEnvironments(env, "~/CDTSEnvironments.json"))
        {
        }

        public WebTemplateBaseController(ICurrentRequestProxy requestProxy, ICacheProxy cacheProxy,
            IOptions<GCConfigurations> config, IDictionary<string, ICDTSEnvironment> cdtsEnvironments)
            : base()
        {
            WebTemplateCore = new Core(requestProxy, cacheProxy, config, cdtsEnvironments);
        }

        public override ViewResult View(string viewName, object model)
        {
            PopulateViewBag();
            return base.View(viewName, model);
        }

        public override ViewResult View(string viewName)
        {
            PopulateViewBag();
            return base.View(viewName);
        }

        public ISession get_Session()
        {
            return this.HttpContext.Session;
        }

        protected virtual void PopulateViewBag()
        {
            this.ViewBag.WebTemplateCore = WebTemplateCore;
            this.ViewBag.WebTemplateVersion =
                Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string text = this.Request.Query["GoCTemplateCulture"];
            string originaltext = text;
            if (string.IsNullOrEmpty(text))
            {
                if (this.HttpContext.Session != null)
                {
                    text = Convert.ToString(this.get_Session().GetString("GoC.Template.Culture"), CultureInfo.CurrentCulture);
                    if (string.IsNullOrEmpty(text))
                    {
                        text = "en-CA";
                        this.get_Session().SetString("GoC.Template.Culture", text);
                    }
                }
                else
                {
                    text = "en-CA";
                }
            }
            else
            {
                text = (text.StartsWith("en", StringComparison.CurrentCultureIgnoreCase) ? "en-CA" : "fr-CA");
                if (this.HttpContext.Session != null)
                {
                    this.get_Session().SetString("GoC.Template.Culture", text);
                }
            }

            if (!string.IsNullOrEmpty(text))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(text);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(text);
                CultureInfo.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            }

            if (!string.IsNullOrEmpty(originaltext))
            {
                
                var routeValues = context.RouteData.Values;
              
                context.Result = new RedirectToActionResult(routeValues["action"].ToString(), routeValues["controller"].ToString(), null);

                context.HttpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Thread.CurrentThread.CurrentCulture)),
                    new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true }); //Set the Language Cookie so .Net Core can process it with it's middleware.

                return;
            }

            WebTemplateCore.LanguageLink = new LanguageLink
            {
                Href = CoreBuilder.BuildLanguageLinkURL(new CurrentRequestProxy(this.Request).QueryString)
            };

            

            base.OnActionExecuting(context);
        }
    }
}