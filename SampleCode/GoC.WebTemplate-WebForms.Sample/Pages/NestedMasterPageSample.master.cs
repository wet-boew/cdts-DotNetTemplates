using System;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class NestedMasterPageSample : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Set the goc web template date modified here. this would set the date for all pages of my site that inherit from the nested master page
            // ReSharper disable once PossibleNullReferenceException - Fail fast and hard to catch problems, this should always work
            ((WebTemplateMasterPage)Master).WebTemplateCore.DateModified = DateTime.Now;

        }
    }
}