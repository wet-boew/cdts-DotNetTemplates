using System;

namespace GoC.WebTemplate.Components.Entities
{ 
    public class SessionTimeout
    {
        public SessionTimeout() { }

        public SessionTimeout(bool enabled, 
                             int inactivity,
                             int reactionTime,
                             int sessionalive,
                             string logouturl,
                             string refreshCallbackUrl,
                             bool refreshOnClick,
                             int refreshLimit,
                             string method,
                             string additionalData)
        {
            Enabled = enabled;
            Inactivity = inactivity;
            ReactionTime = reactionTime;
            Sessionalive = sessionalive;
            Logouturl = logouturl;            
            RefreshCallbackUrl = refreshCallbackUrl;
            RefreshOnClick = refreshOnClick;
            RefreshLimit = refreshLimit;
            Method = method;
            AdditionalData = additionalData;
        }

        public bool Enabled { get; set; }
        public int Inactivity { get; set; }
        public int ReactionTime { get; set; }
        public int Sessionalive { get; set; }
        public string Logouturl { get; set; }
        public string RefreshCallbackUrl { get; set; }
        public bool RefreshOnClick { get; set; }
        public int RefreshLimit { get; set; }
        public string Method { get; set; }
        public string AdditionalData { get; set; }

        /// <summary>
        /// Will check that the timeouts set are equalto or lower than the server session timeout
        /// It will override Sessionalive, Inactivity and ReactionTime if it fails the check
        /// </summary>
        /// <param name="session">current session</param>
        public void CheckWithServerSessionTimout(HttpSessionState session)
        {
            const int min = 60000; //one min in millisections
            if (Enabled && session != null && session.Timeout * min < Sessionalive)
            {
                while (session.Timeout <= 1) session.Timeout++; // one min will force the popup instantly so increase the session
                Sessionalive = session.Timeout * min;
                Inactivity = Sessionalive - min;
                ReactionTime = min;
            }
        }
    }
}
