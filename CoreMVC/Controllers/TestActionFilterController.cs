using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.CoreMVC.ActionFilters;
using GoC.WebTemplate.CoreMVC.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    [WebTemplateActionFilter]
    public sealed class TestActionFilterController : Controller
    {
        // GET: TestActionFilter/Default
        public ActionResult Default()
        {
            return View();
        }

        // GET: TestActionFilter/WithBreadCrumbs
        public ActionResult WithBreadCrumbs()
        {
            var webTemplateModel = ViewData.GetWebTemplateModel();

            webTemplateModel.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb{ Href = "https://www.canada.ca/en.html", Title = "GoC", Acronym = "Government of Canada"  },
                new Breadcrumb { Title = "My application" }
            };

            return View();
        }
    }
}