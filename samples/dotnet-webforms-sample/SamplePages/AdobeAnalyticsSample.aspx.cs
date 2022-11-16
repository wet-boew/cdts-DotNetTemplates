using GoC.WebTemplate.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class AdobeAnalyticsSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Add Analytics
            WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Active = false; //Set to true to activate Adobe Analytics
            WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Environment = GoC.WebTemplate.Components.Entities.WebAnalytics.EnvironmentOption.staging;
            WebTemplateMaster.WebTemplateModel.Settings.WebAnalytics.Version = 1;
        }
    }
}