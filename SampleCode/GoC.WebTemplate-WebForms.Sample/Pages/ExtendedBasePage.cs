using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoC.WebTemplate;

    public class ExtendedBasePage : GoC.WebTemplate.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.WebTemplateMaster.WebTemplateCore.HeaderTitle = "Title set for everypage!";
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