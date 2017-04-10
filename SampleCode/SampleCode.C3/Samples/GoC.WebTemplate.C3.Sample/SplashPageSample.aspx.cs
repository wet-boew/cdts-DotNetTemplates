using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoC.WebTemplate.Proxies;

namespace SampleCode.C3.Samples
{
    public partial class SplashPageSample : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If not using the webtemplate master page you must pass GoC.WebTemplate.Proxies.CurrentRequestProxy and 
            //GoC.WebTemplate.Proxies.ConfigurationProxy objects to the core.
            this.WebTemplateCore = new GoC.WebTemplate.Core(new CurrentRequestProxy(), new ConfigurationProxy());
        }
        public GoC.WebTemplate.Core WebTemplateCore { get; set; }
    }
}