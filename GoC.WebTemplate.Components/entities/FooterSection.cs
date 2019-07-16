using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class FooterSection : IFooterSection
    {
        public string SectionName { get; set; }
        public List<Link> CustomFooterLinks { get; set; } = new List<Link>();
    }
}
