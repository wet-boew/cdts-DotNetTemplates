using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Utils.Caching;
using GoC.WebTemplate.Components.Configs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    public class WebTemplateBaseController : Controller
    {
        protected Model WebTemplateModel;

        public WebTemplateBaseController(ICacheProvider<IDictionary<string, ICdtsEnvironment>> cdtsCacheProvider, ICacheProvider<string> cacheProvider, HttpRequest httpRequest)
            : this(new CurrentRequest(httpRequest),
                  cacheProvider, 
                  new ConfigurationProxy(), 
                  new CdtsEnvironmentCache(cdtsCacheProvider).GetContent())
        { }

        public WebTemplateBaseController(ICurrentRequest request, ICacheProvider<string> cache, IConfigurationProxy configuration, IDictionary<string, ICdtsEnvironment> cdtsEnvironment)
        {
            WebTemplateModel = new Model(request, cache, configuration, cdtsEnvironment);
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