using System;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class ExtendedBaseController : WebTemplateBaseController
    {

        protected override void EndExecuteCore(IAsyncResult asyncResult)
        {
            //Set a the common title for everypage here
            WebTemplateCore.HeaderTitle = "Title set for everypage!";

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

        public string SessionID => Session.SessionID;
    }
}