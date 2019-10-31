using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class MenuItem : Link
    {
        public List<Link> SubItems { get; set; } = new List<Link>();
    }
}
