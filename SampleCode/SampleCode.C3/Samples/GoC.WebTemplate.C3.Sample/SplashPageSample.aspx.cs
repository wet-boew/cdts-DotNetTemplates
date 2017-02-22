using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleCode.C3.Samples
{
    public partial class SplashPageSample : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WebTemplateCore = new GoC.WebTemplate.Core();
        }
        public GoC.WebTemplate.Core WebTemplateCore { get; set; }
    }
}