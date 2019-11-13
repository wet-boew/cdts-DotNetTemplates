using System;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class GCIntranetThemeSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set up theme
            WebTemplateMaster.WebTemplateModel.Settings.Environment = "PROD_SSL";

            //custom intranet title
            WebTemplateMaster.WebTemplateModel.IntranetTitle = new IntranetTitle
            {
                Href = "https://ssl-templates.services.gc.ca/app/cls/WET/gcintranet/v4_0_31/cdts/samples/subtheme-esdc-en.shtml",
                BoldText = "ESDC Sub",
                Acronym = "Employment and Social Development Canada Sub Theme",
                Text = " Custom Title"
            };

            //application template title
            WebTemplateMaster.WebTemplateModel.ApplicationTitle.Text = "My Custom Title";
            WebTemplateMaster.WebTemplateModel.ApplicationTitle.Href = "http://iservice.prv/eng/index.shtml";
        }
    }
}