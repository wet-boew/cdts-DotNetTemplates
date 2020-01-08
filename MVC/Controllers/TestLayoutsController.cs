using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;
using System.Web.Mvc;
using GoC.WebTemplate.Components;

namespace GoC.WebTemplate.MVC.Controllers
{
    public class TestLayoutsController : WebTemplateBaseController
    {
        public ActionResult Default(string env)
        {
            SetEnv(env);
            AddBasicBreadcrumbs();
            return View();
        }

        public ActionResult DefaultLeftMenu(string env)
        {
            SetEnv(env);
            AddABasicLeftMenu();
            AddBasicBreadcrumbs();
            return View();
        }

        public ActionResult SplashPage(string env)
        {
            SetEnv(env);
            return View();
        }

        public ActionResult ErrorPageBilingual(string env)
        {
            SetEnv(env);
            return View();
        }

        public ActionResult ErrorPageUnilingual(string env)
        {
            SetEnv(env);
            return View();
        }

        public ActionResult Application(string env)
        {
            SetEnv(env);
            AddBasicBreadcrumbs();
            return View();
        }

        public ActionResult ApplicationLeftMenu(string env)
        {
            SetEnv(env);
            AddABasicLeftMenu();
            AddBasicBreadcrumbs();
            return View();
        }

        public ActionResult Transactional(string env)
        {
            SetEnv(env);
            return View();
        }

        public ActionResult TransactionalLeftMenu(string env)
        {
            SetEnv(env);
            AddABasicLeftMenu();
            AddBasicBreadcrumbs();
            return View();
        }

        private void SetEnv(string env)
        {
            WebTemplateModel.Settings.Environment = string.IsNullOrEmpty(env) ? "AKAMAI" : env;
            if (WebTemplateModel.Settings.Environment.Equals("ESDC_PROD"))
            {
                WebTemplateModel.Settings.UseHttps = false;
            }
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