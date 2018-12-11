namespace CDTS_Core.WebTemplateCore.Proxies
{
    public interface ISessionTimeOutElementProxy
    {
        bool Enabled
        {
            get;
        }

        int Inactivity
        {
            get;
        }

        int ReactionTime
        {
            get;
        }

        int Sessionalive
        {
            get;
        }

        string Logouturl
        {
            get;
        }

        string RefreshCallbackUrl
        {
            get;
        }

        bool RefreshOnClick
        {
            get;
        }

        int RefreshLimit
        {
            get;
        }

        string Method
        {
            get;
        }

        string AdditionalData
        {
            get;
        }
    }

}
