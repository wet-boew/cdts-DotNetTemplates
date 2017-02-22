using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using GoC.WebTemplate;

namespace GoC.WebTemplateMVC.Controllers
{
    public class TestWebTemplatePageController : GoC.WebTemplate.WebTemplateBaseController
    {
        //
        // GET: /TestWebTemplatePage/
        public ActionResult TestPage()
        { 
            //BASIC SETTINGS ====================================
           // this.WebTemplateCore.WebTemplateTheme = "GCWeb";
            this.WebTemplateCore.Environment = "akamai";
            this.WebTemplateCore.WebTemplateSubTheme = "esdc";
            this.WebTemplateCore.HeaderTitle = "Sample Page";

            this.WebTemplateCore.ScreenIdentifier = "897987sadfjkkla--33";

            this.WebTemplateCore.ApplicationTitle_Text = "HELLO";

            this.WebTemplateCore.HTMLHeaderElements.Add("<meta name='description' content='My Description'>");

            this.WebTemplateCore.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);

            this.WebTemplateCore.VersionIdentifier = "AA927823737.00.99";

            //this.WebTemplateCore.LanguageLink_URL = "http://www.tsn.com";
           // this.WebTemplateCore.LanguageLink_URL = "../lang.aspx";

         //   this.WebTemplateCore.LanguageLink_URL = "../TestWebTemplatePage/ChangeCulture?GoCTemplateCulture=fr-CA";

            this.WebTemplateCore.ShowFeatures = true;

            this.WebTemplateCore.ShowFeedbackLink = true;

            this.WebTemplateCore.ShowSearch = true;

            this.WebTemplateCore.SessionTimeout.Enabled = true;
            this.WebTemplateCore.SessionTimeout.Inactivity = 20000;
            this.WebTemplateCore.SessionTimeout.ReactionTime = 20001;
            this.WebTemplateCore.SessionTimeout.SessionAlive = 20002;
            this.WebTemplateCore.SessionTimeout.LogoutUrl = "20003";
            this.WebTemplateCore.SessionTimeout.RefreshCallbackUrl = "20005";
            this.WebTemplateCore.SessionTimeout.RefreshOnClick = false;
            this.WebTemplateCore.SessionTimeout.RefreshLimit = 20007;
            this.WebTemplateCore.SessionTimeout.Method = "20008";
            this.WebTemplateCore.SessionTimeout.AdditionalData = "20009";

            this.WebTemplateCore.TermsConditionsLink_URL = "http://www.pinkbike.com";
            this.WebTemplateCore.PrivacyLink_URL = "http://www.lapresse.ca";

            //BREADCRUMB ====================================
            this.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.canada.ca/en/index.htm", "l'Homéêçå & gamble", "l'abc&fich"));
            this.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("", "CDN Sample", "Content Delivery Network Sample"));

            //FOOTER Links SECTIONS ====================================
            this.WebTemplateCore.ContactLinks.Add(new Link("http://www.cnn.com", "C'Link 4"));
            this.WebTemplateCore.ContactLinks.Add(new Link("http://www.tsn.com", "CLink 5"));

            this.WebTemplateCore.NewsLinks.Add(new Link("#", "NLink3"));
            this.WebTemplateCore.NewsLinks.Add(new Link("#", "NLink5"));

            this.WebTemplateCore.AboutLinks.Add(new Link("#", "ALink6"));
            this.WebTemplateCore.AboutLinks.Add(new Link("#", "ALink7"));
            
            //Share this page LINK ====================================
            this.WebTemplateCore.ShowSharePageLink = true;

            this.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.bitly);
            this.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.facebook);


            //LEFT MENU ====================================
            GoC.WebTemplate.MenuSection leftmen = new GoC.WebTemplate.MenuSection();

            leftmen.Name = "menu aslfkjsaklj";
            
            leftmen.Items.Add(new GoC.WebTemplate.Link("http://www.tsn.ca", "aaa"));
            leftmen.Items.Add(new GoC.WebTemplate.Link("http://www.cnn.ca", "bbb"));

            this.WebTemplateCore.LeftMenuItems.Add(leftmen);

            ////set the header for this section of the menu
            //leftMenu.Name = "Section A";
            ////set the links for this section of the menu
            //leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.tsn.ca", "TSN"));
            //leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.cnn.ca", "CNN"));

            //add section to template
            //this.WebTemplateCore.LeftMenuItems.Add(leftMenu);

            //or can be done with a 1 liner
            this.WebTemplateCore.LeftMenuItems.Add(new GoC.WebTemplate.MenuSection("l'index Section B", "http://www.pinkbike.com", new GoC.WebTemplate.Link[] { 
                                                                                new GoC.WebTemplate.MenuItem("http://www.rds.ca", "RDS", true, new GoC.WebTemplate.MenuItem[] { 
                                                                                    new GoC.WebTemplate.MenuItem("http://www.rds.ca", "sub 1", true), 
                                                                                    new GoC.WebTemplate.MenuItem("http://www.lapresse.com", "sub 2") }), 
                                                                                new GoC.WebTemplate.Link("http://www.lapresse.com", "L'a Presse") }));

            // GoC.WebTemplate.MenuSection leftMenu = new GoC.WebTemplate.MenuSection();

            //Leaving Secure site ====================================
            this.WebTemplateCore.LeavingSecureSiteWarning_Enabled = true;
            this.WebTemplateCore.LeavingSecureSiteWarning_DisplayModalWindow = false;
            this.WebTemplateCore.leavingSecureSiteWarning_ExcludedDomains = "www.redseal.ca";

            this.WebTemplateCore.LeavingSecureSiteWarning_Message = "Y'éou are about to leave a secure site, do you wish to continue?223";

            //HTML HEADER/BODY ELEMENTS ====================================
            //this.WebTemplateCore.HTMLHeaderElements.Add("jones.css");
            //this.WebTemplateCore.HTMLHeaderElements.Add("<link rel='stylesheet' type='text/css' href='mystyle.css'>");
            //this.WebTemplateCore.HTMLHeaderElements.Add("<script type='text/javascript' src='javascript.js'></script>");

            //this.WebTemplateCore.HTMLHeaderElements.Add("<!--my comments-->");

            //this.WebTemplateCore.HTMLBodyElements.Add("<link rel='stylesheet' type='text/css' href='mystyle.css'>");
            //this.WebTemplateCore.HTMLBodyElements.Add("<script type='text/javascript' src='javascript.js'></script>");


            System.Text.StringBuilder sb = new System.Text.StringBuilder();


            sb.AppendLine("<script>");
            sb.AppendLine("(function( $, wb ) {");
            sb.AppendLine("\"use strict\";");

            sb.AppendLine("$( document ).on( \"click vclick\", \"#increaseMeter, #decreaseMeter\", function( event ) {");
            sb.AppendLine("var $elm = $( \"#updateTest\" ),");
            sb.AppendLine("increase = event.currentTarget.id === \"increaseMeter\",");
            sb.AppendLine("valuenow = parseInt( $elm.attr( \"value\" ), 10 ),");
            sb.AppendLine("limit = parseInt( $elm.attr( increase ? \"max\" : \"min\" ), 10 ),");
            sb.AppendLine("change = increase ? 1 : -1,");
            sb.AppendLine("newValue = valuenow === limit ? 0 : valuenow + change;");

            sb.AppendLine("$elm");

            sb.AppendLine(".attr( \"value\", newValue )");
            sb.AppendLine(".find( \"span\" )");
            sb.AppendLine(".text( newValue );");

            sb.AppendLine("$elm.trigger( \"wb-update.wb-meter\" );");
            sb.AppendLine("});");

            sb.AppendLine("})( jQuery, wb );");
            sb.AppendLine("</script>");

            this.WebTemplateCore.HTMLBodyElements.Add(sb.ToString());

            //this.WebTemplateCore.HTMLBodyElements.Add("<script type='text/javascript' src='./GoC.WebTemplate/blabla.js'></script>");
            
            return View();
        }

        //public RedirectResult RedirectTest()
        //{
        //    return Redirect("http://www.lapresse.com");
        //}

        //public RedirectToRouteResult RedirecttoRouteTest()
        //{
        //    return RedirectToAction("Index");
        //}
       
        //public FileStreamResult FileResutTest()
        //{
        //    string name = "C://sf-ml/me.txt";
        //    System.IO.FileInfo info = new System.IO.FileInfo(name);
        //    if (!info.Exists)
        //    {
        //        using (System.IO.StreamWriter writer = info.CreateText())
        //        {
        //            writer.WriteLine("Hello, I am a new text file");
        //        }
        //    }
        //    return File(info.OpenRead(), "text/plain");
        //}

        //public ActionResult EmptyRestultTest()
        //{
        //    return EmptyResult();
        //}

        //public PartialViewResult PartialVR()
        //{
        //    return PartialView(); tested in separate project
        //}

        //public ContentResult ContentResultTest()
        //{
        //    return Content();
        //}

    }
}