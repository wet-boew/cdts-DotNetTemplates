using GoC.WebTemplate.Components.Entities;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCIntranetController : WebTemplateBaseController
    {               
        public ActionResult GCIntranetThemeSample()
        {
            //set up theme
            WebTemplateModel.Settings.Environment = "ESDC_PROD";
            WebTemplateModel.Settings.UseHttps = false;

            //custom intranet title
            WebTemplateModel.IntranetTitle = new IntranetTitle
            {
                Href = "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/samples/subtheme-esdc-en.shtml",
                BoldText = "ESDC Sub",
                Acronym = "Employment and Social Development Canada Sub Theme",
                Text = " Custom Title"
            };

            //application template title
            WebTemplateModel.ApplicationTitle.Text = "My Custom Title";
            WebTemplateModel.ApplicationTitle.Href = "http://iservice.prv/eng/index.shtml";

            return View();
        }
    }
}