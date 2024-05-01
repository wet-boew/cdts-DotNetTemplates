using System;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    public class ContextualFooter
    {
        public string Title { get; set; }
        public IEnumerable<Link> Links { get; set; }
    }
}
