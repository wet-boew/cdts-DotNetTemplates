using System.Collections.Generic;
using GoC.WebTemplate;

namespace WebTemplateCore.JSONSerializationObjects
{

    /// <summary>
    /// Modifications to App top for Intranet theme
    /// </summary>
    internal class GCIntranetAppTop : AppTop
    {
        public List<Link> IntranetTitle { get; set; }
    }
}