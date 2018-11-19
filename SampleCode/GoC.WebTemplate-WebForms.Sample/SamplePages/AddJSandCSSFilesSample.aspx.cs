using System;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class AddCSSandJSFilesSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Add a CSS to the header
            WebTemplateMaster.WebTemplateCore.HTMLHeaderElements.Add("<link rel='stylesheet' type='text/css' href='mystyle.css'>");

            //Add a JS to the header
            //this.WebTemplateMaster.HTMLHeaderElements.Add("<script src='myJS.js'></script>");
            //or to add it to the body (bottom of page)
            WebTemplateMaster.WebTemplateCore.HTMLBodyElements.Add("<script src='myJS.js'></script>");
        }
    }
}
