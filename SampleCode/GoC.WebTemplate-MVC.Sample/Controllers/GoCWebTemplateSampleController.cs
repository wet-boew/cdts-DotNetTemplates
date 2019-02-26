using System;
using System.Globalization;
using System.Web.Mvc;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using System.Collections.Generic;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class GoCWebTemplateSampleController : WebTemplateBaseController
    {
    
        //
        // GET: /Samples/
        public ActionResult BaseSettingsSample()
        {
            //specify a title for this page
            WebTemplateCore.Environment = "ESDC_PROD";
            WebTemplateCore.UseHTTPS = true;
            WebTemplateCore.HeaderTitle = "Setting Custom Theme";
            WebTemplateCore.ApplicationTitle.Text = "Custom Title";
            WebTemplateCore.ApplicationTitle.Href = "FOO/Bar";
            //specify the metatags
            WebTemplateCore.HTMLHeaderElements.Add("<meta charset='UTF-8'>");
            WebTemplateCore.HTMLHeaderElements.Add("<meta name='singer' content='Elvis'>");
            WebTemplateCore.HTMLHeaderElements.Add("<meta http-equiv='default-style' content='sample'>");
            //specify the date modified
            WebTemplateCore.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);
            //or for using the current date
            //WebTemplateCore.DateModified = DateTime.Now.Date;

            //specify the version identifier (Note: since the date modified is supplied the date takes precedence)
            //WebTemplateCore.VersionIdentifier = "AA927823737.00.99";

            //specify a screen identifier
            WebTemplateCore.ScreenIdentifier = "CALVIN ROCKS";
            return View();
        }

        public ActionResult BreadcrumbSample()
        {
            //Specify your breadcrumbs
            WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.canada.ca/en/index.html", "Home", ""));
            WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.esdc.gc.ca/en/jobs/opportunities/index.page", "Jobs", ""));
            WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.esdc.gc.ca/en/jobs/opportunities/youth_students.page", "Opportunities", ""));
            //Leaving the "href" parameter empty, will create the breadcrumb in text and not as a hyperlink. Useful for the last item of the breadcrumb list. 
            WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("", "FSWEP", "Federal Student Work Experience Program"));

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }

        public ActionResult AddJSandCSSFilesSample()
        {
            //Add a CSS to the header
            WebTemplateCore.HTMLHeaderElements.Add("<link rel='stylesheet' type='text/css' href='/Content/mystyle.css'>");

            //Add a JS to the header
            //WebTemplateMaster.HTMLHeaderElements.Add("<script src='myJS.js'></script>");
            //or to add it to the body (bottom of page)
            WebTemplateCore.HTMLBodyElements.Add("<script src='/Scripts/myJS.js'></script>");

            return View();
        }

        public ActionResult FeedbackandShareThisPageSample()
        {
            //Display the FeedbackLink
            WebTemplateCore.ShowFeedbackLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showFeedbackLink"
            WebTemplateCore.FeedbackLinkUrl = "http://www.aircanada.com/en/customercare/customersolutions.html";
            WebTemplateCore.FeedbackLinkUrlFr = "http://www.aircanada.com/fr/customercare/customersolutions.html"; //will be used if the CurrentUICulture is set to 'fr' / if not set, will assume FeedbackLinkUrl is bilingual


            ////Specify the Share This Page with Media sites.
            //WebTemplateCore.ShowSharePageLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showSharePageLink"

            //WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.bitly);
            //WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.facebook);
            //WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.twitter);

            WebTemplateCore.ShowSharePageLink = true;
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.bitly);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.linkedin);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.blogger);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.myspace);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.delicious);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.pinterest);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.digg);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.reddit);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.diigo);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.stumbleupon);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.email);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.tumblr);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.facebook);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.twitter);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.gmail);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.yahoomail);
            WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.googleplus);

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }

        public ActionResult FooterLinksSample()
        {
            //Contact Links
            WebTemplateCore.ContactLinks = new List<Link> { new Link { Href = "http://travel.gc.ca/" } };

            //The code snippet below displays an example of multiple links that have text and href being updated.
            /*
                WebTemplateCore.ContactLinks = new List<Link> 
                { 
                    new Link { Href = "http://travel.gc.ca/", Text = "Contact Now"}, 
                    new Link { Href = "http://travel.gc.ca/", Text = "Contact Info"} 
                };
            */

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }
        public ActionResult LeavingSecureSiteSample()
        {
            //note: other then the message the rest could be set in the web.config
            WebTemplateCore.LeavingSecureSiteWarning.Enabled = true;
            WebTemplateCore.LeavingSecureSiteWarning.RedirectURL = "Redirect";
            WebTemplateCore.LeavingSecureSiteWarning.ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
            WebTemplateCore.LeavingSecureSiteWarning.Message = "You are leaving a secure session sample text!";
           
            return View();
        }

        public ActionResult LeftSideMenuSample()
        {
            var leftMenu = new MenuSection
            {
                Name = "Section A",
                Link = "http://www.servicecanada.gc.ca",
                OpenInNewWindow = true
            };

            //set the header for this section of the menu
            //set the links for this section of the menu
            leftMenu.Items.Add(new MenuItem("http://www.tsn.ca", "TSN", new [] { 
                                                            new MenuItem("http://www.cbc.ca", "sub 1", true), 
                                                            new MenuItem("http://www.rds.ca", "sub 2") }));
            leftMenu.Items.Add(new Link("http://www.cnn.ca", "CNN"));

            //add section to template
            WebTemplateCore.LeftMenuItems.Add(leftMenu);

            //or can be done with a 1 liner
            WebTemplateCore.LeftMenuItems.Add(new MenuSection("Section B", "http://www.canada.ca", new [] { 
                                                                                new Link("http://www.rds.ca", "RDS"), 
                                                                                new Link("http://www.lapresse.com", "La Presse") }));

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
            
            WebTemplateCore.SessionTimeout.Enabled = true;
            WebTemplateCore.SessionTimeout.Inactivity = 30000;
            WebTemplateCore.SessionTimeout.ReactionTime = 10000;
            WebTemplateCore.SessionTimeout.Sessionalive = 30000;
            WebTemplateCore.SessionTimeout.Logouturl = "Logout";
            WebTemplateCore.SessionTimeout.RefreshCallbackUrl = "SessionValidity";
            WebTemplateCore.SessionTimeout.RefreshOnClick = false;
            WebTemplateCore.SessionTimeout.RefreshLimit = 3;
            WebTemplateCore.SessionTimeout.Method = "";
            WebTemplateCore.SessionTimeout.AdditionalData = "";

            return View();
        }

        public ActionResult TransactionalSample()
        {
            //Contact Links
            WebTemplateCore.ContactLinks = new List<Link> { new Link { Href = "http://travel.gc.ca/" } };
            //set the Terms and Condition Link
            WebTemplateCore.TermsConditionsLinkURL = "http://www.tsn.ca";
            //set the Privacy link
            WebTemplateCore.PrivacyLinkURL = "http://www.lapresse.ca";

            return View();
        }
               
        [HttpPost]
        public ActionResult TransactionalSample(string data1, string data2, string data4)
        {
            //set the Terms and Condition Link
            WebTemplateCore.TermsConditionsLinkURL = "http://www.tsn.ca";
            //set the Privacy link
            WebTemplateCore.PrivacyLinkURL = "http://www.lapresse.ca";
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
            WebTemplateCore.SplashPageInfo.EnglishHomeUrl = "http://www.canada.ca/en/index.html";
            WebTemplateCore.SplashPageInfo.FrenchHomeUrl = "http://www.canada.ca/fr/index.html";
            WebTemplateCore.SplashPageInfo.EnglishTermsUrl = "http://www.canada.ca/en/transparency/terms.html";
            WebTemplateCore.SplashPageInfo.FrenchTermsUrl = "http://www.canada.ca/fr/transparence/avis.html";
            WebTemplateCore.SplashPageInfo.EnglishName = "[My web asset]";
            WebTemplateCore.SplashPageInfo.FrenchName = "[Mon actif web]";
            return View();
        }

        public ActionResult GCIntranetThemeSample()
        {

            WebTemplateCore.Environment = "ESDC_PROD";
            WebTemplateCore.UseHTTPS = true;
            WebTemplateCore.ApplicationTitle.Text = "My Custom Title";
            WebTemplateCore.ApplicationTitle.Href = "http://iservice.prv/eng/index.shtml";  
            
            return View();
        }
	}
}