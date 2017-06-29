using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoC.WebTemplate;
using GoC.WebTemplate.Proxies;
using WebTemplateCore.Proxies;

public partial class SplashPageSample : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If not using the webtemplate master page you must pass GoC.WebTemplate.Proxies.CurrentRequestProxy and 
            //GoC.WebTemplate.Proxies.ConfigurationProxy objects to the core.
            this.WebTemplateCore = new Core(new CurrentRequestProxy(),
                                            new CacheProxy(),
                                            new ConfigurationProxy(), 
                                            new CDTSEnvironmentLoader(new CacheProxy()).LoadCDTSEnvironments("~/CDTSEnvironments.json"));
        }
        public GoC.WebTemplate.Core WebTemplateCore { get; set; }
    }