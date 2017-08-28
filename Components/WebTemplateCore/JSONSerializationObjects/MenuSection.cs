using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{
    [Serializable]
    public class MenuSection
    {
        public MenuSection()
        {
            Name = string.Empty;
            Items = new List<Link>();
        }

        public MenuSection(string sectionName, Link[] menuLinks)
        {
            Name = sectionName;
            Link = string.Empty;
            Items = new List<Link>(menuLinks);
        }

        public MenuSection(string sectionName, MenuItem[] menuLinks)
        {
            Name = sectionName;
            Link = string.Empty;
            Items = new List<Link>(menuLinks);
        }

        public MenuSection(string sectionName, string sectionLink, Link[] menuLinks)
        {
            Name = sectionName;
            Link = sectionLink;
            Items = new List<Link>(menuLinks);            
        }

        public MenuSection(string sectionName, string sectionLink, MenuItem[] menuLinks)
        {
            Name = sectionName;
            Link = sectionLink;
            Items = new List<Link>(menuLinks);
        }

        public string Name { get; set; }

        public string Link { get; set; }

        public bool OpenInNewWindow { get; set; }

        public List<Link> Items { get; set; }
    }
}