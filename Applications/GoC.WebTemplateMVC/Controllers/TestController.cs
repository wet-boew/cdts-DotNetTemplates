using System.Web.Mvc;
using GoC.WebTemplate;

namespace GoC.WebTemplateMVC.Controllers
{
    public class TestController : WebTemplateBaseController
    {


        public ActionResult BilingualErrorPage()
        {
            WebTemplateCore.HeaderTitle = "English | Francais";
            return View();
        }
    }
}