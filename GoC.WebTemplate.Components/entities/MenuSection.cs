using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Entities
{
    public class MenuSection
    {
        public string Text { get; set; }
        public string Href { get; set; }
        public bool NewWindow { get; set; }
        public List<Link> Items { get; set; } = new List<Link>();
    }
}