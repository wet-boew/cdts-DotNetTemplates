using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleCode.C3.Samples
{
    public partial class LeavingSecureSiteSample : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //note: other then the message the rest could be set in the web.config
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning_Enabled = true;
            this.WebTemplateMaster.WebTemplateCore.leavingSecureSiteWarning_RedirectURL = "redirect.aspx";
            this.WebTemplateMaster.WebTemplateCore.leavingSecureSiteWarning_ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning_Message = "You are leaving a secure session sample text!";
        }
    }
}