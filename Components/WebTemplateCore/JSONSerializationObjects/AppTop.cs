using System.Collections.Generic;
using GoC.WebTemplate;

namespace WebTemplateCore.JSONSerializationObjects
{
    /// <summary>
    /// Used to serialize to JSON for the application template in the wet.builder.appTop call
    /// </summary>
    internal class AppTop
    {
        public List<Link> IntranetTitle { get; set; }
        public string AppUrl { get; set; }
        public string MenuPath { get; set; }
        public string CdnEnv { get; set; }
        public string AppName { get; set;  }
        public string CustomSearch { get; set; }
        public List<LanguageLink> LngLinks { get; set; }
        public bool SiteMenu { get; set; }
        public List<Link> AppSettings { get; set; }

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
        public bool ShowPreContent { get; set; }
        public string LocalPath { get; set; }
        public bool TopSecMenu { get; set; }
    }
}