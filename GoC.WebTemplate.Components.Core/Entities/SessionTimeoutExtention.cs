using GoC.WebTemplate.Components.Configs;
using Microsoft.AspNetCore.Http;

namespace GoC.WebTemplate.Components.Entities
{
    public static class SessionTimeoutExtention
    {
        /// <summary>
        /// Will check that the timeouts set are equalto or lower than the server session timeout
        /// It will override SessionAlive, Inactivity and ReactionTime if it fails the check
        /// </summary>
        /// <param name="session">current session</param>
        public static void CheckWithServerSessionTimeout(this SessionTimeout timeoutSettings, ISession session)
        {
            const int min = 60000; //one min in millisections
            if (timeoutSettings.Enabled && session != null && session.IsAvailable &&
                session.TryGetValue("timeout", out byte[] temp) &&
                int.TryParse(temp.ToString(), out int timeout) &&
                timeout * min < timeoutSettings.SessionAlive)
            {
                while (timeout <= 1) timeout++; // one min will force the popup instantly so increase the session
                timeoutSettings.SessionAlive = timeout * min;
                timeoutSettings.Inactivity = timeoutSettings.SessionAlive - min;
                timeoutSettings.ReactionTime = min;
            }
        }
    }
}
