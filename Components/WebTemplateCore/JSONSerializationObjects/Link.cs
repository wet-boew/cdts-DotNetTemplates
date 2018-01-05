using System;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{
    [Serializable]
    public class Link
    {

        public Link() { }

        public Link(string href, string text, string acronym = null)
        {
            Href = href;
            Text = text;
            Acronym = acronym;
        }

        public string Href { get; set; }
        public string Text { get; set; }
        public string Acronym { get; set; }
    }
}