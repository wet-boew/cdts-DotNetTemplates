using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class LeftSideMenuSample : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GoC.WebTemplate.MenuSection leftMenu = new GoC.WebTemplate.MenuSection();

        //set the header for this section of the menu
        leftMenu.Name = "Section A";
        leftMenu.Link = "http://www.servicecanada.gc.ca";
        leftMenu.OpenInNewWindow = true;
        //set the links for this section of the menu
        leftMenu.Items.Add(new GoC.WebTemplate.MenuItem("http://www.tsn.ca", "TSN", new GoC.WebTemplate.MenuItem[] { 
                                                            new GoC.WebTemplate.MenuItem("http://www.cbc.ca", "sub 1", true), 
                                                            new GoC.WebTemplate.MenuItem("http://www.rds.ca", "sub 2") }));
        leftMenu.Items.Add(new GoC.WebTemplate.MenuItem("http://www.cnn.ca", "CNN"));

        //add section to template
        this.WebTemplateMaster.WebTemplateCore.LeftMenuItems.Add(leftMenu);

        //or can be done with a 1 liner
        this.WebTemplateMaster.WebTemplateCore.LeftMenuItems.Add(new GoC.WebTemplate.MenuSection("Section B", new GoC.WebTemplate.MenuItem[] { 
                                                                                new GoC.WebTemplate.MenuItem("http://www.rds.ca", "RDS"), 
                                                                                new GoC.WebTemplate.MenuItem("http://www.lapresse.com", "La Presse") }));        
    }
}
