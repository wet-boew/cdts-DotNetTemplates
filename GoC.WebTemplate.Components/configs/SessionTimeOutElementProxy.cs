namespace GoC.WebTemplate.Components.Configs
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
        public int SessionAlive => _sessionTimeOutElement.SessionAlive;
        public string LogoutUrl => _sessionTimeOutElement.LogoutUrl;
        public string RefreshCallBackUrl => _sessionTimeOutElement.RefreshCallBackUrl;
        public bool RefreshOnClick => _sessionTimeOutElement.RefreshOnClick;
        public int RefreshLimit => _sessionTimeOutElement.RefreshLimit;
        public string Method => _sessionTimeOutElement.Method;
        public string AdditionalData => _sessionTimeOutElement.AdditionalData;
    }
}