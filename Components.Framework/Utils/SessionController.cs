using GoC.WebTemplate.Components.Utils;
using System.Web.SessionState;

namespace GoC.WebTemplate.Components.Framework.Utils
{

    public class SessionController : ISessionController
    {
        private readonly HttpSessionState _session;

        public SessionController(HttpSessionState session)
        {
            _session = session;
        }

        public object GetSession(string key)
        {
            return _session[key];
        }

        public bool SessionExists()
        {
            return _session != null;
        }

        public void SetSession(string key, string value)
        {
            _session[key] = value;
        }
    }
}
