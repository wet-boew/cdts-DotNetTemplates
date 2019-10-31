using System;
using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;

namespace GoC.WebTemplate.WebForms
{
    public partial class TestApplicationLeftMenuPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var leftMenu = new MenuSection
            {
                Text = "Section A",
                Href = "http://www.servicecanada.gc.ca",
                NewWindow = true
            };

            //set the header for this section of the menu
            //set the links for this section of the menu
            leftMenu.Items.Add(new MenuItem
            {
                Href = "http://www.tsn.ca",
                Text = "TSN",
                SubItems = new List<MenuItem> {
                    new MenuItem{ Href = "http://www.cbc.ca", Text = "sub 1", NewWindow = true },
                    new MenuItem{ Href = "http://www.rds.ca", Text = "sub 2" }
                }
            });
            leftMenu.Items.Add(new MenuItem { Href = "http://www.cnn.ca", Text = "CNN" });

            //add title 
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "My Fancy Application Title";
            WebTemplateMaster.WebTemplateCore.ApplicationTitle.Href = "TestApplicationTemplatePage.aspx";

            //add section to template
            WebTemplateMaster.WebTemplateCore.LeftMenuItems.Add(leftMenu);

            //or can be done with a 1 liner
            WebTemplateMaster.WebTemplateCore.LeftMenuItems.Add(new MenuSection
            {
                Text = "Section B",
                Items = new List<Link> {
                    new MenuItem { Href = "http://www.rds.ca", Text = "RDS" },
                    new MenuItem { Href = "http://www.lapresse.com", Text = "La Presse" }
                }
            });
        }
    }
}
