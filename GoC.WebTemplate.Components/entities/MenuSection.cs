using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Entities
{
    public class MenuSection
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool OpenInNewWindow { get; set; }
        public List<Link> Items { get; set; } = new List<Link>();
    }
}