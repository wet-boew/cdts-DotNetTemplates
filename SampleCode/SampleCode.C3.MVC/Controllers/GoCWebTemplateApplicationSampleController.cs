using System.Collections.Generic;
using System.Web.Mvc;
using GoC.WebTemplate;

namespace SampleCode.C3.MVC.Controllers
{
    public class GoCWebTemplateApplicationSampleController : WebTemplateBaseController
    {

        public ActionResult StandardPage()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            return View();
        }

        public ActionResult Secure()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowSecure = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            return View();
        }

        public ActionResult SignIn()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            WebTemplateCore.ShowSignInLink = true;
            WebTemplateCore.SignInLinkURL = "about//blank";
            return View();
        }
        public ActionResult SignOut()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            WebTemplateCore.ShowSignOutLink = true;
            WebTemplateCore.SignOutLinkURL = "about//blank";
            return View();
        }
        public ActionResult NoSearch()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowSearch = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            return View();
        }
        public ActionResult NoMenu()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowSiteMenu = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            return View();
        }

        public ActionResult CustomMenu()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.CustomSiteMenuURL =
                "https://ssl-templates.services.gc.ca/app/cls/WET/gcweb/v4_0_24/cdts/custommenu-en.html";

            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            return View();
        }
        public ActionResult SignInWithCustomMenu()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.CustomSiteMenuURL =
                "https://ssl-templates.services.gc.ca/app/cls/WET/gcweb/v4_0_24/cdts/custommenu-en.html";

            WebTemplateCore.SignInLinkURL = "about//blank";
            WebTemplateCore.ShowSignInLink = true;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            return View();
        }

        public ActionResult WithBreadCrumbs()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb{ Href = "https://www.canada.ca/en.html", Title = "GoC", Acronym = "Government of Canada"  },
                new Breadcrumb { Title = "My application" }
            };
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;

            return View();
        }

        public ActionResult StandardFooter()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            return View();
        }
        public ActionResult CustomFooter()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = null;
            WebTemplateCore.ShowPostContent = false;
            WebTemplateCore.CustomFooterLinks = new List<FooterLink>
            {
                new FooterLink {Href= "#", Text= "Link 1"},
                new FooterLink {Href= "#", Text= "Link 2"},
                new FooterLink {Href= "#", Text= "Link 3", NewWindow= true},
                new FooterLink {Href= "#", Text= "Link 4"},
                new FooterLink {Href= "#", Text= "Link 5"},
                new FooterLink {Href= "#", Text= "Link 6"},
                new FooterLink {Href= "#", Text= "Link 7"},
                new FooterLink {Href= "#", Text= "Link 8"},
                new FooterLink {Href= "#", Text= "Link 9"}
            };
            return View();
        }


        public ActionResult TransactionalFooterCustomLinks()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.ShowLanguageLink = true;
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.ShowGlobalNav = false;
            WebTemplateCore.ContactLinks = new List<Link> { new Link { Href = "#" } };
            WebTemplateCore.ShowFeatures = false;
            WebTemplateCore.TermsConditionsLinkURL = "#";
            WebTemplateCore.PrivacyLinkURL = "#";
            return View();
        }
        // GET: GoCWebTemplateApplicationSample
    }
}