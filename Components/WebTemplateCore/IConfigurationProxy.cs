using System.Collections.Generic;
using System.Linq;
using GoC.WebTemplate.ConfigSections;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{

    public class ConfigurationProxy : IConfigurationProxy
    {


        public ISessionTimeOutElementProxy SessionTimeOut =>  new SessionTimeOutElementProxy(Configurations.Settings.SessionTimeOut);

        public IList<CDTSEnvironmentElementProxy> CDTSEnvironments
            => Configurations.Settings.CDTSEnvironments.Select(env => new CDTSEnvironmentElementProxy(env)).ToList();

        public ILeavingSecureSiteWarningElementProxy LeavingSecureSiteWarning
            => new LeaveSecureSiteWarningElementProxy(Configurations.Settings.leavingSecureSiteWarning);
        public string Version => Configurations.Settings.Version;
        public string Theme => Configurations.Settings.Theme;
        public string SubTheme => Configurations.Settings.SubTheme;
        public string Environment => Configurations.Settings.Environment;
        public bool UseHTTPS => Configurations.Settings.useHTTPS;
        public bool LoadJQueryFromGoogle => Configurations.Settings.LoadJQueryFromGoogle;
        public bool ShowPreContent => Configurations.Settings.ShowPreContent;
        public bool ShowPostContent => Configurations.Settings.ShowPostContent;
        public bool ShowFeedbackLink => Configurations.Settings.ShowFeedbackLink;
        public string FeedbackLinkurl => Configurations.Settings.FeedbackLinkurl;
        public bool ShowShearch => Configurations.Settings.ShowShearch;
        public bool ShowSharePageLink => Configurations.Settings.ShowSharePageLink;
        public bool ShowLanguageLink => Configurations.Settings.ShowLanguageLink;
        public bool ShowFeatures => Configurations.Settings.ShowFeatures;
        public string StaticFilesLocation => Configurations.Settings.StaticFilesLocation;
        public ICDTSEnvironmentElementProxy CurrentEnvironment => new CDTSEnvironmentElementProxy(Configurations.Settings.CDTSEnvironments[Configurations.Settings.Environment]);
    }


    public interface IConfigurationProxy
    {
        ISessionTimeOutElementProxy SessionTimeOut { get;  }
        IList<CDTSEnvironmentElementProxy> CDTSEnvironments { get; }
        ILeavingSecureSiteWarningElementProxy LeavingSecureSiteWarning { get;  }

        /// <summary>
        /// version
        /// </summary>
        string Version { get;  }

        /// <summary>
        /// theme
        /// </summary>
        string Theme { get;  }

        /// <summary>
        /// subTheme
        /// </summary>
        string SubTheme { get;  }

        /// <summary>
        /// cdts environment to use
        /// </summary>
        string Environment { get;  }

        /// <summary>
        /// use Https
        /// </summary>
        bool UseHTTPS { get;  }

        /// <summary>
        /// LoadJQueryFromGoogle
        /// </summary>
        bool LoadJQueryFromGoogle { get;  }

        /// <summary>
        /// ShowPreContent
        /// </summary>
        bool ShowPreContent { get;  }

        /// <summary>
        /// ShowPostContent
        /// </summary>
        bool ShowPostContent { get;  }

        /// <summary>
        /// ShowFeedbackLink
        /// </summary>
        bool ShowFeedbackLink { get;  }

        /// <summary>
        /// URL used to redirect users when they click the feedback link
        /// </summary>
        string FeedbackLinkurl { get;  }

        /// <summary>
        /// ShowSearch
        /// </summary>
        bool ShowShearch { get;  }

        /// <summary>
        /// ShowSharePageLink
        /// </summary>
        bool ShowSharePageLink { get;  }

        /// <summary>
        /// ShowLanguageLink
        /// </summary>
        bool ShowLanguageLink { get; }

        /// <summary>
        /// ShowFeatures
        /// </summary>
        bool ShowFeatures { get;  }

        /// <summary>
        /// StaticFilesLocation
        /// </summary>
        string StaticFilesLocation { get;  }

        ICDTSEnvironmentElementProxy CurrentEnvironment { get; }
    }
}