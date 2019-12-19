using GoC.WebTemplate.Components.Utils;
using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.Entities
{
    public class WebAnalytics
    {
        /// <summary>
        /// Lists the avaliable environments that can be used with the CDTS
        /// </summary>
        /// <remarks>The enum item name must match the name expected by CDTS to work.</remarks>
        public enum EnvironmentOption
        {
            production,
            staging
        }
        /// <summary>
        /// Determins if the analytics should be used or not
        /// </summary>
        [JsonIgnore]
        public bool Active { get; set; }

        /// <summary>
        /// Use this value to state if the AA running is either 'staging' or 'production'.
        /// </summary>
        [JsonConverter(typeof(WebAnalyticsEnvironmentConverter))]
        public EnvironmentOption Environment { get; set; } = EnvironmentOption.production;

        /// <summary>
        /// Use this value to state which version of AA your application / site uses. Presently only versions 1 or 2 are available.
        /// </summary>
        public int Version { get; set; } = 2;
    }
}
