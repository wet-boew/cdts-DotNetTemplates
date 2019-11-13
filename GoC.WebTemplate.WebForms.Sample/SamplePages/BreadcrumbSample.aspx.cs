using System;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class BreadcrumbSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Specify your breadcrumbs
            WebTemplateMaster.WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "http://www.canada.ca/en/index.html", Title = "Home" });
            WebTemplateMaster.WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "http://www.esdc.gc.ca/en/jobs/opportunities/index.page", Title = "Jobs" });
            WebTemplateMaster.WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Href = "http://www.esdc.gc.ca/en/jobs/opportunities/youth_students.page", Title = "Opportunities" });
            //Leaving the "href" parameter empty, will create the breadcrumb in text and not as a hyperlink. Useful for the last item of the breadcrumb list. 
            WebTemplateMaster.WebTemplateModel.Breadcrumbs.Add(new Breadcrumb { Title = "FSWEP", Acronym = "Federal Student Work Experience Program" });

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)

        }
    }
}
