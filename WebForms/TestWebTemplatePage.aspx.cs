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
            Link link = new Link() { Text = (WebTemplateMaster.WebTemplateModel.Settings.Environment == "AKAMAI") ? string.Empty : "Test link", Href = "http://tempuri.com" };
            WebTemplateMaster.WebTemplateModel.ContactLinks = new List<Link>() { link };
            WebTemplateMaster.WebTemplateModel.ScreenIdentifier = "993jjd9-33";
            WebTemplateMaster.WebTemplateModel.ApplicationTitle.Href = "http://www.pinkbike.com";
            WebTemplateMaster.WebTemplateModel.ApplicationTitle.Text = "YUP";
            WebTemplateMaster.WebTemplateModel.HeaderTitle = "Sample Page";
            WebTemplateMaster.WebTemplateModel.HTMLHeaderElements.Add("<meta name='description' content='My Description'>");
            WebTemplateMaster.WebTemplateModel.DateModified = Convert.ToDateTime("9 january 2015", CultureInfo.CurrentCulture);
            WebTemplateMaster.WebTemplateModel.VersionIdentifier = "AA927823737.00.99";
            WebTemplateMaster.WebTemplateModel.Settings.ShowSearch = true;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.Enabled = false;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.ReactionTime = 20001;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.RefreshCallBackUrl = "20005";
            WebTemplateMaster.WebTemplateModel.TermsConditionsLink = new FooterLink { Href = "http://www.pinkbike.com" }; 
            WebTemplateMaster.WebTemplateModel.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca" };
            WebTemplateMaster.WebTemplateModel.Settings.ShowSharePageLink = true;
            WebTemplateMaster.WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.bitly);
            WebTemplateMaster.WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.facebook);
            WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.Enabled = false;
            WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.DisplayModalWindow = true;            
            WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.Message = "You are about to leave a secure site, do you wish to continue?";
            WebTemplateMaster.WebTemplateModel.Settings.LeavingSecureSiteWarning.RedirectUrl = "webform1.aspx";
        }
    }
}