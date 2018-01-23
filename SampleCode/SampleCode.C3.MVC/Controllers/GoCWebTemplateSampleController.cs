using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using GoC.WebTemplate;

namespace SampleCode.C3.MVC.Controllers
{
    public class GoCWebTemplateSampleController : GoC.WebTemplate.WebTemplateBaseController
    {
    
        //
        // GET: /Samples/
        public ActionResult BaseSettingsSample()
        {
            //specify a title for this page
            this.WebTemplateCore.Environment = "ESDC_PROD";
            this.WebTemplateCore.UseHTTPS = true;
            this.WebTemplateCore.HeaderTitle = "Setting Custom Theme";
            this.WebTemplateCore.ApplicationTitle.Text = "Custom Title";
            this.WebTemplateCore.ApplicationTitle.Href = "FOO/Bar";
            //specify the metatags
            this.WebTemplateCore.HTMLHeaderElements.Add("<meta charset='UTF-8'>");
            this.WebTemplateCore.HTMLHeaderElements.Add("<meta name='singer' content='Elvis'>");
            this.WebTemplateCore.HTMLHeaderElements.Add("<meta http-equiv='default-style' content='sample'>");
            //specify the date modified
            this.WebTemplateCore.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);
            //or for using the current date
            //this.WebTemplateCore.DateModified = DateTime.Now.Date;

            //specify the version identifier (Note: since the date modified is supplied the date takes precedence)
            //this.WebTemplateCore.VersionIdentifier = "AA927823737.00.99";

            //specify a screen identifier
            this.WebTemplateCore.ScreenIdentifier = "CALVIN ROCKS";
            return View();
        }

        public ActionResult BreadcrumbSample()
        {
            //Specify your breadcrumbs
            this.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.canada.ca/en/index.html", "Home", ""));
            this.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.esdc.gc.ca/en/jobs/opportunities/index.page", "Jobs", ""));
            this.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.esdc.gc.ca/en/jobs/opportunities/youth_students.page", "Opportunities", ""));
            //Leaving the "href" parameter empty, will create the breadcrumb in text and not as a hyperlink. Useful for the last item of the breadcrumb list. 
            this.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("", "FSWEP", "Federal Student Work Experience Program"));

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }

        public ActionResult AddJSandCSSFilesSample()
        {
            //Add a CSS to the header
            this.WebTemplateCore.HTMLHeaderElements.Add("<link rel='stylesheet' type='text/css' href='/Content/mystyle.css'>");

            //Add a JS to the header
            //this.WebTemplateMaster.HTMLHeaderElements.Add("<script src='myJS.js'></script>");
            //or to add it to the body (bottom of page)
            this.WebTemplateCore.HTMLBodyElements.Add("<script src='/Scripts/myJS.js'></script>");

            return View();
        }

        public ActionResult FeedbackandShareThisPageSample()
        {
            //Display the FeedbackLink
            this.WebTemplateCore.ShowFeedbackLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showFeedbackLink"
            this.WebTemplateCore.FeedbackLinkURL = "http://www.aircanada.com/en/customercare/customersolutions.html";

            ////Specify the Share This Page with Media sites.
            //this.WebTemplateCore.ShowSharePageLink = true; //this could be set in the web.config, key = "GoC.WebTemplate.showSharePageLink"

            //this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.bitly);
            //this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.facebook);
            //this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.twitter);

            this.WebTemplateCore.ShowSharePageLink = true;
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.bitly);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.linkedin);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.blogger);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.myspace);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.delicious);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.pinterest);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.digg);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.reddit);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.diigo);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.stumbleupon);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.email);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.tumblr);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.facebook);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.twitter);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.gmail);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.yahoomail);
            this.WebTemplateCore.SharePageMediaSites.Add(GoC.WebTemplate.Core.SocialMediaSites.googleplus);

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }

        public ActionResult FooterLinksSample()
        {
            //Contact Links
            this.WebTemplateCore.ContactLink = new Link("http://travel.gc.ca/","Contact Us");

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)
            return View();
        }
        public ActionResult LeavingSecureSiteSample()
        {
            //note: other then the message the rest could be set in the web.config
            this.WebTemplateCore.LeavingSecureSiteWarning.Enabled = true;
            this.WebTemplateCore.LeavingSecureSiteWarning.RedirectURL = "Redirect";
            this.WebTemplateCore.LeavingSecureSiteWarning.ExcludedDomains = "www.esdc.gc.ca, esdc.gc.ca, jobbank.gc.ca";
            this.WebTemplateCore.LeavingSecureSiteWarning.Message = "You are leaving a secure session sample text!";
           
            return View();
        }

        public ActionResult LeftSideMenuSample()
        {
            GoC.WebTemplate.MenuSection leftMenu = new GoC.WebTemplate.MenuSection();

            //set the header for this section of the menu
            leftMenu.Name = "Section A";
            leftMenu.Link = "http://www.servicecanada.gc.ca";
            leftMenu.OpenInNewWindow = true;
            //set the links for this section of the menu
            leftMenu.Items.Add(new GoC.WebTemplate.MenuItem("http://www.tsn.ca", "TSN", new GoC.WebTemplate.MenuItem[] { 
                                                            new GoC.WebTemplate.MenuItem("http://www.cbc.ca", "sub 1", true), 
                                                            new GoC.WebTemplate.MenuItem("http://www.rds.ca", "sub 2") }));
            leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.cnn.ca", "CNN"));

            //add section to template
            this.WebTemplateCore.LeftMenuItems.Add(leftMenu);

            //or can be done with a 1 liner
            this.WebTemplateCore.LeftMenuItems.Add(new GoC.WebTemplate.MenuSection("Section B", "http://www.canada.ca", new GoC.WebTemplate.Link[] { 
                                                                                new GoC.WebTemplate.Link("http://www.rds.ca", "RDS"), 
                                                                                new GoC.WebTemplate.Link("http://www.lapresse.com", "La Presse") }));

            return View();
        }

        public ActionResult SessionTimeoutSample()
        {
            //We will set the server session timeout for this page only, to 1 min
            Session.Timeout = 1;

            //Let's display what is in the session on the page
            //We will populate the session with the time
            if (this.Session["stuff"] != null)
            {
                ViewBag.Stuff = (this.Session["stuff"]).ToString();
            }
            else
            {
                ViewBag.Stuff = "Data from session: It is empty, refresh page to have value";
                this.Session.Add("stuff", string.Concat("Data from session: ", DateTime.Now.ToString()));
            }
            //enable the feature
            
            this.WebTemplateCore.SessionTimeout.Enabled = true;
            this.WebTemplateCore.SessionTimeout.Inactivity = 30000;
            this.WebTemplateCore.SessionTimeout.ReactionTime = 10000;
            this.WebTemplateCore.SessionTimeout.SessionAlive = 30000;
            this.WebTemplateCore.SessionTimeout.LogoutUrl = "Logout";
            this.WebTemplateCore.SessionTimeout.RefreshCallbackUrl = "SessionValidity";
            this.WebTemplateCore.SessionTimeout.RefreshOnClick = false;
            this.WebTemplateCore.SessionTimeout.RefreshLimit = 3;
            this.WebTemplateCore.SessionTimeout.Method = "";
            this.WebTemplateCore.SessionTimeout.AdditionalData = "";

            return View();
        }

        public ActionResult TransactionalSample()
        {
            //Contact Links
            this.WebTemplateCore.ContactLink = new Link("http://travel.gc.ca/", "Contact Us");
            //set the Terms and Condition Link
            this.WebTemplateCore.TermsConditionsLinkURL = "http://www.tsn.ca";
            //set the Privacy link
            this.WebTemplateCore.PrivacyLinkURL = "http://www.lapresse.ca";

            return View();
        }
               
        [HttpPost]
        public ActionResult TransactionalSample(string data1, string data2, string data4)
        {
            //set the Terms and Condition Link
            this.WebTemplateCore.TermsConditionsLinkURL = "http://www.tsn.ca";
            //set the Privacy link
            this.WebTemplateCore.PrivacyLinkURL = "http://www.lapresse.ca";
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
            string targetURL = Server.HtmlDecode(this.Request.QueryString.Get("targetUrl"));

            //add any necessary clean up code (clear session, logout user, etc...)

            //redirect user to link they had clicked
            if (!string.IsNullOrEmpty(targetURL))
            {  
                return Redirect(targetURL);
            }
            else
            {
                // decide how you want to handle this situation
                return View();
            }               
        }

        public ActionResult Logout()
        {
            //This sample page is referenced by the "logoutUrl" setting of the WET SessionTimeout control
            //It will not be displayed to the user, but is used to perform session clean up and any other clean up that is required before the user is loged out of your application.
            //This page will then redirect to the page you identify example the login page (in our case the BaseSettingsSample.aspx)

            //destroy users sessions
            this.Session.Abandon();

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
            return View();
        }

        public ActionResult GCIntranetThemeSample()
        {

            this.WebTemplateCore.Environment = "ESDC_PROD";
            this.WebTemplateCore.UseHTTPS = true;
            this.WebTemplateCore.ApplicationTitle.Text = "My Custom Title";
            this.WebTemplateCore.ApplicationTitle.Href = "http://iservice.prv/eng/index.shtml";  
            
            return View();
        }
	}
}