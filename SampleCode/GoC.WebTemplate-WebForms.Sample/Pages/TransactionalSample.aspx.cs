using System;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class TransactionalSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the Terms and Condition Link
            WebTemplateMaster.WebTemplateCore.TermsConditionsLinkURL = "http://www.tsn.ca";
            //set the Privacy link
            WebTemplateMaster.WebTemplateCore.PrivacyLinkURL = "http://www.lapresse.ca";
        }
    }
}
