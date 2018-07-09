using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
  public class SubLink : Link
  {
        
    [JsonProperty("subhref")]
    public new string Href { get; set; }
    [JsonProperty("subtext")]
    public new string Text { get; set; }
    public bool NewWindow { get; set; }
  }
}