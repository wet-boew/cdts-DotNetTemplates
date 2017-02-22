using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace GoC.WebTemplate
{
    public partial class TestWebTemplatePage : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string jsPathPlugin = string.Concat(this.WebTemplateMaster.WebTemplateCore.CDNPath, "plugins-en.js");
            //this.WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add(string.Concat("<script type='text/javascript' src='", jsPathPlugin, "'></script>"));
            //this.WebTemplateMaster.WebTemplateCore.HTMLBodyElements.Add("<script type='text/javascript'>var youtube = document.getElementById('cdn-youtube');youtube.outerHTML = wet.plugins.youtube({title: 'Suspect',url: 'http://www.youtube.com/watch?v=9znKJqnFuuY',transcript: 'http://healthycanadians.gc.ca/video/suspect-eng.php#trans'});</script>");
            
//BASIC SETTINGS ====================================

            this.WebTemplateMaster.WebTemplateCore.Environment = "ESDCPRod";

           // this.WebTemplateMaster.WebTemplateCore.WebTemplateVersion = "v4_0_21";
            //this.WebTemplateMaster.WebTemplateCore.UseHTTPS = false;
            //this.WebTemplateMaster.WebTemplateCore.LoadJQueryFromGoogle = false;
           // this.WebTemplateMaster.WebTemplateCore.ShowLanguageLink = false;
            this.WebTemplateMaster.WebTemplateCore.ScreenIdentifier = "993jjd9-33";

            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle_URL = "http://www.pinkbike.com";
            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle_Text = "YUP";

            this.WebTemplateMaster.WebTemplateCore.WebTemplateTheme = "gcweb";

            this.WebTemplateMaster.WebTemplateCore.HeaderTitle = "Sample Page";

            this.WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta name='description' content='My Description'>");

          //  this.WebTemplateMaster.WebTemplateCore.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);

           // this.WebTemplateMaster.WebTemplateCore.DateModified = Convert.ToDateTime("16 December 2015");
            this.WebTemplateMaster.WebTemplateCore.DateModified = Convert.ToDateTime("9 january 2015");


            this.WebTemplateMaster.WebTemplateCore.VersionIdentifier = "AA927823737.00.99";
            
            //this.WebTemplateMaster.LanguageLink_URL = "http://www.tsn.com";
           // this.WebTemplateMaster.WebTemplateCore.LanguageLink_URL = "../lang.aspx";

            this.WebTemplateMaster.WebTemplateCore.ShowFeatures = true;

            //this.WebTemplateMaster.WebTemplateCore.ShowFeedbackLink = true;
            //this.WebTemplateMaster.WebTemplateCore.FeedbackLink_URL = "http://www.tsn.ca";
           
            this.WebTemplateMaster.WebTemplateCore.ShowSearch = true;

            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Enabled = false;
            //this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Inactivity = 20000;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.ReactionTime = 20001;
            //this.WebTemplateMaster.WebTemplateCore.SessionTimeout.SessionAlive = 20002;
            //this.WebTemplateMaster.WebTemplateCore.SessionTimeout.LogoutUrl = "20003";
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshCallbackUrl = "20005";
            //this.WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshOnClick = "20006";
            //this.WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshLimit = 20007;
            //this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Method = "20008";
            //this.WebTemplateMaster.WebTemplateCore.SessionTimeout.AdditionalData = "20009";


            this.WebTemplateMaster.WebTemplateCore.TermsConditionsLink_URL = "http://www.pinkbike.com";
            this.WebTemplateMaster.WebTemplateCore.PrivacyLink_URL = "http://www.lapresse.ca";

//BREADCRUMB ====================================
            //this.WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.canada.ca/en/index.htm", "l'Homéêçå & gamble", "l'abc&fich"));
            //this.WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("", "CDN Sample", "Content Delivery Network Sample"));

//FOOTER Links SECTIONS ====================================
            this.WebTemplateMaster.WebTemplateCore.ContactLinks.Add(new Link("http://www.cnn.com", "C'Link 4"));
            this.WebTemplateMaster.WebTemplateCore.ContactLinks.Add(new Link("http://www.tsn.com", "CLink 5"));

            this.WebTemplateMaster.WebTemplateCore.NewsLinks.Add(new Link("#", "NLink3"));
            this.WebTemplateMaster.WebTemplateCore.NewsLinks.Add(new Link("#", "NLink5"));

            this.WebTemplateMaster.WebTemplateCore.AboutLinks.Add(new Link("#", "ALink6"));
            this.WebTemplateMaster.WebTemplateCore.AboutLinks.Add(new Link("#", "ALink7"));

            
//Share this page LINK ====================================
            this.WebTemplateMaster.WebTemplateCore.ShowSharePageLink = true;

            this.WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.bitly);
            this.WebTemplateMaster.WebTemplateCore.SharePageMediaSites.Add(Core.SocialMediaSites.facebook);


//LEFT MENU ====================================
            GoC.WebTemplate.MenuSection leftmen = new GoC.WebTemplate.MenuSection();

            leftmen.Name = "menu aslfkjsaklj";
            leftmen.Link = "http://www.pinkbike.com";
            leftmen.OpenInNewWindow = true;
            leftmen.Items.Add(new GoC.WebTemplate.MenuItem("http://www.tsn.ca", "aaa", new GoC.WebTemplate.MenuItem[] { 
                                                                                new GoC.WebTemplate.MenuItem("http://www.rds.ca", "sub 1", true), 
                                                                                new GoC.WebTemplate.MenuItem("http://www.lapresse.com", "sub 2") }));
            leftmen.Items.Add(new GoC.WebTemplate.MenuItem("http://www.cnn.ca", "bbb", true));

            this.WebTemplateMaster.WebTemplateCore.LeftMenuItems.Add(leftmen);
       
            ////set the header for this section of the menu
            //leftMenu.Name = "Section A";
            ////set the links for this section of the menu
            //leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.tsn.ca", "TSN"));
            //leftMenu.Items.Add(new GoC.WebTemplate.Link("http://www.cnn.ca", "CNN"));

            //add section to template
            //this.WebTemplateMaster.LeftMenuItems.Add(leftMenu);

            //or can be done with a 1 liner
            this.WebTemplateMaster.WebTemplateCore.LeftMenuItems.Add(new GoC.WebTemplate.MenuSection("l'index Section B", new GoC.WebTemplate.Link[] { 
                                                                                new GoC.WebTemplate.Link("http://www.rds.ca", "RDS"), 
                                                                                new GoC.WebTemplate.Link("http://www.lapresse.com", "L'a Presse") }));


            //this.WebTemplateMaster.WebTemplateCore.LeftMenuItems.Add(new GoC.WebTemplate.MenuSection("Section A", "http://www.canada.ca", new GoC.WebTemplate.Link[] { }));

           // GoC.WebTemplate.MenuSection leftMenu = new GoC.WebTemplate.MenuSection();

//Leaving Secure site ====================================
            //this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning_Enabled = true;
            //this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning_DisplayModalWindow = true;
            //this.WebTemplateMaster.WebTemplateCore.leavingSecureSiteWarning_ExcludedDomains = "www.esdc.ca";
            
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning_Enabled = false;
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning_DisplayModalWindow = true;            
            this.WebTemplateMaster.WebTemplateCore.LeavingSecureSiteWarning_Message = "Y'éou are about to leave a secure site, do you wish to continue?223";
            this.WebTemplateMaster.WebTemplateCore.leavingSecureSiteWarning_RedirectURL = "webform1.aspx";

//HTML HEADER/BODY ELEMENTS ====================================
            //this.WebTemplateMaster.HTMLHeaderElements.Add("jones.css");
            //this.WebTemplateMaster.HTMLHeaderElements.Add("<link rel='stylesheet' type='text/css' href='mystyle.css'>");
            //this.WebTemplateMaster.HTMLHeaderElements.Add("<script type='text/javascript' src='javascript.js'></script>");

            //this.WebTemplateMaster.HTMLHeaderElements.Add("<!--my comments-->");

            //this.WebTemplateMaster.HTMLBodyElements.Add("<link rel='stylesheet' type='text/css' href='mystyle.css'>");
            //this.WebTemplateMaster.HTMLBodyElements.Add("<script type='text/javascript' src='javascript.js'></script>");


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

            this.WebTemplateMaster.WebTemplateCore.HTMLBodyElements.Add(sb.ToString());

        //this.WebTemplateMaster.HTMLBodyElements.Add("<script type='text/javascript' src='./GoC.WebTemplate/blabla.js'></script>");

           

             
        }
    }
}