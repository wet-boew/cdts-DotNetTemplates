using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoC.WebTemplate;

public partial class FooterLinksSample : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Contact Links
        this.WebTemplateMaster.WebTemplateCore.ContactLinkURL = "http://travel.gc.ca/";

        //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)

    }
}
