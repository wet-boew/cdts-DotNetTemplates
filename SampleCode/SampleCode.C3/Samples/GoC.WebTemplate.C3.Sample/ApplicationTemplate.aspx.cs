using System;
using System.Collections.Generic;
using GoC.WebTemplate;


public partial class ApplicationTemplate : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "Application Name";
            this.WebTemplateMaster.WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            this.WebTemplateMaster.WebTemplateCore.ShowPreContent = false;
            this.WebTemplateMaster.WebTemplateCore.ShowSecure = true;
            this.WebTemplateMaster.WebTemplateCore.ShowSearch = true;

        this.WebTemplateMaster.WebTemplateCore.ContactLink = new Link() {Href="http://travel.gc.ca/"};

        this.WebTemplateMaster.WebTemplateCore.Breadcrumbs = new List<Breadcrumb>
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
        }
    }