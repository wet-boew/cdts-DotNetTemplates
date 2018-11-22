using System.Collections.Generic;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
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