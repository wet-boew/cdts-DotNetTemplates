using System;
using System.Globalization;
using System.Web.Mvc;
using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCWebTemplateSampleController : WebTemplateBaseController
    {
    
        //
        // GET: /Samples/
        public ActionResult BaseSettingsSample()
        {
            //specify a title for this page
            WebTemplateModel.Settings.Environment = "ESDC_PROD";
            WebTemplateModel.Settings.UseHttps = true;
            WebTemplateModel.HeaderTitle = "Setting Custom Theme";
            WebTemplateModel.ApplicationTitle.Text = "Custom Title";
            WebTemplateModel.ApplicationTitle.Href = "FOO/Bar";
            //specify the metatags
            WebTemplateModel.HTMLHeaderElements.Add("<meta charset='UTF-8'>");
            WebTemplateModel.HTMLHeaderElements.Add("<meta name='singer' content='Elvis'>");
            WebTemplateModel.HTMLHeaderElements.Add("<meta http-equiv='default-style' content='sample'>");
            //specify the date modified
            WebTemplateModel.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);
            //or for using the current date
            //WebTemplateModel.DateModified = DateTime.Now.Date;

            //specify the version identifier (Note: since the date modified is supplied the date takes precedence)
            //WebTemplateModel.VersionIdentifier = "AA927823737.00.99";

            //specify a screen identifier
            WebTemplateModel.ScreenIdentifier = "CALVIN ROCKS";
            return View();
        }

        public ActionResult BreadcrumbSample()
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

        public ActionResult AddJSandCSSFilesSample()
        {
            //Add a CSS to the header
            WebTemplateModel.HTMLHeaderElements.Add("<link rel='stylesheet' type='text/css' href='/static/mystyle.css'>");

            //Add a JS to the header
            //WebTemplateMaster.HTMLHeaderElements.Add("<script src='myJS.js'></script>");
            //or to add it to the body (bottom of page)
            WebTemplateModel.HTMLBodyElements.Add("<script src='/static/myJS.js'></script>");

            return View();
        }

        public ActionResult AdobeAnalyticsSample()
        {
            //Add Analytics
            WebTemplateModel.Settings.WebAnalytics.Active = false; //Set to true to activate Adobe Analytics
            WebTemplateModel.Settings.WebAnalytics.Environment = GoC.WebTemplate.Components.Entities.WebAnalytics.EnvironmentOption.staging;
            WebTemplateModel.Settings.WebAnalytics.Version = 1;
            return View();
        }

        public ActionResult FeedbackandShareThisPageSample()
        {
            //Display the FeedbackLink
            WebTemplateModel.Settings.FeedbackLink.Show = true; //this could be set in the web.config, key = "GoC.WebTemplate.showFeedbackLink"
            WebTemplateModel.Settings.FeedbackLink.Url = "http://www.aircanada.com/en/customercare/customersolutions.html";
            WebTemplateModel.Settings.FeedbackLink.UrlFr = "http://www.aircanada.com/fr/customercare/customersolutions.html"; //will be used if the CurrentUICulture is set to 'fr' / if not set, will assume FeedbackLinkUrl is bilingual

            ////Specify the Share This Page with Media sites.
            WebTemplateModel.Settings.ShowSharePageLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showSharePageLink"
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

        public ActionResult FooterLinksSample()
        {
            //Contact Links
            WebTemplateModel.ContactLinks = new List<Link> { new Link { Href = "http://travel.gc.ca/" } };


            //Footer Sections - Application, GCIntranet
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

            //Custom Footer Links - Application, GCWeb
            //WebTemplateModel.CustomFooterLinks = new List<FooterLink>
            //{
            //    new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 1" },
            //    new FooterLink { Href = "http://travel.gc.ca/", Text = "Link 2" }
            //};

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }
        public ActionResult LeavingSecureSiteSample()
        {
            //note: other then the message the rest could be set in the web.config
            WebTemplateModel.Settings.LeavingSecureSiteWarning.Enabled = true;
            WebTemplateModel.Settings.LeavingSecureSiteWarning.RedirectUrl = "Redirect";
            WebTemplateModel.Settings.LeavingSecureSiteWarning.ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
            WebTemplateModel.Settings.LeavingSecureSiteWarning.Message = "You are leaving a secure session sample text!";
           
            return View();
        }

        public ActionResult LeftSideMenuSample()
        {
            var leftMenu = new MenuSection
            {
                Text = "Section A",
                Href = "http://www.servicecanada.gc.ca",
                NewWindow = true
            };

            //set the header for this section of the menu
            //set the links for this section of the menu
            leftMenu.Items.Add(new MenuItem
            {
                Href = "http://www.tsn.ca",
                Text = "TSN",
                SubItems = new List<Link> {
                    new Link { Href="http://www.cbc.ca", Text="sub 1", NewWindow= true },
                    new Link { Href="http://www.rds.ca", Text="sub 2" } 
                }
            });
            leftMenu.Items.Add(new MenuItem { Href = "http://www.cnn.ca", Text = "CNN" });

            //add section to template
            WebTemplateModel.LeftMenuItems.Add(leftMenu);

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

        public ActionResult SessionTimeoutSample()
        {
            //We will set the server session timeout for this page only, to 1 min
            Session.Timeout = 1;

            //Let's display what is in the session on the page
            //We will populate the session with the time
            if (Session["stuff"] != null)
            {
                ViewBag.Stuff = (Session["stuff"]).ToString();
            }
            else
            {
                ViewBag.Stuff = "Data from session: It is empty, refresh page to have value";
                Session.Add("stuff", string.Concat("Data from session: ", DateTime.Now.ToString()));
            }

            //enable the feature            
            WebTemplateModel.Settings.SessionTimeout.Enabled = true;
            WebTemplateModel.Settings.SessionTimeout.Inactivity = 30000;
            WebTemplateModel.Settings.SessionTimeout.ReactionTime = 10000;
            WebTemplateModel.Settings.SessionTimeout.SessionAlive = 30000;
            WebTemplateModel.Settings.SessionTimeout.LogoutUrl = "Logout";
            WebTemplateModel.Settings.SessionTimeout.RefreshCallBackUrl = "SessionValidity";
            WebTemplateModel.Settings.SessionTimeout.RefreshOnClick = false;
            WebTemplateModel.Settings.SessionTimeout.RefreshLimit = 3;
            WebTemplateModel.Settings.SessionTimeout.Method = "";
            WebTemplateModel.Settings.SessionTimeout.AdditionalData = "";

            return View();
        }

        public ActionResult TransactionalSample()
        {
            //Contact Links
            WebTemplateModel.ContactLinks = new List<Link> { new Link { Href = "http://travel.gc.ca/" } };
            //set the Terms and Condition Link
            WebTemplateModel.TermsConditionsLink = new FooterLink { Href = "http://www.tsn.ca", NewWindow = true };
            //set the Privacy link
            WebTemplateModel.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca" }; // NewWindow defaults to false

            return View();
        }
               
        [HttpPost]
#pragma warning disable IDE0060 // Remove unused parameter
        public ActionResult TransactionalSample(string data1, string data2, string data4)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            //set the Terms and Condition Link
            WebTemplateModel.TermsConditionsLink = new FooterLink { Href = "http://www.tsn.ca", NewWindow = true };
            //set the Privacy link
            WebTemplateModel.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca" }; // NewWindow defaults to false
            //execute logic for the submit.
            return View();
        }

        public ActionResult UnilingualErrorSample() 
        {
            return View();
        }
        public ActionResult BilingualErrorSample() 
        {
            return View();
        }

        //TO DO should this be overidden? put in users ExtendedBaseController? should only write once for all controllers
        public ActionResult Redirect()
        {
            //this page has no visual and will not be displayed to the user
            string targetURL = Server.HtmlDecode(Request.QueryString.Get("targetUrl"));

            //add any necessary clean up code (clear session, logout user, etc...)

            //redirect user to link they had clicked
            if (!string.IsNullOrEmpty(targetURL)) return Redirect(targetURL);

            // decide how you want to handle this situation
            throw new ApplicationException("targetURL must be specified.");
        }

        public ActionResult Logout()
        {
            //This sample page is referenced by the "logoutUrl" setting of the WET SessionTimeout control
            //It will not be displayed to the user, but is used to perform session clean up and any other clean up that is required before the user is loged out of your application.
            //This page will then redirect to the page you identify example the login page (in our case the BaseSettingsSample.aspx)

            //destroy users sessions
            Session.Abandon();

            //perform any other clean up that needs to occur for your application

            //redirect to the page of your preference
            return Redirect("BaseSettingsSample");
        }

        public ActionResult SessionValidity()
        {
            //This sample page is referenced by the "refreshCallbackUrl" setting of the WET SessionTimeout control
            //It's first function is to verify that the users session is still alive
            //It's second function is to reset the timer of the server session to zero.

            //returns "true" if the original session is still alive
            Response.Write((!Session.IsNewSession).ToString().ToLower());
            Response.End();
            return new EmptyResult();
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