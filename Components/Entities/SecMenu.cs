using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoC.WebTemplate.Components.Entities
{
    public class SecMenu
    {
        public List<MenuSection> Sections { get; set; }

        public SecMenu(List<MenuSection> menuSections)
        {
            if (menuSections != null && menuSections.Count > 0)
            {
                Sections = new List<MenuSection>();
                foreach(MenuSection sec in Sections)
                {
                    this.Sections.Add(sec);
                }
            } //(else this.sections stays null)
        }
    }
}
