using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public partial class LeavingSecureSiteSample : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //note: other then the message the rest could be set in the web.config
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.Enabled = true;
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.RedirectURL = "redirect.aspx";
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.Message = "You are leaving a secure session sample text!";
        }
    }