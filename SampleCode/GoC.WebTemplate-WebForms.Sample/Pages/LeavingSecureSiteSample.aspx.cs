using System;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class LeavingSecureSiteSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //note: other then the message the rest could be set in the web.config
            WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.Enabled = true;
            WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.RedirectURL = "redirect.aspx";
            WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
            WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.Message = "You are leaving a secure session sample text!";
        }
    }
}