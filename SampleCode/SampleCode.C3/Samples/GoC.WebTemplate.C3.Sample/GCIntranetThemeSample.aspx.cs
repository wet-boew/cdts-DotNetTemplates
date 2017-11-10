using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public partial class GCIntranetThemeSample : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WebTemplateMaster.WebTemplateCore.UseHTTPS = true;
            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "My Custom Title";
            this.WebTemplateMaster.WebTemplateCore.ApplicationTitle.Href = "http://iservice.prv/eng/index.shtml";
            this.WebTemplateMaster.WebTemplateCore.Environment = "ESDC_PROD";
        }
    }