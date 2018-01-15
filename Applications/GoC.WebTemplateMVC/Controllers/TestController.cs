using System.Web.Mvc;
using GoC.WebTemplate;

namespace GoC.WebTemplateMVC.Controllers
{
    public class TestController : WebTemplateBaseController
    {


        public ActionResult BilingualErrorPage()
        {
            WebTemplateCore.HeaderTitle = "Bilingual Error Test Page";
            return View();
        }

        public ActionResult UnilingualErrorPage()
        {
            WebTemplateCore.HeaderTitle = "Unilingual Error Test Page";
            return View();
        }
    }
}