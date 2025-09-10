using System;
using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class FeedbackandShareThisPageSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            //Display the FeedbackLink
            WebTemplateMaster.WebTemplateModel.Settings.FeedbackLink.Show = true; //this could be set in the web.config, key = "GoC.WebTemplate.showFeedbackLink"
            WebTemplateMaster.WebTemplateModel.Settings.FeedbackLink.Url = "http://www.aircanada.com/en/customercare/customersolutions.html";
            WebTemplateMaster.WebTemplateModel.Settings.FeedbackLink.UrlFr = "http://www.aircanada.com/fr/customercare/customersolutions.html"; //will be used if the CurrentUICulture is set to 'fr' / if not set, will assume FeedbackLinkUrl is bilingual

            //Specify the Share This Page with Media sites.
            WebTemplateMaster.WebTemplateModel.Settings.ShowSharePageLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showSharePageLink"

            WebTemplateMaster.WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.facebook);
            WebTemplateMaster.WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.twitter);

            //Display the Contributors pattern
            WebTemplateMaster.WebTemplateModel.Contributors = new List<Link>() { new Link() { Text = "ESDC", Href = "esdc.prv" } };

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
        }
    }
}
