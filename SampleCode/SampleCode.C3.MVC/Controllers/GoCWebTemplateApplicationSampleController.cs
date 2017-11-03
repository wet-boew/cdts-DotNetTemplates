using System.Collections.Generic;
using System.Web.Mvc;
using GoC.WebTemplate;

namespace SampleCode.C3.MVC.Controllers
{
    public class GoCWebTemplateApplicationSampleController : WebTemplateBaseController
    {

        public ActionResult StandardPage()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name Test";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowPostContent = false;
            WebTemplateCore.CustomSiteMenuURL =
                "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_25/cdts/ajax/custommenu-eng.html";
            WebTemplateCore.SignOutLinkURL = "about:blank";
            WebTemplateCore.ShowSignOutLink = true;
            WebTemplateCore.AppSettingsURL = "http://tempuri.com";

            return View();
        }

    }
}