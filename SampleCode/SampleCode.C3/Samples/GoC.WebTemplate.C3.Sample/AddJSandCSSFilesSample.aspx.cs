using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using GoC.WebTemplate;

public partial class AddCSSandJSFilesSample : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Add a CSS to the header
        this.WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<link rel='stylesheet' type='text/css' href='mystyle.css'>");

        //Add a JS to the header
        //this.WebTemplateMaster.HTMLHeaderElements.Add("<script src='myJS.js'></script>");
        //or to add it to the body (bottom of page)
        this.WebTemplateMaster.WebTemplateCore.HTMLBodyElements.Add("<script src='myJS.js'></script>");
    }
}
