using GoC.WebTemplate.Components.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCWebTemplateExtendedCtrlSampleController : ExtendedBaseController
    {
        public GoCWebTemplateExtendedCtrlSampleController(IModelAccessor modelAccessor)
            : base(modelAccessor) { }

        public IActionResult ExtendedCtrlSample()
        {            
            //call the functions/properties of the extended controller
            ViewData["Weather"] = GetWeather();
            ViewData["UserName"] = UserName;

            // ".." not reccommened but used to keep all samples in the same folder
            return View("../GoCWebTemplateSamples/ExtendedCtrlSample");
        }
	}
}