using System;
using GoC.WebTemplate.Components;
using System.Collections.Generic;
using System.Globalization;

namespace GoC.WebTemplate.WebForms
{
    public partial class TestWebTemplatePage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Link link = new Link() { Text = (WebTemplateMaster.WebTemplateCore.Environment == "AKAMAI") ? string.Empty : "Test link", Href = "http://tempuri.com" };
            WebTemplateMaster.WebTemplateCore.ContactLinks = new List<Link>() { link };
            WebTemplateMaster.WebTemplateCore.ScreenIdentifier = "993jjd9-33";
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Href = "http://www.pinkbike.com";
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "YUP";
            WebTemplateMaster.WebTemplateCore.HeaderTitle = "Sample Page";
            WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta name='description' content='My Description'>");
            WebTemplateMaster.WebTemplateCore.DateModified = Convert.ToDateTime("9 january 2015", CultureInfo.CurrentCulture);
            WebTemplateMaster.WebTemplateCore.VersionIdentifier = "AA927823737.00.99";
            WebTemplateMaster.WebTemplateCore.ShowSearch = true;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.Enabled = false;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.ReactionTime = 20001;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshCallbackUrl = "20005";
            WebTemplateMaster.WebTemplateCore.TermsConditionsLinkURL = "http://www.pinkbike.com";
            WebTemplateMaster.WebTemplateCore.PrivacyLinkURL = "http://www.lapresse.ca";
            WebTemplateMaster.WebTemplateCore.ShowSharePageLink = true;
            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.bitly);
            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.facebook);
            WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.Enabled = false;
            WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.DisplayModalWindow = true;            
            WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.Message = "You are about to leave a secure site, do you wish to continue?";
            WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning.RedirectURL = "webform1.aspx";
        }
    }
}