using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    public class TestLayoutsController : WebTemplateBaseController
    {
        //todo: abstact as a default constructor in base controller
        public TestLayoutsController(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
            : base(new CacheProvider<IDictionary<string, ICdtsEnvironment>>(memoryCache), new CacheProvider<string>(memoryCache), httpContextAccessor.HttpContext.Request)
        { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Application()
        {
            return View();
        }

        public IActionResult ApplicationLeftMenu()
        {
            return View();
        }

        public IActionResult BilingualErrorPage()
        {
            return View();
        }


    }
}
