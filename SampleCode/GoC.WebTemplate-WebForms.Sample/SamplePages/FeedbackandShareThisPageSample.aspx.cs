using System;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class FeedbackandShareThisPageSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            //Display the FeedbackLink
            WebTemplateMaster.WebTemplateCore.ShowFeedbackLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showFeedbackLink"
            WebTemplateMaster.WebTemplateCore.FeedbackLinkUrl = "http://www.aircanada.com/en/customercare/customersolutions.html";
            WebTemplateMaster.WebTemplateCore.FeedbackLinkUrlFr = "http://www.aircanada.com/fr/customercare/customersolutions.html"; //will be used if the CurrentUICulture is set to 'fr' / if not set, will assume FeedbackLinkUrl is bilingual

            //Specify the Share This Page with Media sites.
            WebTemplateMaster.WebTemplateCore.ShowSharePageLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showSharePageLink"

            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.bitly);
            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.facebook);
            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.twitter);

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
        }
    }
}
