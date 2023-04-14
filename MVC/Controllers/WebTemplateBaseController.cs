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

namespace GoC.WebTemplate.MVC
{
    public class WebTemplateBaseController : Controller
    {

        public WebTemplateBaseController()
            : this(new FileContentCacheProvider(HttpRuntime.Cache),
                  new WebTemplateSettings(ConfigurationManager.GetSection("GoC.WebTemplate") as GocWebTemplateConfigurationSection),
                  new CdtsCacheProvider(HttpRuntime.Cache),
                  new CdtsSRIHashesCacheProvider(HttpRuntime.Cache))
        { }

        public WebTemplateBaseController(IFileContentCacheProvider fileContentCacheProvider,
            IWebTemplateSettings settings,
            ICdtsCacheProvider cdtsCacheProvider,
            ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            WebTemplateModel = 
                new Model(
                    fileContentCacheProvider, 
                    settings, 
                    cdtsCacheProvider,
                    cdtsSRIHashesCacheProvider
                );
        }

        /// <summary>
        /// Method is overridden to allows us to add the web template data/info to the viewbag
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="masterName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override ViewResult View(string viewName, string masterName, object model)
        {
            PopulateViewBag();
            return base.View(viewName, masterName, model);
        }
        protected override ViewResult View(IView view, object model)
        {
            PopulateViewBag();
            return base.View(view, model);
        }
        /// <summary>
        /// Method that adds the info to the ViewBag. The viewbag is used by the layout files.
        /// </summary>
        protected virtual void PopulateViewBag()
        {

            ViewData["WebTemplateModel"] = WebTemplateModel;
            ViewData["WebTemplateVersion"] = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        /// <summary>
        /// Method is executed for every action.  It is used to control the culture(language) of the site
        /// It also instantiates the Code object
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            //update the culture based on the query string or what is stored in session
            var langSwitcher = new LanguageSwitcher(new SessionController(System.Web.HttpContext.Current.Session));
            langSwitcher.UpdateCulture(Request.QueryString.Get(Constants.QUERYSTRING_CULTURE_KEY));
            //update the herf link depending on the current culture keeping the rest of the querystring intact
            WebTemplateModel.LanguageLink.Href = ModelBuilder.BuildLanguageLinkURL(HttpUtility.ParseQueryString(Request.QueryString.ToString()));

            //set timeout based on session
            WebTemplateModel.Settings.SessionTimeout.CheckWithServerSessionTimeout(System.Web.HttpContext.Current.Session);
            
            return base.BeginExecuteCore(callback, state);
        }

        protected IModel WebTemplateModel { get; set; }
    }
}