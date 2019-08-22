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

        /// <summary>
        /// Will check that the timeouts set are equalto or lower than the server session timeout
        /// It will override SessionAlive, Inactivity and ReactionTime if it fails the check
        /// </summary>
        /// <param name="session">current session</param>
        public void CheckWithServerSessionTimout(ISession session)
        {
            const int min = 60000; //one min in millisections
            if (Enabled && session != null && session.IsAvailable &&
                session.TryGetValue("timeout", out byte[] temp) &&
                int.TryParse(temp.ToString(), out int timeout) &&
                timeout * min < SessionAlive)
            {
                while (timeout <= 1) timeout++; // one min will force the popup instantly so increase the session
                SessionAlive = timeout * min;
                Inactivity = SessionAlive - min;
                ReactionTime = min;

            }
        }
    }
}
