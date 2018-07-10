using System;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class NestedMasterPageSample1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the page title
            WebTemplateMaster.WebTemplateCore.HeaderTitle = "Nested Master Page Sample";
        }
    }
}