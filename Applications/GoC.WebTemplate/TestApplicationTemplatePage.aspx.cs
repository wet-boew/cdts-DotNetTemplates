using System;
using System.Collections.Generic;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.Components;

namespace GoC.WebTemplate.WebForms
{
  
  public class TestApplicationTemplatePage : BasePage
  {
    
    protected void Page_Load(object sender, EventArgs e)
    {

      if (WebTemplateMaster.WebTemplateCore.Environment == "AKAMAI")
      {
          Link link = new Link() { Href = "foo", Text = string.Empty };
          WebTemplateMaster.WebTemplateCore.ContactLinks.Add(link);
      }

      WebTemplateMaster.WebTemplateCore.ApplicationTitle.Href = "http://tempuri.com";
      WebTemplateMaster.WebTemplateCore.ApplicationTitle.Text = "Test page";
      WebTemplateMaster.WebTemplateCore.MenuLinks = new List<MenuLink>
      {
        
        new MenuLink {
          Href = "Foo", Text = "Item 1"
        },
        new MenuLink { 
          Text = "Item 2", SubLinks = new List<SubLink> {
            new SubLink {
               Href = "Foo", Text = "SubLink 1"
            }, 
            new SubLink {
              Text = "SubLink 2"
            }
          }
        } 
        
      };
      
    }
    
  }
}