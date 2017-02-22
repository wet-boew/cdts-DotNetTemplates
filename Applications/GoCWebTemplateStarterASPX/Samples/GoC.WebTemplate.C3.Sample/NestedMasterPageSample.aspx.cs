using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoC.WebTemplate;

namespace SampleCode.C3.Samples
{
    public partial class NestedMasterPageSample1 : GoC.WebTemplate.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //set the page title
            this.WebTemplateMaster.WebTemplateCore.HeaderTitle = "Nested Master Page Sample";
        }
    }
}