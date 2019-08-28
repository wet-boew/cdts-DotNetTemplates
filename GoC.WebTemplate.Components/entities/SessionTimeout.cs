using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.Entities
{
    public class SessionTimeout
    {
        public SessionTimeout() { }

        public SessionTimeout(bool enabled,
                             int inactivity,
                             int reactionTime,
                             int sessionAlive,
                             string logoutUrl,
                             string refreshCallBackUrl,
                             bool refreshOnClick,
                             int refreshLimit,
                             string method,
                             string additionalData)
        {
            Enabled = enabled;
            Inactivity = inactivity;
            ReactionTime = reactionTime;
            SessionAlive = sessionAlive;
            LogoutUrl = logoutUrl;            
            RefreshCallBackUrl = refreshCallBackUrl;
            RefreshOnClick = refreshOnClick;
            RefreshLimit = refreshLimit;
            Method = method;
            AdditionalData = additionalData;
        }

        public bool Enabled { get; set; }
        public int Inactivity { get; set; }
        public int ReactionTime { get; set; }

        [JsonProperty(PropertyName = "sessionalive")]
        public int SessionAlive { get; set; }

        [JsonProperty(PropertyName = "logouturl")]
        public string LogoutUrl { get; set; }

        [JsonProperty(PropertyName = "refreshCallbackUrl")]
        public string RefreshCallBackUrl { get; set; }

        public bool RefreshOnClick { get; set; }
        public int RefreshLimit { get; set; }
        public string Method { get; set; }
        public string AdditionalData { get; set; }
    }
}
