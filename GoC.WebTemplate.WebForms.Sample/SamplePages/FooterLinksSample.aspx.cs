using System;
using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class FooterLinksSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Contact Links
            WebTemplateMaster.WebTemplateModel.ContactLinks = new List<Link> { new Link { Href = "http://travel.gc.ca/" } };

            //Footer Sections - Application, GCIntranet
            //WebTemplateMaster.WebTemplateModel.FooterSections = new List<FooterSection>
            //{
            //    new FooterSection
            //    {
            //        SectionName = "Footer Section 1",
            //        CustomFooterLinks = new List<FooterLink>
            //        {
            //            new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 1" },
            //            new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 2" }
            //        }
            //    }
            //};

            //Custom Footer Links - Application, GCWeb
            //WebTemplateMaster.WebTemplateModel.CustomFooterLinks = new List<FooterLink>
            //{
            //    new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 1" },
            //    new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 2" }
            //};
            
            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
        }
    }
}