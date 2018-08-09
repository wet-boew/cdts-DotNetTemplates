using System;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class FeedbackandShareThisPageSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            //Display the FeedbackLink
            WebTemplateMaster.WebTemplateCore.ShowFeedbackLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showFeedbackLink"
            WebTemplateMaster.WebTemplateCore.FeedbackLinkURL = "http://www.aircanada.com/en/customercare/customersolutions.html";
                        
            //Specify the Share This Page with Media sites.
            WebTemplateMaster.WebTemplateCore.ShowSharePageLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showSharePageLink"

            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.bitly);
            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.facebook);
            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.twitter);

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
        }
    }
}
