using System.Collections.Generic;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
    /// <summary>
    /// This version is closer to the final version
    /// </summary>
    internal class AppTop
    {
        /// <summary>
        /// This is an array but it should only have one item in it. 
        /// </summary>
        public List<Link> AppName { get; set; }
        public string MenuPath { get; set; }
        public string CdnEnv { get; set; }
        public string CustomSearch { get; set; }
        public List<LanguageLink> LngLinks { get; set; }
        public List<Link> AppSettings { get; set; }
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
        public List<MenuLink> MenuLinks { get; set; }
    }
}