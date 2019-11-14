using System;
using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;

namespace GoC.WebTemplate.WebForms
{
  
  public class TestApplicationTemplatePage : BasePage
  {
    
    protected void Page_Load(object sender, EventArgs e)
    {

      if (WebTemplateMaster.WebTemplateModel.Settings.Environment == "AKAMAI")
      {
          Link link = new Link() { Href = "foo", Text = string.Empty };
          WebTemplateMaster.WebTemplateModel.ContactLinks.Add(link);
      }

      WebTemplateMaster.WebTemplateModel.ApplicationTitle.Href = "http://tempuri.com";
      WebTemplateMaster.WebTemplateModel.ApplicationTitle.Text = "Test page";
      WebTemplateMaster.WebTemplateModel.MenuLinks = new List<MenuLink>
      {
        
        new MenuLink {
          Href = "Foo", Text = "Item 1"
        },
        new MenuLink { 
          Text = "Item 2", SubLinks = new List<SubLink> {
            new SubLink {
               Href = "Foo", Text = "Sub Link 1"
            }, 
            new SubLink {
              Text = "Sub Link 2"
            }
          }
        } 
        
      };
      
    }
    
  }
}