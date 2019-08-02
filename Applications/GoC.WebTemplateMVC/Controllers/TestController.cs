using System.Collections.Generic;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using System.Web.Mvc;
using GoC.WebTemplate.Components;

namespace GoC.WebTemplate.MVC.Controllers
{
    public class TestController : WebTemplateBaseController
    {


        public ActionResult BilingualErrorPage()
        {
            WebTemplateCore.HeaderTitle = "Bilingual Error Test Page";
            return View();
        }

        public ActionResult UnilingualErrorPage()
        {
            WebTemplateCore.HeaderTitle = "Unilingual Error Test Page";
            return View();
        }
        public ActionResult SubThemeTestPage()
        {
            WebTemplateCore.Environment = "ESDC_PROD";
            WebTemplateCore.UseHTTPS = false;
            WebTemplateCore.HeaderTitle = "SubTheme Test Page";
            WebTemplateCore.IntranetTitle = new IntranetTitle
            {
                Acronym = "yoyo",
                BoldText = "BOLD",
                Href = "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/samples/subtheme-eccc-en.shtml",
                Text = "CDTS Example"
            };
            AddABasicLeftMenu();

            return View();
        }

        public ActionResult NoCustomSearchTestPage()
        {
            AddABasicLeftMenu();
            AddBasicBreadcrumbs();

            return View();
        }

        private void AddBasicBreadcrumbs()
        {
            WebTemplateCore.Breadcrumbs = new List<Breadcrumb>
            {
                new Breadcrumb
                {
                    Href= "lolsjk",
                    Title = "Home",
                },
                new Breadcrumb
                {
                    Title = "This Page"
                }
            };
        }

        private void AddABasicLeftMenu()
        {
            WebTemplateCore.LeftMenuItems = new List<MenuSection>
            {
                new MenuSection
                {
                    Name = "My Left Menu",
                    Items = new List<Link>
                    {
                        new MenuItem
                        {
                            Text = "Item 1",
                            Href = "Test/SubThemeTestPage",
                            SubItems = new List<MenuItem>
                            {
                                new MenuItem
                                {
                                    Text = "Hello!",
                                    Href = "n/a"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}