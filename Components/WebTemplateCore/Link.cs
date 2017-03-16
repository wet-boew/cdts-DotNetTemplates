using System;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{
    [Serializable]
    public class Link
    {

        public Link() { }

        public Link(string href, string text)
        {
            Href = href;
            Text = text;
        }

        public string Href { get; set; }
        public string Text { get; set; }
    }
}