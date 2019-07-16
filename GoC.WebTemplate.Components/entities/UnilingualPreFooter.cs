using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.Entities
{
    public class UnilingualPreFooter 
    {
        public string CdnEnv { get; set; }

        [JsonProperty(propertyName:"pagedetails")]
        public bool PageDetails { get; set; }
    }
}