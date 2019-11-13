using System;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class NestedMasterPageSample1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the page title
            WebTemplateMaster.WebTemplateModel.HeaderTitle = "Nested Master Page Sample";
        }
    }
}