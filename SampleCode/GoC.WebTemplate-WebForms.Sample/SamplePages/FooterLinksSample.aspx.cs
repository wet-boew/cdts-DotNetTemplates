using System;
using System.Collections.Generic;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class FooterLinksSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Contact Links
            WebTemplateMaster.WebTemplateCore.ContactLinks = new List<Link> { new Link { Href = "http://travel.gc.ca/" } };

            //The code snippet below displays an example of multiple links that have text and href being updated. 
            /*
                WebTemplateMaster.WebTemplateCore.ContactLinks = new List<Link> 
                { 
                    new Link { Href = "http://travel.gc.ca/", Text = "Contact Now"}, 
                    new Link { Href = "http://travel.gc.ca/", Text = "Contact Info"} 
                };
            */

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
        }
    }
}