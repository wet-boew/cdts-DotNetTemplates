using System;

namespace CDTS_Core.WebTemplateCore
{
    [Serializable]
    public class Link
    {
        public string Href
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public string Acronym
        {
            get;
            set;
        }

        public Link()
        {
        }

        public Link(string href, string text, string acronym = null)
        {
            Href = href;
            Text = text;
            Acronym = acronym;
        }
    }

}
