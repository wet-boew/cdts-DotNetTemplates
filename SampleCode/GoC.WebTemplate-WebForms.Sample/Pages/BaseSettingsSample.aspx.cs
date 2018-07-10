using System;
using System.Globalization;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class BaseSettingsSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //specify a title for this page
            WebTemplateMaster.WebTemplateCore.HeaderTitle = "My Title";

            //specify the metatags
            WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta charset='UTF-8'>");
            WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta name='singer' content='Elvis'>");
            WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta http-equiv='default-style' content='sample'>");

            //specify the date modified
            WebTemplateMaster.WebTemplateCore.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);
            //or for using the current date
            //this.WebTemplateMaster.WebTemplateCore.DateModified = DateTime.Now.Date;

            //specify the version identifier (Note: since the date modified is supplied the date takes precedence)
            //this.WebTemplateMaster.WebTemplateCore.VersionIdentifier = "AA927823737.00.99";

            //specify a screen identifier
            WebTemplateMaster.WebTemplateCore.ScreenIdentifier = "SP-3485-01";

        }
    }
}
