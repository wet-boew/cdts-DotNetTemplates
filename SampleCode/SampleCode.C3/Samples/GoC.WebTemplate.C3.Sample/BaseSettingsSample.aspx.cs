using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using GoC.WebTemplate;

public partial class BaseSettingsSample : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //specify a title for this page
        this.WebTemplateMaster.WebTemplateCore.HeaderTitle = "My Title";

        //specify the metatags
        this.WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta charset='UTF-8'>");
        this.WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta name='singer' content='Elvis'>");
        this.WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<meta http-equiv='default-style' content='sample'>");

        //specify the date modified
        this.WebTemplateMaster.WebTemplateCore.DateModified = Convert.ToDateTime("2016-08-28", CultureInfo.CurrentCulture);
        //or for using the current date
        //this.WebTemplateMaster.WebTemplateCore.DateModified = DateTime.Now.Date;

        //specify the version identifier (Note: since the date modified is supplied the date takes precedence)
        //this.WebTemplateMaster.WebTemplateCore.VersionIdentifier = "AA927823737.00.99";

        //specify a screen identifier
        this.WebTemplateMaster.WebTemplateCore.ScreenIdentifier = "SP-3485-01";

    }
}
