using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class FooterSection : IFooterSection
    {
        public string SectionName { get; set; }
        public List<FooterLink> CustomFooterLinks { get; set; } = new List<FooterLink>();
    }
}
