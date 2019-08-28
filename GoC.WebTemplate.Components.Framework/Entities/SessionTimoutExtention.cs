using System.Web.SessionState;

namespace GoC.WebTemplate.Components.Entities
{
    public static class SessionTimoutExtention
    {
        /// <summary>
        /// Will check that the timeouts set are equalto or lower than the server session timeout
        /// It will override SessionAlive, Inactivity and ReactionTime if it fails the check
        /// </summary>
        /// <param name="session">current session</param>
        public static void CheckWithServerSessionTimeout(this SessionTimeout timeoutSettings, HttpSessionState session)
        {
            const int min = 60000; //one min in millisections
            if (timeoutSettings.Enabled && session != null && session.Timeout * min < timeoutSettings.SessionAlive)
            {
                while (session.Timeout <= 1) session.Timeout++; // one min will force the popup instantly so increase the session
                timeoutSettings.SessionAlive = session.Timeout * min;
                timeoutSettings.Inactivity = timeoutSettings.SessionAlive - min;
                timeoutSettings.ReactionTime = min;
            }
        }

    }
}
