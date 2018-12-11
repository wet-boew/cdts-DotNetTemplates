using CDTS_Core.WebTemplateCore.CustomJsonConverters;
using Newtonsoft.Json;

namespace CDTS_Core.WebTemplateCore.JsonSerializationObjects
{
    public class PreFooter
    {
        public string CdnEnv
        {
            get;
            set;
        }

        public string VersionIdentifier
        {
            get;
            set;
        }

        public string DateModified
        {
            get;
            set;
        }

        public bool ShowPostContent
        {
            get;
            set;
        }

        [JsonConverter(typeof(FeedbackLinkConverter))]
        public FeedbackLink ShowFeedback
        {
            get;
            set;
        }

        [JsonConverter(typeof(ShareListConverter))]
        public ShareList ShowShare
        {
            get;
            set;
        }

        public string ScreenIdentifier
        {
            get;
            set;
        }
    }

}
