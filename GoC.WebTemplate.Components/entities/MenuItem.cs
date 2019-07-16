using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Entities
{
    public class MenuItem : Link
    {
        public bool OpenInNewWindow { get; set; }
        public List<MenuItem> SubItems { get; set; } = new List<MenuItem>();
    }
}
