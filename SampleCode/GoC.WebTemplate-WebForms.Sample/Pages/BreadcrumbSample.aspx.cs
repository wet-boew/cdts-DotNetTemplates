using System;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class BreadcrumbSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Specify your breadcrumbs
            WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.canada.ca/en/index.html", "Home", ""));
            WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.esdc.gc.ca/en/jobs/opportunities/index.page", "Jobs", ""));
            WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.esdc.gc.ca/en/jobs/opportunities/youth_students.page", "Opportunities", ""));
            //Leaving the "href" parameter empty, will create the breadcrumb in text and not as a hyperlink. Useful for the last item of the breadcrumb list. 
            WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("", "FSWEP", "Federal Student Work Experience Program"));

            //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)

        }
    }
}
