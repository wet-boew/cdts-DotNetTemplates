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
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                var modelAccessor = (ModelAccessor)controller.HttpContext.RequestServices.GetService(typeof(ModelAccessor));
                var webTemplateModel = modelAccessor.Model;

                // Update the herf link depending on the current culture keeping the rest of the querystring intact
                webTemplateModel.LanguageLink.Href = ModelBuilder.BuildLanguageLinkURL(HttpUtility.ParseQueryString(context.HttpContext.Request.QueryString.ToString()));

                controller.ViewData["WebTemplateModel"] = webTemplateModel;
            }

            base.OnActionExecuting(context);
        }
    }
}