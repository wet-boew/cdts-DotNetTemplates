using System.Collections.Generic;

namespace CDTS_Core.WebTemplateCore.JsonSerializationObjects
{
    internal class AppTop
    {
        public List<Link> AppName
        {
            get;
            set;
        }

        public string MenuPath
        {
            get;
            set;
        }

        public string CdnEnv
        {
            get;
            set;
        }

        public string CustomSearch
        {
            get;
            set;
        }

        public List<LanguageLink> LngLinks
        {
            get;
            set;
        }

        public List<Link> AppSettings
        {
            get;
            set;
        }

        public List<Link> SignIn
        {
            get;
            set;
        }

        public List<Link> SignOut
        {
            get;
            set;
        }

        public bool Search
        {
            get;
            set;
        }

        public List<Breadcrumb> Breadcrumbs
        {
            get;
            set;
        }

        public string SubTheme
        {
            get;
            set;
        }

        public bool ShowPreContent
        {
            get;
            set;
        }

        public string LocalPath
        {
            get;
            set;
        }

        public bool TopSecMenu
        {
            get;
            set;
        }

        public List<MenuLink> MenuLinks
        {
            get;
            set;
        }
    }

}
