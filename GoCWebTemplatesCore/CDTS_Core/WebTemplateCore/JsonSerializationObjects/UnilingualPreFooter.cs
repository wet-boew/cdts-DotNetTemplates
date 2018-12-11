using Newtonsoft.Json;

namespace CDTS_Core.WebTemplateCore.JsonSerializationObjects
{
    public class UnilingualPreFooter
    {
        public string CdnEnv
        {
            get;
            set;
        }

        [JsonProperty("pagedetails")]
        public bool PageDetails
        {
            get;
            set;
        }
    }

}
