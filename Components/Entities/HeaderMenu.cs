using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class HeaderMenu
    {
        public string Text { get; set; }
        public List<Link> Links { get; set; }
        public Link LogoutLink { get; set; }
    }
}
