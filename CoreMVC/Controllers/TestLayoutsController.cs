using GoC.WebTemplate.Components.Core.Services;
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
            //WebTemplateModel.HidePlaceholderMenu = true;
            return View();
        }

        [HttpGet]
        public IActionResult DefaultLeftMenu(string env)
        {
            SetEnv(env);
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
            return View();
        }

        [HttpGet]
        public IActionResult ApplicationLeftMenu(string env)
        {
            SetEnv(env);
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
