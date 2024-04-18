using System;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    /// <summary>
    /// Marks class as being a valid object to be passed in the CDTS "top" function/parameter.
    /// </summary>
    public interface ITop
    {
        bool TopSecMenu { get; set; }
        List<LanguageLink> LngLinks { get; set; }
    }
}
