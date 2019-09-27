using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Utils.Caching;
using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.MVC
{
    public class WebTemplateBaseController : Controller
    {

        public WebTemplateBaseController()
            : this(new FileContentCacheProvider(HttpRuntime.Cache),
                  new WebTemplateSettings(ConfigurationManager.GetSection("GoC.WebTemplate") as GocWebTemplateConfigurationSection),
                  new CdtsCacheProvider(HttpRuntime.Cache))
        { }

        public WebTemplateBaseController(IFileContentCacheProvider fileContentCacheProvider,
            WebTemplateSettings settings,
            ICdtsCacheProvider cdtsCacheProvider)
        {
            WebTemplateCore = 
                new Model(
                    fileContentCacheProvider, 
                    settings, 
                    cdtsCacheProvider
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

            ViewData["WebTemplateModel"] = WebTemplateCore;
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
            //Get culture from Querystring
            string culture = this.Request.QueryString.Get(Constants.QUERYSTRING_CULTURE_KEY);

            if ((string.IsNullOrEmpty(culture)))
            {
                if (this.HttpContext.Session != null)
                {
                    //culture not found in querystring, check session
                    culture = Convert.ToString(Session[Constants.SESSION_CULTURE_KEY], CultureInfo.CurrentCulture);

                    if ((string.IsNullOrEmpty(culture)))
                    {
                        //culture not found in session, use default language
                        culture = Constants.ENGLISH_CULTURE;
                        this.Session[Constants.SESSION_CULTURE_KEY] = culture;
                    }
                }
                else
                {
                    culture = Constants.ENGLISH_CULTURE;
                }
            }
            else
            {
                //culture found in querystring, use it
                culture = culture.StartsWith(Constants.ENGLISH_ACCRONYM, StringComparison.CurrentCultureIgnoreCase) ? Constants.ENGLISH_CULTURE : Constants.FRENCH_CULTURE;
                if (this.HttpContext.Session != null)
                    this.Session[Constants.SESSION_CULTURE_KEY] = culture;
            }

            //If we have a culture, set it
            if (!string.IsNullOrEmpty(culture))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            }

            //set the language link according to the culture
            WebTemplateCore.LanguageLink = new LanguageLink
            {
                Href = ModelBuilder.BuildLanguageLinkURL(System.Web.HttpContext.Current.Request.QueryString.ToString())
            };

            //set timeout based on session
            WebTemplateCore.Settings.SessionTimeout.CheckWithServerSessionTimeout(System.Web.HttpContext.Current.Session);
            
            return base.BeginExecuteCore(callback, state);
        }

        #region Properties
        protected Model WebTemplateCore { get; set; }
        #endregion

    }
}