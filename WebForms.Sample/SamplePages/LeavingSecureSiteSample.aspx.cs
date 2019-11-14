using System;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class LeavingSecureSiteSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //note: other then the message the rest could be set in the web.config
            WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.Enabled = true;
            WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.RedirectUrl = "redirect.aspx";
            WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
            WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.Message = "You are leaving a secure session sample text!";
        }
    }
}