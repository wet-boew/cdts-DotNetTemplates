using GoC.WebTemplate.CoreMVC.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    [WebTemplateActionFilter]
    public sealed class TestActionFilterController : Controller
    {
        // GET: TestActionFilter
        public ActionResult Default()
        {
            return View();
        }
    }
}