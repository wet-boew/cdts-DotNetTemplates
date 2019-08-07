using Microsoft.AspNetCore.Mvc;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    public class TestLayoutsController : WebTemplateBaseController
    {
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
