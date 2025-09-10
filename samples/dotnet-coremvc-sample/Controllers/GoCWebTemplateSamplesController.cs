using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.CoreMVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GoC.WebTemplate.CoreMVC.Sample.Controllers
{
    public class GoCWebTemplateSamplesController : WebTemplateBaseController
    {
        public GoCWebTemplateSamplesController(IModelAccessor modelAccessor)
            : base(modelAccessor) { }


        public ActionResult AdobeAnalyticsSample()
        {
            //Add Analytics
            WebTemplateModel.Settings.WebAnalytics.Active = false; //Set to true to activate Adobe Analytics
            WebTemplateModel.Settings.WebAnalytics.Environment = GoC.WebTemplate.Components.Entities.WebAnalytics.EnvironmentOption.staging;
            WebTemplateModel.Settings.WebAnalytics.Version = 1;
            return View();
        }

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
            WebTemplateModel.Settings.FeedbackLink.Show = true; //this could be set in the appsettings.json, key = "GoC.WebTemplate.showFeedbackLink"
            WebTemplateModel.Settings.FeedbackLink.Url = "http://www.aircanada.com/en/customercare/customersolutions.html";
            WebTemplateModel.Settings.FeedbackLink.UrlFr = "http://www.aircanada.com/fr/customercare/customersolutions.html"; //will be used if the CurrentUICulture is set to 'fr' / if not set, will assume FeedbackLinkUrl is bilingual


            ////Specify the Share This Page with Media sites.
            WebTemplateModel.Settings.ShowSharePageLink = true; //this could be set in the appsettings.json, key = "GoC.WebTemplate.showSharePageLink"
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.blogger);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.diigo);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.email);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.facebook);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.gmail);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.linkedin);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.myspace);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.pinterest);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.reddit);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.tinyurl);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.tumblr);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.twitter);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.whatsapp);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.yahoomail);

            //Display the Contributors pattern
            WebTemplateModel.Contributors = new List<Link>() { new Link() { Text = "ESDC", Href = "esdc.prv" } };

            return View();
        }

        public IActionResult FooterLinksSample()
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
            //WebTemplateModel.CustomFooterLinks = new List<FooterLink>
            //{
            //    new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 1" },
            //    new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 2" }
            //};
            //return View("FooterLinksAppSample");

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }
        
        public IActionResult LeavingSecureSiteSample()
        {
            //note: other then the message the rest could be set in the web.config
            WebTemplateModel.Settings.LeavingSecureSiteWarning.Enabled = true;
            WebTemplateModel.Settings.LeavingSecureSiteWarning.RedirectUrl = "Redirect";
            WebTemplateModel.Settings.LeavingSecureSiteWarning.ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
            WebTemplateModel.Settings.LeavingSecureSiteWarning.Message = "You are leaving a secure session sample text!";
            WebTemplateModel.Settings.LeavingSecureSiteWarning.CancelMessage = "Absalutly Not!";
            WebTemplateModel.Settings.LeavingSecureSiteWarning.YesMessage = "Fine with me.";

            return View();
        }

        [HttpGet()]
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public RedirectResult Redirect(string targetUrl)
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            //add any necessary clean up code (clear session, logout user, etc...)

            //redirect user to link they had clicked
            if (!string.IsNullOrEmpty(targetUrl)) return base.Redirect(targetUrl);

            // decide how you want to handle this situation
            throw new ApplicationException("targetUrl must be specified.");
        }

        public IActionResult LeftMenuSample()
        {
            //set the header for this section of the menu
            //set the links for this section of the menu
            var item = new MenuItem
            {
                Href = "http://www.tsn.ca",
                Text = "TSN",
                SubItems = new List<Link> {
                    new Link { Href="http://www.cbc.ca", Text="sub 1", NewWindow= true },
                    new Link { Href="http://www.rds.ca", Text="sub 2" }
                }
            };

            //add section to template
            WebTemplateModel.LeftMenuItems = new List<MenuSection>
            {
                new MenuSection
                {
                    Text = "Section A",
                    Href = "http://www.servicecanada.gc.ca",
                    NewWindow = true,
                    Items = new List<MenuItem> {
                        item,
                        new MenuItem { Href = "http://www.cnn.ca", Text = "CNN" }
                    }
                }
            };

            //or can be done with a 1 liner
            WebTemplateModel.LeftMenuItems.Add(new MenuSection
            {
                Text = "Section B",
                Href = "http://www.canada.ca",
                Items = new List<MenuItem> {
                    new MenuItem{ Href="http://www.rds.ca", Text="RDS" },
                    new MenuItem{ Href= "http://www.lapresse.com", Text="La Presse"}
                }
            });

            return View();
        }

        public IActionResult SessionTimeoutSample()
        {
            //Session is configured in the Startup.cs
            //see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-2.1

            //Let's display what is in the session on the page
            //We will populate the session with the time
            var key = "STUFF";
            HttpContext.Session.TryGetValue(key, out byte[] value);
            if (value != null)
            {
                ViewData[key] = value.ToString();
            }
            else
            {
                ViewData[key] = "Data from session: It is empty, refresh page to have value";
                HttpContext.Session.Set(key, Encoding.ASCII.GetBytes(string.Concat("Data from session: ", DateTime.Now.ToString())));
            }

            //enable the feature
            WebTemplateModel.Settings.SessionTimeout.Enabled = true;
            WebTemplateModel.Settings.SessionTimeout.Inactivity = 30000;
            WebTemplateModel.Settings.SessionTimeout.ReactionTime = 180000;
            WebTemplateModel.Settings.SessionTimeout.SessionAlive = 210000;
            WebTemplateModel.Settings.SessionTimeout.LogoutUrl = "Logout";
            WebTemplateModel.Settings.SessionTimeout.RefreshCallBackUrl = "SessionValidity";
            WebTemplateModel.Settings.SessionTimeout.RefreshOnClick = false;
            WebTemplateModel.Settings.SessionTimeout.RefreshLimit = 3;
            WebTemplateModel.Settings.SessionTimeout.Method = "";
            WebTemplateModel.Settings.SessionTimeout.AdditionalData = "";

            return View();
        }

        public void SessionValidity()
        {
            //Make changes here to confirm the session validity and end it if nessasarcy.
        }

        public IActionResult Logout()
        {
            //This sample page is referenced by the "logoutUrl" setting of the WET SessionTimeout control
            //It will not be displayed to the user, but is used to perform session clean up and any other clean up that is required before the user is loged out of your application.
            //This page will then redirect to the page you identify example the login page (in our case the BaseSettingsSample.aspx)

            //destroy users sessions
            HttpContext.Session.Clear();

            //perform any other clean up that needs to occur for your application

            //redirect to the page of your preference
            return Redirect("BaseSettingsSample");
        }

        public IActionResult TransactionalSample()
        {
            //set the Terms and Condition Link
            WebTemplateModel.TermsConditionsLink = new FooterLink { Href = "http://www.tsn.ca", NewWindow = true };
            //set the Privacy link
            WebTemplateModel.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca" }; // NewWindow defaults to false

            return View();
        }

        [HttpPost]
#pragma warning disable IDE0060 // Remove unused parameter
        public IActionResult TransactionalSample(string data1, string data2, string data4)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            //set the Terms and Condition Link
            WebTemplateModel.TermsConditionsLink = new FooterLink { Href = "http://www.tsn.ca", NewWindow = true };
            //set the Privacy link
            WebTemplateModel.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca" }; // NewWindow defaults to false
            //execute logic for the submit.
            return View();
        }

        public IActionResult GCIntranetThemeSample()
        {
            //set up theme
            WebTemplateModel.Settings.Environment = "PROD_SSL";

            //custom intranet title
            WebTemplateModel.IntranetTitle = new IntranetTitle
            {
                Href = "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/samples/subtheme-esdc-en.shtml",
                BoldText = "ESDC Sub",
                Acronym = "Employment and Social Development Canada Sub Theme",
                Text = " Custom Title"
            };

            //GCTools model
            WebTemplateModel.Settings.GcToolsModal = true;
            
            return View();
        }

        public IActionResult ApplicationTemplateSample()
        {
            //Application Title
            WebTemplateModel.ApplicationTitle.Text = "Application Name Test";
            WebTemplateModel.ApplicationTitle.Href = "http://canada.ca";

            //Application Settings
            WebTemplateModel.AppSettingsURL = "http://tempuri.com";

            //Change Language Link
            WebTemplateModel.Settings.ShowLanguageLink = true;
            WebTemplateModel.LanguageLink.Href = "apptop-fr.html";

            //Hide Search
            WebTemplateModel.Settings.ShowSearch = true; 

            //Custom Search
            WebTemplateModel.CustomSearch = new CustomSearch
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

            //Pre-Post-Content
            WebTemplateModel.Settings.ShowPostContent = false;
            WebTemplateModel.Settings.ShowPreContent = false;

            //Site Menu
            WebTemplateModel.CustomSiteMenuURL = "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_25/cdts/ajax/custommenu-eng.html";

            //WebTemplateModel.MenuLinks = new List<MenuLink>
            //{
            //    //Add a top level menu item with no drop down
            //    new MenuLink { Href = "Foo", Text = "Item 1" },
            //    //Add a top level menu item with two items in the drop down.
            //    new MenuLink {
            //        Text = "Item 2", 
            //        SubLinks = new List<SubLink> {
            //            new SubLink { Href = "Foo", Text = "SubLink 1" }, 
            //            //Add a placeholder menu item
            //            new SubLink { Text = "SubLink 2" }
            //        }
            //    }
            //};

            //Sign in & Out
            WebTemplateModel.ShowSignOutLink = true;
            WebTemplateModel.Settings.SignOutLinkUrl = "about:blank";
            WebTemplateModel.ShowSignInLink = false;
            WebTemplateModel.Settings.SignInLinkUrl = "about:blank";
            
            return View();
        }
        
        public ActionResult SplashPageSample()
        {
            WebTemplateModel.SplashPageInfo.EnglishHomeUrl = "http://www.canada.ca/en/index.html";
            WebTemplateModel.SplashPageInfo.FrenchHomeUrl = "http://www.canada.ca/fr/index.html";
            WebTemplateModel.SplashPageInfo.EnglishTermsUrl = "http://www.canada.ca/en/transparency/terms.html";
            WebTemplateModel.SplashPageInfo.FrenchTermsUrl = "http://www.canada.ca/fr/transparence/avis.html";
            WebTemplateModel.SplashPageInfo.EnglishName = "[My web asset]";
            WebTemplateModel.SplashPageInfo.FrenchName = "[Mon actif web]";

            //Select the order in which the official languages appear in
            //Values can either be "English" or "French" (English is default)
            //WebTemplateModel.SplashPageInfo.LanguagePrecedence = "French";

            return View();
        }
    }
}
