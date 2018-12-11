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
    public class GoCWebTemplateExtendedCtrlSampleController : ExtendedBaseController
    {
        public GoCWebTemplateExtendedCtrlSampleController(IStringLocalizer<Program> localizer, IMemoryCache memCache, IHostingEnvironment env, IOptions<GCConfigurations> config, IHttpContextAccessor context) : base(localizer, memCache, env, config, context)
        {
        }

        public ActionResult ExtendedCtrlSample()
        {

            //call the functions/properties of the extended controller
            ViewBag.lblSessionID = GetSessionID();
            ViewBag.lblWeather = GetWeather();

            return View();
        }
    }
}