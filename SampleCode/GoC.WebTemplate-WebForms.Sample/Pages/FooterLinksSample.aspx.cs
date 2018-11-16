using GoC.WebTemplate.WebForms;
using System;
using GoC.WebTemplate.Components;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class FooterLinksSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Contact Links
            WebTemplateMaster.WebTemplateCore.ContactLinks = new List<Link> { new Link("http://travel.gc.ca/", "Contact Us") };

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
        }
    }
}