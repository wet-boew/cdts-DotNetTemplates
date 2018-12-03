using CDTS_Core;
using CDTS_Core.WebTemplate;
using GoCCore.WebTemplate_MVC.Sample;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class ExtendedBaseController : WebTemplateBaseController
    {
        private readonly IStringLocalizer<Program> _localizer;
        
        public ExtendedBaseController(IStringLocalizer<Program> localizer, IMemoryCache memCache, IHostingEnvironment env, IOptions<GCConfigurations> config, IHttpContextAccessor context) : base(memCache, env, config, context.HttpContext.Request)
        {
            _localizer = localizer;
        }

        public string GetWeather()
        {
            // get data from source...
            // do some calculation...
            // etc....
            return "Sunny";
        }

        public string GetSessionID()
        {
            return ""; //Todo: Expose Session ID.
        }
        
    }
}