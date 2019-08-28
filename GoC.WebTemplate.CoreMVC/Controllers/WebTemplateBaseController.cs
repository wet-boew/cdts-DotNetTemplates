using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Utils.Caching;
using GoC.WebTemplate.Components.Configs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using GoC.WebTemplate.Components.Entities;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    public class WebTemplateBaseController : Controller
    {
        protected Model WebTemplateModel;

        public WebTemplateBaseController(IMemoryCache memoryCache, HttpRequest httpRequest)
            : this(httpRequest,
                  new FileContentMemoryCacheProvider(memoryCache), 
                  new ConfigurationProxy(), 
                  new CdtsMemoryCacheProvider(memoryCache))
        { }

        public WebTemplateBaseController(HttpRequest request, IFileContentCacheProvider fileContentCacheProvider, IConfigurationProxy configuration, ICdtsCacheProvider cdtsCacheProvider)
        {
            WebTemplateModel = new Model(fileContentCacheProvider, configuration, cdtsCacheProvider);

            //set the language link according to the culture
            WebTemplateModel.LanguageLink = new LanguageLink
            {
                Href = ModelBuilder.BuildLanguageLinkURL(request.QueryString.ToString())
            };
            //set timeout based on session
            WebTemplateModel.SessionTimeout.CheckWithServerSessionTimeout(request.HttpContext.Session);
        }

        public override ViewResult View()
        {
            AddViewData();
            return base.View();
        }
        public override ViewResult View(object model)
        {
            AddViewData();
            return base.View(model);
        }
        public override ViewResult View(string viewName)
        {
            AddViewData();
            return base.View(viewName);
        }
        public override ViewResult View(string viewName, object model)
        {
            AddViewData();
            return base.View(viewName, model);
        }

        private void AddViewData()
        {
            ViewData["WebTemplateModel"] = WebTemplateModel;
        }
    }
}