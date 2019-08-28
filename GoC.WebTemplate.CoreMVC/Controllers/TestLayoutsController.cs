using GoC.WebTemplate.Components.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    public class TestLayoutsController : WebTemplateBaseController
    {
        public TestLayoutsController(ModelAccessor modelAccessor)
            : base(modelAccessor)
        {
        }

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
