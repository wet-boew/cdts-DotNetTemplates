using System;
using System.Collections.Generic;
using System.Globalization;
using GoC.WebTemplate.Components.Entities;

namespace GoC.WebTemplate.WebForms
{
    public partial class TestWebTemplatePage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Link link = new Link() { Text = (WebTemplateMaster.WebTemplateCore.Settings.Environment == "AKAMAI") ? string.Empty : "Test link", Href = "http://tempuri.com" };
            WebTemplateMaster.WebTemplateCore.ContactLinks = new List<Link>() { link };
            WebTemplateMaster.WebTemplateCore.ScreenIdentifier = "993jjd9-33";
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Href = "http://www.pinkbike.com";
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "YUP";
            WebTemplateMaster.WebTemplateCore.HeaderTitle = "Sample Page";
            WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta name='description' content='My Description'>");
            WebTemplateMaster.WebTemplateCore.DateModified = Convert.ToDateTime("9 january 2015", CultureInfo.CurrentCulture);
            WebTemplateMaster.WebTemplateCore.VersionIdentifier = "AA927823737.00.99";
            WebTemplateMaster.WebTemplateCore.Settings.ShowSearch = true;
            WebTemplateMaster.WebTemplateCore.Settings.SessionTimeout.Enabled = false;
            WebTemplateMaster.WebTemplateCore.Settings.SessionTimeout.ReactionTime = 20001;
            WebTemplateMaster.WebTemplateCore.Settings.SessionTimeout.RefreshCallBackUrl = "20005";
            WebTemplateMaster.WebTemplateCore.TermsConditionsLink = new FooterLink { Href = "http://www.pinkbike.com" }; 
            WebTemplateMaster.WebTemplateCore.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca" };
            WebTemplateMaster.WebTemplateCore.Settings.ShowSharePageLink = true;
            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(SocialMediaSites.bitly);
            WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(SocialMediaSites.facebook);
            WebTemplateMaster.WebTemplateCore.Settings.LeavingSecureSiteWarning.Enabled = false;
            WebTemplateMaster.WebTemplateCore.Settings.LeavingSecureSiteWarning.DisplayModalWindow = true;            
            WebTemplateMaster.WebTemplateCore.Settings.LeavingSecureSiteWarning.Message = "You are about to leave a secure site, do you wish to continue?";
            WebTemplateMaster.WebTemplateCore.Settings.LeavingSecureSiteWarning.RedirectUrl = "webform1.aspx";
        }
    }
}