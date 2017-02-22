using System;

namespace GoC.WebTemplate
{
     [Serializable]
    public class SessionTimeout
    {
        public SessionTimeout() { }

        public SessionTimeout(bool enabled, 
                             int inactivity,
                             int reactionTime,
                             int sessionAlive,
                             string logoutUrl,
                             string refreshCallbackUrl,
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
            RefreshCallbackUrl = refreshCallbackUrl;
            RefreshOnClick = refreshOnClick;
            RefreshLimit = refreshLimit;
            Method = method;
            AdditionalData = additionalData;
        }

        public bool Enabled { get; set; }
        public int Inactivity { get; set; }
        public int ReactionTime { get; set; }
        public int SessionAlive { get; set; }
        public string LogoutUrl { get; set; }
        public string RefreshCallbackUrl { get; set; }
        public bool RefreshOnClick { get; set; }
        public int RefreshLimit { get; set; }
        public string Method { get; set; }
        public string AdditionalData { get; set; }
        
    }
}
