using GoC.WebTemplate.Components.ConfigSections;

namespace GoC.WebTemplate.Components.Proxies
{
    public class SessionTimeOutElementProxy : ISessionTimeOutElementProxy {
        private readonly SessionTimeOutElement _sessionTimeOutElement;

        public SessionTimeOutElementProxy(SessionTimeOutElement sessionTimeOutElement)
        {
            _sessionTimeOutElement = sessionTimeOutElement;
        }

        public bool Enabled => _sessionTimeOutElement.Enabled;
        public int Inactivity => _sessionTimeOutElement.Inactivity;
        public int ReactionTime => _sessionTimeOutElement.ReactionTime;
        public int Sessionalive => _sessionTimeOutElement.SessionAlive;
        public string Logouturl => _sessionTimeOutElement.LogoutUrl;
        public string RefreshCallbackUrl => _sessionTimeOutElement.RefreshCallbackUrl;
        public bool RefreshOnClick => _sessionTimeOutElement.RefreshOnClick;
        public int RefreshLimit => _sessionTimeOutElement.RefreshLimit;
        public string Method => _sessionTimeOutElement.Method;
        public string AdditionalData => _sessionTimeOutElement.AdditionalData;
    }
}