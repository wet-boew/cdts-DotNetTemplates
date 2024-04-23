using GoC.WebTemplate.Components.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoC.WebTemplate.Components.Utils
{
    public static class SectionMenuBuilder
    {
        /// <summary>
        /// Builds the "LeftMenu" object needed in rendering the CDTS setup JSON
        /// </summary>
        public static object BuildLeftMenu(List<MenuSection> leftMenuItems)
        {
            var leftMenuForSerialization = new { sections = new List<object>() };

            // capitalization on anonymous types matters here, CDTS will reject the json objects if not done right
            foreach (var menu in leftMenuItems)
            {
                var menuForSerialization = new
                {
                    sectionName = WebUtility.HtmlEncode(menu.Text),
                    sectionLink = GetStringForJson(menu.Href),
                    newWindow = menu.NewWindow,
                    menuLinks = new List<object>() //can't be null
                };

                foreach (var menuItem in menu.Items)
                {
                    var item = menuItem as MenuItem;
                    if (item == null)
                    {
                        menuForSerialization.menuLinks.Add(new
                        {
                            href = menuItem.Href,
                            text = menuItem.Text
                        });
                    }
                    else
                    {
                        var subMenuForSerialization = new
                        {
                            href = item.Href,
                            text = item.Text,
                            newWindow = item.NewWindow,
                            subLinks = item.SubItems.Any() ? new List<object>() : null
                        };

                        foreach (var subMenuItem in item.SubItems)
                        {
                            subMenuForSerialization.subLinks.Add(new
                            {
                                subhref = subMenuItem.Href,
                                subtext = subMenuItem.Text,
                                newWindow = subMenuItem.NewWindow
                            });
                        }

                        menuForSerialization.menuLinks.Add(subMenuForSerialization);
                    }
                }

                leftMenuForSerialization.sections.Add(menuForSerialization);
            }

            return leftMenuForSerialization;
        }

        internal static string GetStringForJson(string str) => string.IsNullOrWhiteSpace(str) ? null : str;
    }
}
