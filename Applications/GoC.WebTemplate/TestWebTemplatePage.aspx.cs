using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace GoC.WebTemplate
{
    public partial class TestWebTemplatePage : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WebTemplateMaster.WebTemplateCore.ScreenIdentifier = "993jjd9-33";
            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle.Href = "http://www.pinkbike.com";
            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "YUP";
            this.WebTemplateMaster.WebTemplateCore.HeaderTitle = "Sample Page";
            this.WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta name='description' content='My Description'>");
            this.WebTemplateMaster.WebTemplateCore.DateModified = Convert.ToDateTime("9 january 2015");
            this.WebTemplateMaster.WebTemplateCore.VersionIdentifier = "AA927823737.00.99";
            this.WebTemplateMaster.WebTemplateCore.ShowFeatures = true;
            this.WebTemplateMaster.WebTemplateCore.ShowSearch = true;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Enabled = false;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.ReactionTime = 20001;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshCallbackUrl = "20005";
            this.WebTemplateMaster.WebTemplateCore.TermsConditionsLinkURL = "http://www.pinkbike.com";
            this.WebTemplateMaster.WebTemplateCore.PrivacyLinkURL = "http://www.lapresse.ca";
            this.WebTemplateMaster.WebTemplateCore.ShowSharePageLink = true;
            this.WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.bitly);
            this.WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.facebook);
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.Enabled = false;
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.DisplayModalWindow = true;            
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.Message = "Y'éou are about to leave a secure site, do you wish to continue?223";
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.RedirectURL = "webform1.aspx";
        }
    }
}