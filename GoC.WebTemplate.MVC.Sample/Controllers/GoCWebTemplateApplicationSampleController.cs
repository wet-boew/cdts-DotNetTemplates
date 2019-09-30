using GoC.WebTemplate.Components.JSONSerializationObjects;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCWebTemplateApplicationSampleController : WebTemplateBaseController
    {

        public ActionResult StandardPage()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name Test";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowPostContent = false;
            WebTemplateCore.CustomSiteMenuURL =
                "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_25/cdts/ajax/custommenu-eng.html";
            WebTemplateCore.SignOutLinkURL = "about:blank";
            WebTemplateCore.ShowSignOutLink = true;
            WebTemplateCore.AppSettingsURL = "http://tempuri.com";

            WebTemplateCore.CustomSearch = new CustomSearch
            {
                Action = "http://hrsdc.prv/cgi-bin/recherche-search/Intraweb/index.aspx",
                // Id = "0001", optional
                Method = "get", // 'get' or 'post'
                Placeholder = "Search ESDC IntraWeb",
                HiddenInput = new List<KeyValuePair<string, string>> //optional
                {
                    new KeyValuePair<string, string>("GoCTemplateCulture", "en-CA"),
                    new KeyValuePair<string, string>("p1", "gc")
                }
            };
            
            return View();
        }

    }
}