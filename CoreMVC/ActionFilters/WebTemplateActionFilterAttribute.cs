using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.Components.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Web;

namespace GoC.WebTemplate.CoreMVC.ActionFilters
{
    public class WebTemplateActionFilter : ActionFilterAttribute
    {
        private Model WebTemplateModel { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                var modelAccessor = (ModelAccessor)controller.HttpContext.RequestServices.GetService(typeof(ModelAccessor));
                WebTemplateModel = modelAccessor.Model;

                // Update the herf link depending on the current culture keeping the rest of the querystring intact
                WebTemplateModel.LanguageLink.Href = ModelBuilder.BuildLanguageLinkURL(HttpUtility.ParseQueryString(context.HttpContext.Request.QueryString.ToString()));
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller controller)
            {
                controller.ViewData["WebTemplateModel"] = WebTemplateModel;
            }

            base.OnActionExecuted(context);
        }
    }
}