using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;
using System.Web.Mvc;
using GoC.WebTemplate.Components;

namespace GoC.WebTemplate.MVC.Controllers
{
    public class TestController : WebTemplateBaseController
    {


        public ActionResult BilingualErrorPage()
        {
            WebTemplateModel.HeaderTitle = "Bilingual Error Test Page";
            return View();
        }

        public ActionResult UnilingualErrorPage()
        {
            WebTemplateModel.HeaderTitle = "Unilingual Error Test Page";
            return View();
        }
        public ActionResult SubThemeTestPage()
        {
            WebTemplateModel.Settings.Environment = "ESDC_PROD";
            WebTemplateModel.Settings.UseHttps = false;
            WebTemplateModel.HeaderTitle = "SubTheme Test Page";
            WebTemplateModel.IntranetTitle = new IntranetTitle
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
            WebTemplateModel.Breadcrumbs = new List<Breadcrumb>
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
            WebTemplateModel.LeftMenuItems = new List<MenuSection>
            {
                new MenuSection
                {
                    Text = "My Left Menu",
                    Items = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Text = "Item 1",
                            Href = "Test/SubThemeTestPage",
                            SubItems = new List<Link>
                            {
                                new Link
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