using System;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public class ExtendedBasePage : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            WebTemplateMaster.WebTemplateModel.HeaderTitle = "Title set for everypage!";
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