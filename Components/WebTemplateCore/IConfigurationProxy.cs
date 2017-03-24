using System;
using System.Collections.Generic;
using System.Linq;
using GoC.WebTemplate.ConfigSections;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{

    public class ConfigurationProxy : IConfigurationProxy
    {
        private readonly Configurations _configurations;

        public ConfigurationProxy(Configurations configurations)
        {
            _configurations = configurations;
        }

        public ISessionTimeOutElementProxy SessionTimeOut =>  new SessionTimeOutElementProxy(_configurations.SessionTimeOut);

        public IList<CDTSEnvironmElementProxy> CDTSEnvironments
            => _configurations.CDTSEnvironments.Select(env => new CDTSEnvironmElementProxy(env)).ToList();

        public ILeavingSecureSiteWarningElementProxy LeavingSecureSiteWarning
            => new LeaveSecureSiteWarningElementProxy(_configurations.leavingSecureSiteWarning);
        public string Version => _configurations.Version;
        public string Theme => _configurations.Theme;
        public string SubTheme => _configurations.SubTheme;
        public string Environment => _configurations.Environment;
        public bool useHTTPS => _configurations.useHTTPS;
        public bool LoadJQueryFromGoogle => _configurations.LoadJQueryFromGoogle;
        public bool ShowPreContent => _configurations.ShowPreContent;
        public bool ShowPostContent => _configurations.ShowPostContent;
        public bool ShowFeedbackLink => _configurations.ShowFeedbackLink;
        public string FeedbackLinkurl => _configurations.FeedbackLinkurl;
        public bool ShowShearch => _configurations.ShowShearch;
        public bool ShowSharePageLink => _configurations.ShowSharePageLink;
        public bool ShowLanguageLink => _configurations.ShowLanguageLink;
        public bool ShowFeatures => _configurations.ShowFeatures;
        public string StaticFilesLocation => _configurations.StaticFilesLocation;
    }


    public interface IConfigurationProxy
    {
        ISessionTimeOutElementProxy SessionTimeOut { get;  }
        IList<CDTSEnvironmElementProxy> CDTSEnvironments { get; }
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
        Boolean useHTTPS { get;  }

        /// <summary>
        /// LoadJQueryFromGoogle
        /// </summary>
        Boolean LoadJQueryFromGoogle { get;  }

        /// <summary>
        /// ShowPreContent
        /// </summary>
        Boolean ShowPreContent { get;  }

        /// <summary>
        /// ShowPostContent
        /// </summary>
        Boolean ShowPostContent { get;  }

        /// <summary>
        /// ShowFeedbackLink
        /// </summary>
        Boolean ShowFeedbackLink { get;  }

        /// <summary>
        /// URL used to redirect users when they click the feedback link
        /// </summary>
        string FeedbackLinkurl { get;  }

        /// <summary>
        /// ShowSearch
        /// </summary>
        Boolean ShowShearch { get;  }

        /// <summary>
        /// ShowSharePageLink
        /// </summary>
        Boolean ShowSharePageLink { get;  }

        /// <summary>
        /// ShowLanguageLink
        /// </summary>
        Boolean ShowLanguageLink { get; }

        /// <summary>
        /// ShowFeatures
        /// </summary>
        Boolean ShowFeatures { get;  }

        /// <summary>
        /// StaticFilesLocation
        /// </summary>
        string StaticFilesLocation { get;  }
    }
}