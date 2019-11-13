using System;
using System.Globalization;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class BaseSettingsSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //specify a title for this page
            WebTemplateMaster.WebTemplateModel.HeaderTitle = "My Title";

            //specify the metatags
            WebTemplateMaster.WebTemplateModel.HTMLHeaderElements.Add("<meta charset='UTF-8'>");
            WebTemplateMaster.WebTemplateModel.HTMLHeaderElements.Add("<meta name='singer' content='Elvis'>");
            WebTemplateMaster.WebTemplateModel.HTMLHeaderElements.Add("<meta http-equiv='default-style' content='sample'>");

            //specify the date modified
            WebTemplateMaster.WebTemplateModel.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);
            //or for using the current date
            //this.WebTemplateMaster.WebTemplateModel.DateModified = DateTime.Now.Date;

            //specify the version identifier (Note: since the date modified is supplied the date takes precedence)
            //this.WebTemplateMaster.WebTemplateModel.VersionIdentifier = "AA927823737.00.99";

            //specify a screen identifier
            WebTemplateMaster.WebTemplateModel.ScreenIdentifier = "SP-3485-01";

        }
    }
}
