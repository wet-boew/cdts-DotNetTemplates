using System;

namespace GoC.WebTemplate.Components.Entities
{
    public class Feedback
    {
        public bool Enabled { get; set; }
        public string Text { get; set; }
        public string Href { get; set; }
        public string Theme { get; set; }
        public string Section { get; set; }
#pragma warning disable CA1056
        public string LegacyBtnUrl { get; set; }
#pragma warning restore CA1056
    }
}
