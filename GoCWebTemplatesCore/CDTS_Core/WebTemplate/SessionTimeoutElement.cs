using System.Configuration;

namespace CDTS_Core.WebTemplate
{
    public class SessionTimeOutElement : ConfigurationElement
    {
	    [ConfigurationProperty("enabled", DefaultValue = false, IsRequired = true)]
        public bool Enabled
        {
            get
            {
                return (bool)base["enabled"];
            }
            set
            {
                base["enabled"] = value;
            }
        }

        [ConfigurationProperty("inactivity", DefaultValue = 1200000, IsRequired = false)]
        [IntegerValidator(MinValue = 1000)]
        public int Inactivity
        {
            get
            {
                return (int)base["inactivity"];
            }
            set
            {
                base["inactivity"] = value;
            }
        }

        [ConfigurationProperty("reactionTime", DefaultValue = 30000, IsRequired = false)]
        [IntegerValidator(MinValue = 1000)]
        public int ReactionTime
        {
            get
            {
                return (int)base["reactionTime"];
            }
            set
            {
                base["reactionTime"] = value;
            }
        }

        [ConfigurationProperty("sessionalive", DefaultValue = 1200000, IsRequired = false)]
        [IntegerValidator(MinValue = 1000)]
        public int Sessionalive
        {
            get
            {
                return (int)base["sessionalive"];
            }
            set
            {
                base["sessionalive"] = value;
            }
        }

        [ConfigurationProperty("logouturl", IsRequired = true)]
        public string Logouturl
        {
            get
            {
                return (string)base["logouturl"];
            }
            set
            {
                base["logouturl"] = value;
            }
        }

        [ConfigurationProperty("refreshCallbackUrl", IsRequired = false)]
        public string RefreshCallbackUrl
        {
            get
            {
                return (string)base["refreshCallbackUrl"];
            }
            set
            {
                base["refreshCallbackUrl"] = value;
            }
        }

        [ConfigurationProperty("refreshOnClick", IsRequired = false)]
        public bool RefreshOnClick
        {
            get
            {
                return (bool)base["refreshOnClick"];
            }
            set
            {
                base["refreshOnClick"] = value;
            }
        }

        [ConfigurationProperty("refreshLimit", IsRequired = false)]
        public int RefreshLimit
        {
            get
            {
                return (int)base["refreshLimit"];
            }
            set
            {
                base["refreshLimit"] = value;
            }
        }

        [ConfigurationProperty("method", IsRequired = false)]
        public string Method
        {
            get
            {
                return (string)base["method"];
            }
            set
            {
                base["method"] = value;
            }
        }

        [ConfigurationProperty("additionalData", IsRequired = false)]
        public string AdditionalData
        {
            get
            {
                return (string)base["additionalData"];
            }
            set
            {
                base["additionalData"] = value;
            }
        }
    }
}