using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.CoreMVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GoC.WebTemplate.CoreMVC.Sample.Controllers
{
    public class GoCWebTemplateSamplesController : WebTemplateBaseController
    {
        public GoCWebTemplateSamplesController(ModelAccessor modelAccessor)
            : base(modelAccessor) { }

        public IActionResult BaseSettingsSample()
        {
            //Page Title
            WebTemplateModel.HeaderTitle = "Basic Settings";

            //Metatags
            WebTemplateModel.HTMLHeaderElements.Add("<meta charset='UTF-8'>");
            WebTemplateModel.HTMLHeaderElements.Add("<meta name='singer' content='Elvis'>");
            WebTemplateModel.HTMLHeaderElements.Add("<meta http-equiv='default-style' content='sample'>");

            //Date Modifiied
            WebTemplateModel.DateModified = new DateTime(2019,10,23);

            //Version Identifier
            WebTemplateModel.VersionIdentifier = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            //Screen Identifier
            WebTemplateModel.ScreenIdentifier = "BASE-SETTING-SAMPLE";

            //Contact Links
            WebTemplateModel.ContactLinks.Add(new Components.Entities.Link { Href = "http://travel.gc.ca/" });

            return View();
        }


        public IActionResult CustomJSandCSSFilesSample()
        {
            //Add a CSS to the header
            WebTemplateModel.HTMLHeaderElements.Add("<link rel='stylesheet' type='text/css' href='/css/mystyle.css'>");

            //Add a JS to the header
            //WebTemplateMaster.HTMLHeaderElements.Add("<script src='/js/myJS.js'></script>");
            //or to add it to the body (bottom of page)
            WebTemplateModel.HTMLBodyElements.Add("<script src='/js/myJS.js'></script>");

            return View();
        }

        public IActionResult BilingualErrorSample()
        {
            return View();
        }

        public IActionResult UnilingualErrorSample()
        {
            return View();
        }

        public IActionResult BreadcrumbSample()
        {
            //Specify your breadcrumbs
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "http://www.canada.ca/en/index.html", Title = "Home" });
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "http://www.esdc.gc.ca/en/jobs/opportunities/index.page", Title = "Jobs" });
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "http://www.esdc.gc.ca/en/jobs/opportunities/youth_students.page", Title = "Opportunities" });
            //Leaving the "href" parameter empty, will create the breadcrumb in text and not as a hyperlink. Useful for the last item of the breadcrumb list. 
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Title = "FSWEP", Acronym = "Federal Student Work Experience Program" });

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }

        public IActionResult FeedbackandShareThisPageSample()
        {
            //Display the FeedbackLink
            WebTemplateModel.FeedbackLink.Show = true; //this could be set in the appsettings.json, key = "GoC.WebTemplate.showFeedbackLink"
            WebTemplateModel.FeedbackLink.Url = "http://www.aircanada.com/en/customercare/customersolutions.html";
            WebTemplateModel.FeedbackLink.UrlFr = "http://www.aircanada.com/fr/customercare/customersolutions.html"; //will be used if the CurrentUICulture is set to 'fr' / if not set, will assume FeedbackLinkUrl is bilingual


            ////Specify the Share This Page with Media sites.
            WebTemplateModel.Settings.ShowSharePageLink = true; //this could be set in the appsettings.json, key = "GoC.WebTemplate.showSharePageLink"
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.bitly);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.blogger);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.digg);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.diigo);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.email);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.facebook);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.gmail);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.linkedin);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.myspace);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.pinterest);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.reddit);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.tumblr);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.twitter);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.yahoomail);

            return View();
        }

        public ActionResult FooterLinksSample()
        {
            //Contact Links
            WebTemplateModel.ContactLinks = new List<Link> { new Link { Href = "http://travel.gc.ca/" } };

            //The code snippet below displays an example of multiple links that have text and href being updated. 
            //WebTemplateModel.Settings.Environment = "Prod_SSL";
            //WebTemplateModel.ContactLinks = new List<Link>
            //{
            //    new Link { Href = "http://travel.gc.ca/", Text = "Contact Now" },
            //    new Link { Href = "http://travel.gc.ca/", Text = "Contact Info" }
            //};

            //Footer Sections - Application, GCIntranet
            //WebTemplateModel.Settings.Environment = "Prod_SSL";
            //WebTemplateModel.FooterSections = new List<FooterSection>
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
            //return View("FooterLinksAppSample");

            //Custom Footer Links - Application, GCWeb
            //WebTemplateModel.CustomFooterLinks = new List<Link>
            //{
            //    new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 1" },
            //    new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 2" }
            //};
            //return View("FooterLinksAppSample");

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }
    }
}
