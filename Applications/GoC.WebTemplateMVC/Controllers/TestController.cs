using GoC.WebTemplate.Components.JSONSerializationObjects;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Controllers
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
        public ActionResult SubThemeTestPage()
        {
            WebTemplateCore.Environment = "ESDC_PROD";
            WebTemplateCore.UseHTTPS = false;
            WebTemplateCore.HeaderTitle = "SubTheme Test Page";
            WebTemplateCore.IntranetTitle = new IntranetTitle
            {
                Acronym = "yoyo",
                BoldText = "BOLD",
                Href = "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/samples/subtheme-eccc-en.shtml",
                Text = "CDTS Example"
            };
            return View();
        }
    }
}