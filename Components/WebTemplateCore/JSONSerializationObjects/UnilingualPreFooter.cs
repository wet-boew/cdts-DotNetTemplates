using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
    public class UnilingualPreFooter 
    {
        public string CdnEnv { get; set; }
        //CDTS has inconsistent naming for this property
        //Will be changed at a later date.
        [JsonProperty(propertyName:"pagedetails")]
        public bool PageDetails { get; set; }
    }
}