using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Entities
{
    public class MenuItem : Link
    {
        public List<MenuItem> SubItems { get; set; } = new List<MenuItem>();
    }
}
