﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using System.Reflection;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.Components.Proxies;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.MVC
{
    public class WebTemplateBaseController : Controller
    {

        public WebTemplateBaseController() : this(new CurrentRequestProxy(),
            new CacheProxy(),
                                       new ConfigurationProxy(),
                                       new CDTSEnvironmentLoader(new CacheProxy()).LoadCDTSEnvironments("~/CDTSEnvironments.json"))
        { }

        public WebTemplateBaseController(ICurrentRequestProxy requestProxy, ICacheProxy cacheProxy, IConfigurationProxy configurationProxy, IDictionary<string, ICDTSEnvironment> cdtsEnvironments)
        {
            WebTemplateCore = new Core(requestProxy,
                cacheProxy,
                                   configurationProxy,
                                   cdtsEnvironments);

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
            ViewBag.WebTemplateCore = this.WebTemplateCore;
            ViewBag.WebTemplateVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
                Href = CoreBuilder.BuildLanguageLinkURL(new CurrentRequestProxy().QueryString)
            };

            return base.BeginExecuteCore(callback, state);
        }

        #region Properties
        protected Core WebTemplateCore { get; set; }
        #endregion

    }
}