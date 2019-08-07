namespace GoC.WebTemplate.Components.Configs
{

    public class ConfigurationProxy : IConfigurationProxy
    {
        public string CustomSiteMenuURL => Configurations.Settings.CustomSiteMenuURL;
        public string SignOutLinkURL => Configurations.Settings.SignOutLinkURL;
        public string SignInLinkURL => Configurations.Settings.SignInLinkURL;

        public ILeaveSecureSiteWarningElementProxy LeavingSecureSiteWarning
            => new LeaveSecureSiteWarningElementProxy(Configurations.Settings.LeavingSecureSiteWarning);

        //Convert to upper case to work with the enums
        public string Environment => Configurations.Settings.Environment.ToUpperInvariant();
        public bool LoadJQueryFromGoogle => Configurations.Settings.LoadJQueryFromGoogle;

        public ISessionTimeOutElementProxy SessionTimeOut
            => new SessionTimeOutElementProxy(Configurations.Settings.SessionTimeOut);

        public bool ShowFeedbackLink => Configurations.Settings.ShowFeedbackLink;
        public bool ShowPreContent => Configurations.Settings.ShowPreContent;
        public bool ShowPostContent => Configurations.Settings.ShowPostContent;
        public string FeedbackLinkUrl => Configurations.Settings.FeedbackLinkUrl;
        public string FeedbackLinkUrlFr => Configurations.Settings.FeedbackLinkUrlFr;
        public bool ShowSearch => Configurations.Settings.ShowSearch;
        public bool ShowSharePageLink => Configurations.Settings.ShowSharePageLink;
        public bool ShowLanguageLink => Configurations.Settings.ShowLanguageLink;
        public string StaticFilesLocation => Configurations.Settings.StaticFilesLocation;
        public string Theme => Configurations.Settings.Theme;
        public string Version => Configurations.Settings.Version;
        public bool? UseHttps => Configurations.Settings.UseHttps;
    }
}