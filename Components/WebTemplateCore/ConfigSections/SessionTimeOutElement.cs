using System.Configuration;

namespace GoC.WebTemplate.Components.ConfigSections
{
    public class SessionTimeOutElement : ConfigurationElement
    {
        /// <summary>
        /// enabled
        /// </summary>
        [ConfigurationProperty("enabled", DefaultValue = false, IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }
        /// <summary>
        /// inactivity period of time after which the modal dialog will appear (default 20 minutes).
        /// </summary>
        /// <remarks>time provided in milliseconds</remarks>
        [ConfigurationProperty("inactivity", DefaultValue = 1200000, IsRequired = false)]
        [IntegerValidator(MinValue = 1000)]

        public int Inactivity
        {
            get { return (int)this["inactivity"]; }
            set { this["inactivity"] = value; }
        }

        /// <summary>
        /// period of time the user has to perform an action once the modal dialog is displayed (default 3 minutes).
        /// </summary>
        /// <remarks>time provided in milliseconds</remarks>
        [ConfigurationProperty("reactionTime", DefaultValue = 30000, IsRequired = false)]
        [IntegerValidator(MinValue = 1000)]
        public int ReactionTime
        {
            get { return (int)this["reactionTime"]; }
            set { this["reactionTime"] = value; }
        }

        /// <summary>
        /// period of time for the session to stay alive until the modal dialog appears (default 20 minutes).
        /// </summary>
        /// <remarks>time provided in milliseconds</remarks>
        [ConfigurationProperty("sessionalive", DefaultValue = 1200000, IsRequired = false)]
        [IntegerValidator(MinValue = 1000)]
        public int Sessionalive
        {
            get { return (int)this["sessionalive"]; }
            set { this["sessionalive"] = value; }
        }

        /// <summary>
        /// URL that users are sent to when the session has expired.
        /// </summary>
        [ConfigurationProperty("logouturl", IsRequired = true)]
        public string Logouturl
        {
            get { return (string)this["logouturl"]; }
            set { this["logouturl"] = value; }
        }

        /// <summary>
        /// URL used to perform an ajax request to determine the validity of the session.
        /// </summary>
        [ConfigurationProperty("refreshCallbackUrl", IsRequired = false)]
        public string RefreshCallbackUrl
        {
            get { return (string)this["refreshCallbackUrl"]; }
            set { this["refreshCallbackUrl"] = value; }
        }

        /// <summary>
        /// Determines if clicking on the document should reset the inactivity timeout and perform an ajax request (if a refreshCallbackUrl has been specified).
        /// </summary>
        [ConfigurationProperty("refreshOnClick", IsRequired = false)]
        public bool RefreshOnClick
        {
            get { return (bool)this["refreshOnClick"]; }
            set { this["refreshOnClick"] = value; }
        }

        /// <summary>
        /// Sets the amount of time that must pass before an ajax request can be made.
        /// </summary>
        [ConfigurationProperty("refreshLimit", IsRequired = false)]
        public int RefreshLimit
        {
            get { return (int)this["refreshLimit"]; }
            set { this["refreshLimit"] = value; }
        }

        /// <summary>
        /// Sets the request method used for ajax requests. Recommended: GET or POST.
        /// </summary>
        [ConfigurationProperty("method", IsRequired = false)]
        public string Method
        {
            get { return (string)this["method"]; }
            set { this["method"] = value; }
        }

        /// <summary>
        /// Additional data to send with the request.
        /// </summary>
        [ConfigurationProperty("additionalData", IsRequired = false)]
        public string AdditionalData
        {
            get { return (string)this["additionalData"]; }
            set { this["additionalData"] = value; }
        }

    }
}