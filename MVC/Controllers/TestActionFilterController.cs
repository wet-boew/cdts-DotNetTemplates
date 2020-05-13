using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.MVC.ActionFilters;
using GoC.WebTemplate.MVC.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Controllers
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