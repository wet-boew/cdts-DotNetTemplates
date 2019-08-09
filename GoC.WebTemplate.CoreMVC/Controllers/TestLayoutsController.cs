using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using GoC.WebTemplate.Components.Utils.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    public class TestLayoutsController : WebTemplateBaseController
    {
        public TestLayoutsController(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
            : base(memoryCache, httpContextAccessor.HttpContext.Request)
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
