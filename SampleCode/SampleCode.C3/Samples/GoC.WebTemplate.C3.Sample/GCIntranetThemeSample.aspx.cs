using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleCode.C3.Samples
{
    public partial class GCIntranetThemeSample : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WebTemplateMaster.WebTemplateCore.WebTemplateTheme = "GCIntranet";
            this.WebTemplateMaster.WebTemplateCore.WebTemplateSubTheme = "ESDC";
            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle_Text = "My Custom Title";
            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle_URL = "http://iservice.prv/eng/index.shtml";
            this.WebTemplateMaster.WebTemplateCore.Environment = GoC.WebTemplate.Core.CDTSEnvironments.ESDCPROD.ToString();
        }
    }
}