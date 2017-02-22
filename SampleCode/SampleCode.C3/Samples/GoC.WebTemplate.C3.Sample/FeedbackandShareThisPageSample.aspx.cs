using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoC.WebTemplate;

public partial class FeedbackandShareThisPageSample : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {            
        //Display the FeedbackLink
        this.WebTemplateMaster.WebTemplateCore.ShowFeedbackLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showFeedbackLink"
        this.WebTemplateMaster.WebTemplateCore.FeedbackLink_URL = "http://www.aircanada.com/en/customercare/customersolutions.html";
                        
        //Specify the Share This Page with Media sites.
        this.WebTemplateMaster.WebTemplateCore.ShowSharePageLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showSharePageLink"

        this.WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.bitly);
        this.WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.facebook);
        this.WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.twitter);

        //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
    }
}
