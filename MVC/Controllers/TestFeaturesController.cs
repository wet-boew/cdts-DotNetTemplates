using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Globalization;
using GoC.WebTemplate.Components.Entities;

namespace GoC.WebTemplate.MVC.Controllers
{
    public class TestFeaturesController : WebTemplateBaseController
    {
        public ActionResult SignIn()
        {
            WebTemplateModel.ApplicationTitle.Text = "Application Name";
            WebTemplateModel.Breadcrumbs = null;
            WebTemplateModel.LanguageLink.Href = "apptop-fr.html";
            WebTemplateModel.Settings.ShowLanguageLink = true;
            WebTemplateModel.Settings.ShowPostContent = false;
            WebTemplateModel.ShowSignInLink = true;
            WebTemplateModel.Settings.SignInLinkUrl = "about//blank";

            return View("TestAppPage");
        }
        public ActionResult SignOut()
        {

            WebTemplateModel.ApplicationTitle.Text = "Application Name";
            WebTemplateModel.Breadcrumbs = null;
            WebTemplateModel.LanguageLink.Href = "apptop-fr.html";
            WebTemplateModel.Settings.ShowLanguageLink = true;
            WebTemplateModel.Settings.ShowPostContent = false;
            WebTemplateModel.ShowSignOutLink = true;
            WebTemplateModel.Settings.SignOutLinkUrl = "about//blank";

            return View("TestAppPage");
        }
        public ActionResult NoSearch()
        {

            WebTemplateModel.ApplicationTitle.Text = "Application Name";
            WebTemplateModel.Breadcrumbs = null;
            WebTemplateModel.LanguageLink.Href = "apptop-fr.html";
            WebTemplateModel.Settings.ShowLanguageLink = true;
            WebTemplateModel.Settings.ShowSearch = false;
            WebTemplateModel.Settings.ShowPostContent = false;

            return View("TestAppPage");
        }

        public ActionResult CustomMenu()
        {
            WebTemplateModel.ApplicationTitle.Text = "Application Name";
            WebTemplateModel.Breadcrumbs = null;
            WebTemplateModel.LanguageLink.Href = "apptop-fr.html";
            WebTemplateModel.Settings.ShowLanguageLink = true;
            WebTemplateModel.CustomSiteMenuURL =
               "https://ssl-templates.services.gc.ca/app/cls/WET/gcweb/" + WebTemplateModel.Settings.Version + "/cdts/ajax/appmenu-en.html";
            WebTemplateModel.Settings.ShowPostContent = false;

            return View("TestAppPage");
        }

        public ActionResult WithBreadCrumbs()
        {
            WebTemplateModel.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb{ Href = "https://www.canada.ca/en.html", Title = "GoC", Acronym = "Government of Canada"  },
                new Breadcrumb { Title = "My application" }
            };
            WebTemplateModel.LanguageLink.Href = "apptop-fr.html";
            WebTemplateModel.Settings.ShowLanguageLink = true;
            WebTemplateModel.Settings.ShowPostContent = false;

            return View("TestPage");
        }

        public ActionResult TestPage()
        {
            //BASIC SETTINGS ====================================
            // this.WebTemplateCore.WebTemplateTheme = "GCWeb";
            WebTemplateModel.Settings.Environment = "PROD_SSL";
            WebTemplateModel.Settings.UseHttps = null;
            WebTemplateModel.HeaderTitle = "Sample Page";

            WebTemplateModel.ScreenIdentifier = "897987sadfjkkla--33";

            WebTemplateModel.ApplicationTitle.Text = "HELLO";

            WebTemplateModel.HTMLHeaderElements.Add("<meta name='description' content='My Description'>");

            WebTemplateModel.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);

            WebTemplateModel.VersionIdentifier = "AA927823737.00.99";

            //this.WebTemplateCore.LanguageLink_URL = "http://www.tsn.com";
            // this.WebTemplateCore.LanguageLink_URL = "../lang.aspx";

            //   this.WebTemplateCore.LanguageLink_URL = "../TestWebTemplatePage/ChangeCulture?GoCTemplateCulture=fr-CA";

            WebTemplateModel.Settings.FeedbackLink.Show = true;

            WebTemplateModel.Settings.ShowSearch = true;

            WebTemplateModel.Settings.SessionTimeout.Enabled = true;
            WebTemplateModel.Settings.SessionTimeout.Inactivity = 20000;
            WebTemplateModel.Settings.SessionTimeout.ReactionTime = 20001;
            WebTemplateModel.Settings.SessionTimeout.SessionAlive = 20002;
            WebTemplateModel.Settings.SessionTimeout.LogoutUrl = "20003";
            WebTemplateModel.Settings.SessionTimeout.RefreshCallBackUrl = "20005";
            WebTemplateModel.Settings.SessionTimeout.RefreshOnClick = false;
            WebTemplateModel.Settings.SessionTimeout.RefreshLimit = 20007;
            WebTemplateModel.Settings.SessionTimeout.Method = "20008";
            WebTemplateModel.Settings.SessionTimeout.AdditionalData = "20009";

            WebTemplateModel.TermsConditionsLink = new FooterLink { Href = "http://www.pinkbike.com" };
            WebTemplateModel.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca" };

            //BREADCRUMB ====================================
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "http://www.canada.ca/en/index.htm", Title = "l'Homéêçå & gamble", Acronym = "l'abc&fich" });
            WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "", Title = "CDN Sample", Acronym = "Content Delivery Network Sample" });


            //Share this page LINK ====================================
            WebTemplateModel.Settings.ShowSharePageLink = true;

            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.bitly);
            WebTemplateModel.SharePageMediaSites.Add(SocialMediaSites.facebook);


            //LEFT MENU ====================================
            MenuSection leftmen = new MenuSection
            {
                Text = "menu aslfkjsaklj"
            };

            leftmen.Items.Add(new MenuItem { Href = "http://www.tsn.ca", Text = "aaa" });
            leftmen.Items.Add(new MenuItem { Href = "http://www.cnn.ca", Text = "bbb" });

            WebTemplateModel.LeftMenuItems.Add(leftmen);

            ////set the header for this section of the menu
            //leftMenu.Name = "Section A";
            ////set the links for this section of the menu
            //leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.tsn.ca", "TSN"));
            //leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.cnn.ca", "CNN"));

            //add section to template
            //this.WebTemplateCore.LeftMenuItems.Add(leftMenu);

            //or can be done with a 1 liner
            WebTemplateModel.LeftMenuItems.Add(new MenuSection
            {
                Text = "l'index Section B",
                Href = "http://www.pinkbike.com",
                Items = new List<MenuItem>
                {
                    new MenuItem
                    {
                        Href="http://www.rds.ca",
                        Text="RDS",
                        NewWindow= true,
                        SubItems = new List<Link>
                        {
                            new Link
                            {
                                Href="http://www.rds.ca",
                                Text="sub 1",
                                NewWindow= true
                            },
                            new Link
                            {
                                Href="http://www.lapresse.com",
                                Text="sub 2"
                            }
                        }
                    },
                    new MenuItem
                    {
                        Href = "http://www.lapresse.com",
                        Text = "L'a Presse"
                    }
                }
            });

            // GoC.WebTemplate.MenuSection leftMenu = new GoC.WebTemplate.MenuSection();

            //Leaving Secure site ====================================
            WebTemplateModel.Settings.LeavingSecureSiteWarning.Enabled = true;
            WebTemplateModel.Settings.LeavingSecureSiteWarning.DisplayModalWindow = false;
            WebTemplateModel.Settings.LeavingSecureSiteWarning.ExcludedDomains = "www.redseal.ca";
            WebTemplateModel.Settings.LeavingSecureSiteWarning.Message = "You are about to leave a secure site, do you wish to continue?";

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

            WebTemplateModel.HTMLBodyElements.Add(sb.ToString());

            //this.WebTemplateCore.HTMLBodyElements.Add("<script type='text/javascript' src='./GoC.WebTemplate/blabla.js'></script>");

            return View();
        }
    }
}