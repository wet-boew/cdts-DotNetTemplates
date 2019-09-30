using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCWebTemplateExtendedCtrlSampleController : ExtendedBaseController
    {

        public ActionResult ExtendedCtrlSample()
        {
            
            //call the functions/properties of the extended controller
            ViewBag.lblSessionID = SessionID;
            ViewBag.lblWeather = GetWeather();

            return View();
        }
	}
}