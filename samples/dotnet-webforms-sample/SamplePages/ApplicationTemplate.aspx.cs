using System;
using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class ApplicationTemplate : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebTemplateMaster.WebTemplateModel.ApplicationTitle.Text = "Application Name";
            WebTemplateMaster.WebTemplateModel.LanguageLink.Href = "apptop-fr.html";
            WebTemplateMaster.WebTemplateModel.ShowPreContent = false;
            WebTemplateMaster.WebTemplateModel.Settings.ShowSearch = true;

            WebTemplateMaster.WebTemplateModel.ContactLinks = new List<Link> { new Link() { Href = "http://travel.gc.ca/" } };

            WebTemplateMaster.WebTemplateModel.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb { Href = "https://www.canada.ca/en.html", Title = "GoC", Acronym = "Government of Canada"  },
                new Breadcrumb { Title = "My application" }
            };
            WebTemplateMaster.WebTemplateModel.CustomFooterLinks = new List<FooterLink>
            {
                new FooterLink
                {
                    Href = "about:blank",
                    NewWindow = true,
                    Text = "Footer Link 1"
                }
            };
            WebTemplateMaster.WebTemplateModel.CustomFooterLinks.Add(new FooterLink
            {
                Href="about:blank",
                Text = "Footer Link 2"
            });

            WebTemplateMaster.WebTemplateModel.Settings.SignInLinkUrl = "about:blank";
            WebTemplateMaster.WebTemplateModel.ShowSignInLink = true;
            WebTemplateMaster.WebTemplateModel.AppSettingsURL = "http://tempuri.com";


            WebTemplateMaster.WebTemplateModel.CustomSearch = new CustomSearch
            {
                Action = "http://hrsdc.prv/cgi-bin/recherche-search/Intraweb/index.aspx",
                // Id = "0001", optional
                Method = "get", // 'get' or 'post'
                Placeholder = "Search ESDC IntraWeb",
                HiddenInput = new List<KeyValuePair<string, string>> //optional
                {
                    new KeyValuePair<string, string>("GoCTemplateCulture", "en-CA"),
                    new KeyValuePair<string, string>("p1", "gc")
                }
            };
        }
    }
}