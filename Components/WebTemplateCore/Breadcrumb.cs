using System;

namespace GoC.WebTemplate
{
    [Serializable]
    public class Breadcrumb
    {
        public Breadcrumb() { }

        public Breadcrumb(string href, string title, string acronym)
        {
            Href = href;
            Title = title;
            Acronym = acronym;
        }

        public string Href { get; set; }
        public string Title { get; set; }
        public string Acronym { get; set; }

    }
}