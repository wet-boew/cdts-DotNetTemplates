using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleCode.C3.MVC.Controllers
{
    public class ExtendedBaseController : GoC.WebTemplate.WebTemplateBaseController
    {

        protected override void EndExecuteCore(IAsyncResult asyncResult)
        {
            //Set a the common title for everypage here
            this.WebTemplateCore.HeaderTitle = "Title set for everypage!";

            base.EndExecuteCore(asyncResult);
        }

        protected override void PopulateViewBag()
        {
            base.PopulateViewBag();
        }

        public string GetWeather()
        {
            // get data from source...
            // do some calculation...
            // etc....
            return "Sunny";
        }

        public string SessionID
        {
            get { return this.Session.SessionID; }
        }
	}
}