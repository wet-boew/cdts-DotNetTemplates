using System.Collections.Generic;
using GoC.WebTemplate;

namespace WebTemplateCore.JSONSerializationObjects
{
  public class MenuLink : Link
  {
    public List<SubLink> SubLinks { get; set; }
    public bool NewWindow { get; set; }

        
    public bool ShouldSerializeNewWindow()
    {
      return NewWindow;
    }
  }
}