using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Globalization;
using GoC.WebTemplate.Components.Entities;

namespace GoC.WebTemplate.MVC.Controllers
{
    public class TestWebTemplatePageController : WebTemplateBaseController
    {

        public ActionResult LeftMenuSecure()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            WebTemplateCore.LeftMenuItems = new List<MenuSection>
            {
                new MenuSection
                {
                    Items = new List<Link> { new Link {
                            Href = "",
                            Text = "Menu Item 1"
                        }, new Link {
                            Href= "#",
                            Text = "Menu Item 2"
                        }
                    } ,
                    Href = "#",
                    Text  = "MenuSection",
                    NewWindow = false
                },
                new MenuSection{
                    Text="Principale",
                    Items= new List<Link> {
                        new MenuItem{
                            Href="linka",
                            Text="Secondaire",
                            SubItems= new List<MenuItem> {
                                new MenuItem{
                                    Href="linkb",
                                    Text="Tertiaire"
                                }
                            }
                        }
                    }
                }
            };
            return View("HelloWorldLeftMenu");
        }
        public ActionResult HelloWorld()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Web Template";
            WebTemplateCore.Breadcrumbs = null;
            return View();
        }

        public ActionResult StandardPage()
        {
            WebTemplateCore.AppSettingsURL = "http://tempuri.com";
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            return View("HelloWorld");
        }

        public ActionResult Secure()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            return View("HelloWorld");
        }

        public ActionResult SignIn()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            WebTemplateCore.ShowSignInLink = true;
            WebTemplateCore.Settings.SignInLinkUrl = "about//blank";
            return View("HelloWorld");
        }
        public ActionResult SignOut()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            WebTemplateCore.ShowSignOutLink = true;
            WebTemplateCore.Settings.SignOutLinkUrl = "about//blank";
            return View("HelloWorld");
        }
        public ActionResult NoSearch()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowSearch = false;
            WebTemplateCore.Settings.ShowPostContent = false;
            return View("HelloWorld");
        }
        public ActionResult NoMenu()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            return View("HelloWorld");
        }

        public ActionResult CustomMenu()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.CustomSiteMenuURL =
               "https://ssl-templates.services.gc.ca/app/cls/WET/gcweb/" + WebTemplateCore.Settings.Version + "/cdts/ajax/appmenu-en.html";

            WebTemplateCore.Settings.ShowPostContent = false;
            return View("HelloWorld");
        }
        public ActionResult SignInWithCustomMenu()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.CustomSiteMenuURL =
               "https://ssl-templates.services.gc.ca/app/cls/WET/gcweb/" + WebTemplateCore.Settings.Version + "/cdts/ajax/appmenu-en.html";

            WebTemplateCore.Settings.SignInLinkUrl = "about//blank";
            WebTemplateCore.ShowSignInLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            return View("HelloWorld");
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
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;

            return View("HelloWorld");
        }

        public ActionResult StandardFooter()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            return View("HelloWorld");
        }
        public ActionResult CustomFooterGcWeb()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            WebTemplateCore.CustomFooterLinks = new List<Link>
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
            return View("HelloWorld");
        }

        public ActionResult CustomFooterGcIntranet()
        {
            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.Settings.ShowPostContent = false;
            WebTemplateCore.FooterSections = new List<FooterSection>
            {
                new FooterSection
                {
                    SectionName = "Section 1 Custom",
                    CustomFooterLinks = new List<FooterLink>
                    {
                        new FooterLink {Href= "google.ca", Text= "Link 1"},
                        new FooterLink {Href= "#", Text= "Link 2"},
                        new FooterLink {Href= "google.ca", Text= "Link 3", NewWindow= true},
                    }
                },
                new FooterSection
                {
                    SectionName = "Section 2 Custom",
                    CustomFooterLinks = new List<FooterLink>
                    {
                        new FooterLink {Href= "#", Text= "Link 4"},
                        new FooterLink {Href= "#", Text= "Link 5"},
                        new FooterLink {Href= "#", Text= "Link 6"},
                    }
                },
                new FooterSection
                {
                    SectionName = "Section 3 Custom",
                    CustomFooterLinks = new List<FooterLink>
                    {
                        new FooterLink {Href= "#", Text= "Link 7"},
                        new FooterLink {Href= "#", Text= "Link 8"},
                        new FooterLink {Href= "#", Text= "Link 9"},
                    }
                }
            };
            return View("HelloWorld");
        }

        public ActionResult TransactionalFooterCustomLinks()
        {

            WebTemplateCore.ApplicationTitle.Text = "Application Name";
            WebTemplateCore.Breadcrumbs = null;
            WebTemplateCore.LanguageLink.Href = "apptop-fr.html";
            WebTemplateCore.Settings.ShowLanguageLink = true;
            WebTemplateCore.TermsConditionsLink = new FooterLink { Href = "#" };
            WebTemplateCore.PrivacyLink = new FooterLink { Href = "#" };
            return View("HelloWorld");
        }
        //
        // GET: /TestWebTemplatePage/
        public ActionResult TestPage()
        {
            //BASIC SETTINGS ====================================
            // this.WebTemplateCore.WebTemplateTheme = "GCWeb";
            WebTemplateCore.Settings.Environment = "PROD_SSL";
            WebTemplateCore.Settings.UseHttps = null;
            WebTemplateCore.HeaderTitle = "Sample Page";

            WebTemplateCore.ScreenIdentifier = "897987sadfjkkla--33";

            WebTemplateCore.ApplicationTitle.Text = "HELLO";

            WebTemplateCore.HTMLHeaderElements.Add("<meta name='description' content='My Description'>");

            WebTemplateCore.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);

            WebTemplateCore.VersionIdentifier = "AA927823737.00.99";

            //this.WebTemplateCore.LanguageLink_URL = "http://www.tsn.com";
            // this.WebTemplateCore.LanguageLink_URL = "../lang.aspx";

            //   this.WebTemplateCore.LanguageLink_URL = "../TestWebTemplatePage/ChangeCulture?GoCTemplateCulture=fr-CA";

            WebTemplateCore.FeedbackLink.Show = true;

            WebTemplateCore.Settings.ShowSearch = true;

            WebTemplateCore.Settings.SessionTimeout.Enabled = true;
            WebTemplateCore.Settings.SessionTimeout.Inactivity = 20000;
            WebTemplateCore.Settings.SessionTimeout.ReactionTime = 20001;
            WebTemplateCore.Settings.SessionTimeout.SessionAlive = 20002;
            WebTemplateCore.Settings.SessionTimeout.LogoutUrl = "20003";
            WebTemplateCore.Settings.SessionTimeout.RefreshCallBackUrl = "20005";
            WebTemplateCore.Settings.SessionTimeout.RefreshOnClick = false;
            WebTemplateCore.Settings.SessionTimeout.RefreshLimit = 20007;
            WebTemplateCore.Settings.SessionTimeout.Method = "20008";
            WebTemplateCore.Settings.SessionTimeout.AdditionalData = "20009";

            WebTemplateCore.TermsConditionsLink = new FooterLink { Href = "http://www.pinkbike.com" };
            WebTemplateCore.PrivacyLink = new FooterLink { Href = "http://www.lapresse.ca" };

            //BREADCRUMB ====================================
            WebTemplateCore.Breadcrumbs.Add(new Breadcrumb { Href = "http://www.canada.ca/en/index.htm", Title = "l'Homéêçå & gamble", Acronym = "l'abc&fich" });
            WebTemplateCore.Breadcrumbs.Add(new Breadcrumb { Href = "", Title = "CDN Sample", Acronym = "Content Delivery Network Sample" });


            //Share this page LINK ====================================
            WebTemplateCore.Settings.ShowSharePageLink = true;

            WebTemplateCore.SharePageMediaSites.Add(SocialMediaSites.bitly);
            WebTemplateCore.SharePageMediaSites.Add(SocialMediaSites.facebook);


            //LEFT MENU ====================================
            MenuSection leftmen = new MenuSection();

            leftmen.Text = "menu aslfkjsaklj";

            leftmen.Items.Add(new Link { Href = "http://www.tsn.ca", Text = "aaa" });
            leftmen.Items.Add(new Link { Href = "http://www.cnn.ca", Text = "bbb" });

            WebTemplateCore.LeftMenuItems.Add(leftmen);

            ////set the header for this section of the menu
            //leftMenu.Name = "Section A";
            ////set the links for this section of the menu
            //leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.tsn.ca", "TSN"));
            //leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.cnn.ca", "CNN"));

            //add section to template
            //this.WebTemplateCore.LeftMenuItems.Add(leftMenu);

            //or can be done with a 1 liner
            WebTemplateCore.LeftMenuItems.Add(new MenuSection
            {
                Text = "l'index Section B",
                Href = "http://www.pinkbike.com",
                Items = new List<Link>
                {
                    new MenuItem
                    {
                        Href="http://www.rds.ca",
                        Text="RDS",
                        NewWindow= true,
                        SubItems = new List<MenuItem>
                        {
                            new MenuItem
                            {
                                Href="http://www.rds.ca",
                                Text="sub 1",
                                NewWindow= true
                            },
                            new MenuItem
                            {
                                Href="http://www.lapresse.com",
                                Text="sub 2"
                            }
                        }
                    },
                    new Link
                    {
                        Href = "http://www.lapresse.com",
                        Text = "L'a Presse"
                    }
                }
            });

            // GoC.WebTemplate.MenuSection leftMenu = new GoC.WebTemplate.MenuSection();

            //Leaving Secure site ====================================
            WebTemplateCore.Settings.LeavingSecureSiteWarning.Enabled = true;
            WebTemplateCore.Settings.LeavingSecureSiteWarning.DisplayModalWindow = false;
            WebTemplateCore.Settings.LeavingSecureSiteWarning.ExcludedDomains = "www.redseal.ca";
            WebTemplateCore.Settings.LeavingSecureSiteWarning.Message = "You are about to leave a secure site, do you wish to continue?";

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

            WebTemplateCore.HTMLBodyElements.Add(sb.ToString());

            //this.WebTemplateCore.HTMLBodyElements.Add("<script type='text/javascript' src='./GoC.WebTemplate/blabla.js'></script>");

            return View();
        }
        public ActionResult SplashPage()
        {
            WebTemplateCore.SplashPageInfo.EnglishHomeUrl = "http://www.canada.ca/en/index.html";
            WebTemplateCore.SplashPageInfo.FrenchHomeUrl = "http://www.canada.ca/fr/index.html";
            WebTemplateCore.SplashPageInfo.EnglishTermsUrl = "http://www.canada.ca/en/transparency/terms.html";
            WebTemplateCore.SplashPageInfo.FrenchTermsUrl = "http://www.canada.ca/fr/transparence/avis.html";
            WebTemplateCore.SplashPageInfo.EnglishName = "[My web asset]";
            WebTemplateCore.SplashPageInfo.FrenchName = "[Mon actif web]";
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