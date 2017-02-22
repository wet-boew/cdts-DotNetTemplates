using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoC.WebTemplate;

namespace SampleCode.C3.Samples
{
    public partial class NestedMasterPageSample : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Set the goc web template date modified here. this would set the date for all pages of my site that inherit from the nested master page
            ((GoC.WebTemplate.WebTemplateMasterPage)this.Master).WebTemplateCore.DateModified = DateTime.Now;

        }
    }
}