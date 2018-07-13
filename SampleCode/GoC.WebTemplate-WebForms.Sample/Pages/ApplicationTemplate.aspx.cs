using System;
using System.Collections.Generic;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class ApplicationTemplate : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateMaster.WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateMaster.WebTemplateCore.ShowPreContent = false;
            WebTemplateMaster.WebTemplateCore.ShowSearch = true;

            WebTemplateMaster.WebTemplateCore.ContactLink = new Link() {Href="http://travel.gc.ca/"};

            WebTemplateMaster.WebTemplateCore.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb{ Href = "https://www.canada.ca/en.html", Title = "GoC", Acronym = "Government of Canada"  },
                new Breadcrumb { Title = "My application" }
            };
            WebTemplateMaster.WebTemplateCore.CustomFooterLinks = new List<FooterLink>
            {
                new FooterLink
                {
                    Href = "about:blank",
                    NewWindow = true,
                    Text = "Footer Link 1"
                }
            };
            WebTemplateMaster.WebTemplateCore.CustomFooterLinks.Add(new FooterLink
            {
                Href="about:blank",
                Text = "Footer Link 2"
            });

            WebTemplateMaster.WebTemplateCore.SignInLinkURL = "about:blank";
            WebTemplateMaster.WebTemplateCore.ShowSignInLink = true;
            WebTemplateMaster.WebTemplateCore.AppSettingsURL = "http://tempuri.com";
        }
    }
}