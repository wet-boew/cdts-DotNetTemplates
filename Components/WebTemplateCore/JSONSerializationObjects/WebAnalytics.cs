using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
    public class WebAnalytics
    {
        /// <summary>
        /// Determins if the analytics should be used or not
        /// </summary>
        [JsonIgnore]
        public bool Active { get; set; }

        /// <summary>
        /// Use this value to state if the AA running is either 'staging' or 'production'.
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Use this value to state which version of AA your application / site uses. Presently only versions 1 or 2 are available.
        /// </summary>
        public int Version { get; set; }
    }
}
