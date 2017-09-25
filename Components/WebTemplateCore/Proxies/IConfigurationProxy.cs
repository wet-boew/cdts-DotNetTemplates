using System.Collections.Generic;
using GoC.WebTemplate.ConfigSections;
using WebTemplateCore.Proxies;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Proxies
{
    public interface IConfigurationProxy
    {
        string CustomSearch { get; }
        bool ShowSiteMenu { get; }
        string SignOutLinkURL { get; }
        string SignInLinkURL { get; }
        ISessionTimeOutElementProxy SessionTimeOut { get;  }
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

        bool? UseHttps { get; }
    }
}