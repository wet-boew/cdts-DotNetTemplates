using System;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    /// <summary>
    /// Marks class as being a valid object to be passed in the CDTS "top" function/parameter.
    /// </summary>
#pragma warning disable CA1040 // Avoid empty interfaces    
    public interface ITop
#pragma warning restore CA1040 // Avoid empty interfaces
    {
        bool TopSecMenu { get; set; }
        List<LanguageLink> LngLinks { get; set; }
    }
}
