using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class MenuLink : Link
    {
        public List<SubLink> SubLinks { get; set; }
    }
}