using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.Entities
{
    public class SubLink : Link
    {
        [JsonProperty("subhref")]
        public new string Href { get; set; }
        [JsonProperty("subtext")]
        public new string Text { get; set; }
        [JsonProperty("acronym")]
        public new string Acronym { get; set; }
    }
}