using System;
using System.Web.SessionState;

// ReSharper disable once CheckNamespace
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

        /// <summary>
        /// Will check that the timeouts set are equalto or lower than the server session timeout
        /// It will override SessionAlive, Inactivity and ReactionTime if it fails the check
        /// </summary>
        /// <param name="session">current session</param>
        public void CheckWithServerSessionTimout(HttpSessionState session)
        {
            const int min = 60000; //one min in millisections
            if (Enabled && session != null && session.Timeout * min < SessionAlive)
            {
                while (session.Timeout <= 1) session.Timeout++; // one min will force the popup instantly so increase the session
                SessionAlive = session.Timeout * min;
                Inactivity = SessionAlive - min;
                ReactionTime = min;
            }
        }
    }
}
