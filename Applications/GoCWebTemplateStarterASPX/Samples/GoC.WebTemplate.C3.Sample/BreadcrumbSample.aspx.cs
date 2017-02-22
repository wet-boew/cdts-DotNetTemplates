using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoC.WebTemplate;


public partial class BreadcrumbSample : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Specify your breadcrumbs
        this.WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.canada.ca/en/index.html", "Home", ""));
        this.WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.esdc.gc.ca/en/jobs/opportunities/index.page", "Jobs", ""));
        this.WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("http://www.esdc.gc.ca/en/jobs/opportunities/youth_students.page", "Opportunities", ""));
        //Leaving the "href" parameter empty, will create the breadcrumb in text and not as a hyperlink. Useful for the last item of the breadcrumb list. 
        this.WebTemplateMaster.WebTemplateCore.Breadcrumbs.Add(new Breadcrumb("", "FSWEP", "Federal Student Work Experience Program"));

        //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)

    }
}
