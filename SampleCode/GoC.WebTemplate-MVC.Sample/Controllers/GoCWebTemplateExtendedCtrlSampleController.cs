using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleCode.C3.MVC.Controllers
{
    public class GoCWebTemplateExtendedCtrlSampleController : ExtendedBaseController
    {

        public ActionResult ExtendedCtrlSample()
        {
            
            //call the functions/properties of the extended controller
            ViewBag.lblSessionID = this.SessionID;
            ViewBag.lblWeather = this.GetWeather();

            return View();
        }
	}
}