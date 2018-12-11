namespace CDTS_Core.WebTemplateCore.Proxies
{
    public interface IConfigurationProxy
    {
        string CustomSearch
        {
            get;
        }

        string SignOutLinkURL
        {
            get;
        }

        string SignInLinkURL
        {
            get;
        }

        ISessionTimeOutElementProxy SessionTimeOut
        {
            get;
        }

        ILeavingSecureSiteWarningElementProxy LeavingSecureSiteWarning
        {
            get;
        }

        string Version
        {
            get;
        }

        string Theme
        {
            get;
        }

        string Environment
        {
            get;
        }

        bool LoadJQueryFromGoogle
        {
            get;
        }

        bool ShowPreContent
        {
            get;
        }

        bool ShowPostContent
        {
            get;
        }

        bool ShowFeedbackLink
        {
            get;
        }

        string FeedbackLinkurl
        {
            get;
        }

        bool ShowShearch
        {
            get;
        }

        bool ShowSharePageLink
        {
            get;
        }

        bool ShowLanguageLink
        {
            get;
        }

        bool ShowFeatures
        {
            get;
        }

        string StaticFilesLocation
        {
            get;
        }

        bool? UseHttps
        {
            get;
        }
    }

}
