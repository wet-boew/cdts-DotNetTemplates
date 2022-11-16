using GoC.WebTemplate.Components.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCWebTemplateApplicationSampleController : WebTemplateBaseController
    {

        public ActionResult StandardPage()
        {
            WebTemplateModel.ApplicationTitle.Text = "Application Name Test";
            WebTemplateModel.Breadcrumbs = null;
            WebTemplateModel.LanguageLink.Href = "apptop-fr.html";
            WebTemplateModel.Settings.ShowLanguageLink = true;
            WebTemplateModel.Settings.ShowPostContent = false;
            WebTemplateModel.CustomSiteMenuURL =
                "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_25/cdts/ajax/custommenu-eng.html";
            WebTemplateModel.Settings.SignOutLinkUrl = "about:blank";
            WebTemplateModel.ShowSignOutLink = true;
            WebTemplateModel.AppSettingsURL = "http://tempuri.com";

            WebTemplateModel.CustomSearch = new CustomSearch
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