using System.Collections.Generic;

namespace GoC.WebTemplate
{
    public class AppTop
    {
        
        public string MenuPath { get; set; }
        public string CdnEnvVar { get; set; }
        public string AppName { get; set;  }
        public List<LanguageLink> LngLinks { get; set; }
        public bool SiteMenu { get; set; }

        public bool Secure { get; set; }
        /// <summary>
        /// This is an array but it should only have one item in it. 
        /// </summary>
        public List<Link> SignIn { get; set; }
        /// <summary>
        /// This is an array but it should only have one item in it. 
        /// </summary>
        public List<Link> SignOut { get; set; }
        public bool Search { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public string SubTheme { get; set; }
        public List<Link> IntranetTitle { get; set; }
        public bool ShowPreContent { get; set; }
        public string LocalPath { get; set; }
    }
}