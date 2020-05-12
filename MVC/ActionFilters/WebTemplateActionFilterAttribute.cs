using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Framework.Utils;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Utils.Caching;
using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.ActionFilters
{
    public class WebTemplateActionFilterAttribute : ActionFilterAttribute
    {
        public WebTemplateActionFilterAttribute()
            : this(new FileContentCacheProvider(HttpRuntime.Cache),
                  new WebTemplateSettings(ConfigurationManager.GetSection("GoC.WebTemplate") as GocWebTemplateConfigurationSection),
                  new CdtsCacheProvider(HttpRuntime.Cache))
        {
        }

        public WebTemplateActionFilterAttribute(
            IFileContentCacheProvider fileContentCacheProvider,
            WebTemplateSettings settings,
            ICdtsCacheProvider cdtsCacheProvider)
        {
            if (fileContentCacheProvider is null) throw new ArgumentNullException(nameof(fileContentCacheProvider));
            if (settings is null) throw new ArgumentNullException(nameof(settings));
            if (cdtsCacheProvider is null) throw new ArgumentNullException(nameof(cdtsCacheProvider));

            WebTemplateModel = new Model(
                fileContentCacheProvider,
                settings,
                cdtsCacheProvider);
        }

        public Model WebTemplateModel { get; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Update the culture based on the query string or what is stored in session
            var langSwitcher = new LanguageSwitcher(new SessionController(System.Web.HttpContext.Current.Session));
            langSwitcher.UpdateCulture(filterContext.RequestContext.HttpContext.Request.QueryString.Get(Constants.QUERYSTRING_CULTURE_KEY));

            // Update the herf link depending on the current culture keeping the rest of the querystring intact
            WebTemplateModel.LanguageLink.Href = ModelBuilder.BuildLanguageLinkURL(HttpUtility.ParseQueryString(filterContext.RequestContext.HttpContext.Request.QueryString.ToString()));

            // Set timeout based on session
            WebTemplateModel.Settings.SessionTimeout.CheckWithServerSessionTimeout(System.Web.HttpContext.Current.Session);

            filterContext.Controller.ViewData["WebTemplateModel"] = WebTemplateModel;
            filterContext.Controller.ViewData["WebTemplateVersion"] = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            base.OnActionExecuting(filterContext);
        }
    }
}