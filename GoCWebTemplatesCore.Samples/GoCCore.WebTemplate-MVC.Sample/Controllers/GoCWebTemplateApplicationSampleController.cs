using CDTS_Core;
using CDTS_Core.WebTemplate;
using GoCCore.WebTemplate_MVC.Sample;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCWebTemplateApplicationSampleController : WebTemplateBaseController
    {
        private readonly IStringLocalizer<Program> _localizer;

        public GoCWebTemplateApplicationSampleController(IStringLocalizer<Program> localizer, IMemoryCache memCache, IHostingEnvironment env, IOptions<GCConfigurations> config, IHttpContextAccessor context) : base(memCache, env, config, context.HttpContext.Request)
        {
            _localizer = localizer;
        }

        [Route("/")]
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

            return View();
        }

    }
}