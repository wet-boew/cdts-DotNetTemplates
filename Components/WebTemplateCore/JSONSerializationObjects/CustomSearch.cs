using System.Collections.Generic;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
    public class CustomSearch
    {
        /// <summary>
        /// If customSearch is set a path to the action attribute needs to be given.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// This controls the text for the search label, placeholder and hidden heading. If not set, the text "search" will be used for all the text.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Optional. Used to replace the default id for the search input field, also used in the `for` attribute for the search label. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// "get" or "post"
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Used to create hidden form inputs.
        /// </summary>
        public Dictionary<string, string> HiddenInput { get; set; }
    }
}
