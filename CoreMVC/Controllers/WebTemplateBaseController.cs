using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.Components.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    public class WebTemplateBaseController : Controller
    {
        protected Model WebTemplateModel { get; }

        public WebTemplateBaseController(ModelAccessor modelAccessor)
        {
            WebTemplateModel = modelAccessor.Model;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //update the herf link depending on the current culture keeping the rest of the querystring intact
            WebTemplateModel.LanguageLink.Href = ModelBuilder.BuildLanguageLinkURL(HttpContext.Request.QueryString.ToString());

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewData["WebTemplateModel"] = WebTemplateModel;
            base.OnActionExecuted(context);
        }
    }
}