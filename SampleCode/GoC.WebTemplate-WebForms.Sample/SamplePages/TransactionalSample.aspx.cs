using System;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class TransactionalSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the Terms and Condition Link
            WebTemplateMaster.WebTemplateCore.TermsConditionsLink = new FooterLink {Href = "http://www.tsn.ca", NewWindow = true};
            //set the Privacy link
            WebTemplateMaster.WebTemplateCore.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca"}; // NewWindow defaults to false
        }
    }
}
