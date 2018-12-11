using System.Collections.Generic;

namespace CDTS_Core.WebTemplateCore.JsonSerializationObjects
{
    public class MenuLink : Link
    {
        public List<SubLink> SubLinks
        {
            get;
            set;
        }

        public bool NewWindow
        {
            get;
            set;
        }

        public bool ShouldSerializeNewWindow()
        {
            return NewWindow;
        }
    }
}
