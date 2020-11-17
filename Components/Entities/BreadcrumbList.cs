using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class BreadcrumbList
    {
        public bool Show { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
    }
}
