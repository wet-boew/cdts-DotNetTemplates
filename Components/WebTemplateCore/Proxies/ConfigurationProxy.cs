using System.Collections.Generic;
using System.Linq;
using GoC.WebTemplate.ConfigSections;

// ReSharper disable once CheckNamespace

namespace GoC.WebTemplate.Proxies
{

    public class ConfigurationProxy : IConfigurationProxy
    {
        public string CustomSearch => Configurations.Settings.CustomSearch;
        public string CustomSiteMenuURL => Configurations.Settings.CustomSiteMenuURL;
        public bool ShowGlobalNav => Configurations.Settings.ShowGlobalNav;
        public bool ShowSiteMenu => Configurations.Settings.ShowSiteMenu;
        public string SignOutLinkURL => Configurations.Settings.SignOutLinkURL;
        public string SignInLinkURL => Configurations.Settings.SignInLinkURL;

        public IList<CDTSEnvironmentElementProxy> CDTSEnvironments
            => Configurations.Settings.CDTSEnvironments.Select(env => new CDTSEnvironmentElementProxy(env)).ToList();

        public ICDTSEnvironmentElementProxy CurrentEnvironment
            =>
                new CDTSEnvironmentElementProxy(
                    Configurations.Settings.CDTSEnvironments[Configurations.Settings.Environment]);

        public ILeavingSecureSiteWarningElementProxy LeavingSecureSiteWarning
            => new LeaveSecureSiteWarningElementProxy(Configurations.Settings.LeavingSecureSiteWarning);

        public string Environment => Configurations.Settings.Environment;
        public bool UseHTTPS => Configurations.Settings.useHTTPS;
        public bool LoadJQueryFromGoogle => Configurations.Settings.LoadJQueryFromGoogle;

        public ISessionTimeOutElementProxy SessionTimeOut
            => new SessionTimeOutElementProxy(Configurations.Settings.SessionTimeOut);

        public bool ShowFeedbackLink => Configurations.Settings.ShowFeedbackLink;
        public bool ShowPreContent => Configurations.Settings.ShowPreContent;
        public bool ShowPostContent => Configurations.Settings.ShowPostContent;
        public string FeedbackLinkurl => Configurations.Settings.FeedbackLinkurl;
        public bool ShowShearch => Configurations.Settings.ShowShearch;
        public bool ShowSharePageLink => Configurations.Settings.ShowSharePageLink;
        public bool ShowLanguageLink => Configurations.Settings.ShowLanguageLink;
        public bool ShowFeatures => Configurations.Settings.ShowFeatures;
        public string StaticFilesLocation => Configurations.Settings.StaticFilesLocation;
        public string SubTheme => Configurations.Settings.SubTheme;
        public string Theme => Configurations.Settings.Theme;
        public string Version => Configurations.Settings.Version;
    }
}