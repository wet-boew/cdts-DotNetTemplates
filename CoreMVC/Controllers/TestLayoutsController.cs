using System.Collections.Generic;

using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.Components.Entities;

using Microsoft.AspNetCore.Mvc;

namespace GoC.WebTemplate.CoreMVC.Controllers
{
    public class TestLayoutsController : WebTemplateBaseController
    {
        public TestLayoutsController(IModelAccessor modelAccessor)
            : base(modelAccessor)
        {
        }

        [HttpGet]
        public IActionResult Default(string env)
        {
            SetEnv(env);
            return View();
        }

        [HttpGet]
        public IActionResult DefaultLeftMenu(string env)
        {
            SetEnv(env);

            var item = new MenuItem
            {
                Href = "",
                Text = "Sports",
                SubItems = new List<Link> {
                    new Link { Href="http://www.nhl.com", Text="NHL", NewWindow= true },
                    new Link { Href="http://www.mlb.com", Text="MLB" }
                }
            };

            //add section to template
            WebTemplateModel.LeftMenuItems = new List<MenuSection>
            {
                new MenuSection
                {
                    Text = "Section A",
                    Href = "http://www.google.ca",
                    NewWindow = true,
                    Items = new List<MenuItem> {
                        new MenuItem { Href = "http://www.tsn.ca", Text = "TSN" },
                        item
                    }
                }
            };

            return View();
        }

        [HttpGet]
        public IActionResult SplashPage(string env)
        {
            SetEnv(env);
            return View();
        }

        [HttpGet]
        public IActionResult ErrorPageBilingual(string env)
        {
            SetEnv(env);
            return View();
        }

        [HttpGet]
        public IActionResult ErrorPageUnilingual(string env)
        {
            SetEnv(env);
            return View();
        }

        [HttpGet]
        public IActionResult Application(string env)
        {
            SetEnv(env);

            WebTemplateModel.ApplicationTitle.Text = "Application Title";

            return View();
        }

        [HttpGet]
        public IActionResult ApplicationLeftMenu(string env)
        {
            SetEnv(env);

            WebTemplateModel.ApplicationTitle.Text = "Application Title";

            var item = new MenuItem
            {
                Href = "",
                Text = "Sports",
                SubItems = new List<Link> {
                    new Link { Href="http://www.nhl.com", Text="NHL", NewWindow= true },
                    new Link { Href="http://www.mlb.com", Text="MLB" }
                }
            };

            //add section to template
            WebTemplateModel.LeftMenuItems = new List<MenuSection>
            {
                new MenuSection
                {
                    Text = "Section A",
                    Href = "http://www.google.ca",
                    NewWindow = true,
                    Items = new List<MenuItem> {
                        new MenuItem { Href = "http://www.tsn.ca", Text = "TSN" },
                        item
                    }
                }
            };

            return View();
        }

        [HttpGet]
        public IActionResult Transactional(string env)
        {
            SetEnv(env);
            return View();
        }

        [HttpGet]
        public IActionResult TransactionalLeftMenu(string env)
        {
            SetEnv(env);

            var item = new MenuItem
            {
                Href = "",
                Text = "Sports",
                SubItems = new List<Link> {
                    new Link { Href="http://www.nhl.com", Text="NHL", NewWindow= true },
                    new Link { Href="http://www.mlb.com", Text="MLB" }
                }
            };

            //add section to template
            WebTemplateModel.LeftMenuItems = new List<MenuSection>
            {
                new MenuSection
                {
                    Text = "Section A",
                    Href = "http://www.google.ca",
                    NewWindow = true,
                    Items = new List<MenuItem> {
                        new MenuItem { Href = "http://www.tsn.ca", Text = "TSN" },
                        item
                    }
                }
            };

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

    }
}
