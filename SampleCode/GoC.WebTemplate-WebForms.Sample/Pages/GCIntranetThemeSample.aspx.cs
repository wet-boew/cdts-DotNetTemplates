using System;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class GCIntranetThemeSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebTemplateMaster.WebTemplateCore.UseHTTPS = true;
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "My Custom Title";
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Href = "http://iservice.prv/eng/index.shtml";
            WebTemplateMaster.WebTemplateCore.Environment = "ESDC_PROD";
        }
    }
}