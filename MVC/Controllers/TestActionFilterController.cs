using GoC.WebTemplate.MVC.ActionFilters;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Controllers
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