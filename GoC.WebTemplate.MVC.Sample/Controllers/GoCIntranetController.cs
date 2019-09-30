using GoC.WebTemplate.Components.JSONSerializationObjects;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCIntranetController : WebTemplateBaseController
    {               
        public ActionResult GCIntranetThemeSample()
        {
            //set up theme
            WebTemplateCore.Environment = "ESDC_PROD";
            WebTemplateCore.UseHTTPS = false;

            //custom intranet title
            WebTemplateCore.IntranetTitle = new IntranetTitle
            {
                Href = "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/samples/subtheme-esdc-en.shtml",
                BoldText = "ESDC Sub",
                Acronym = "Employment and Social Development Canada Sub Theme",
                Text = " Custom Title"
            };

            //application template title
            WebTemplateCore.ApplicationTitle.Text = "My Custom Title";
            WebTemplateCore.ApplicationTitle.Href = "http://iservice.prv/eng/index.shtml";

            return View();
        }
    }
}