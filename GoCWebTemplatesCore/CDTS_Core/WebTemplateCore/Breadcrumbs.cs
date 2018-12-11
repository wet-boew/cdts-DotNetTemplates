using System;

namespace CDTS_Core.WebTemplateCore
{
    [Serializable]
    public class Breadcrumb
    {
        public string Href
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Acronym
        {
            get;
            set;
        }

        public Breadcrumb()
        {
        }

        public Breadcrumb(string href, string title, string acronym)
        {
            Href = href;
            Title = title;
            Acronym = acronym;
        }
    }


}
