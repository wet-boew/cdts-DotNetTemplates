using GoC.WebTemplate.Components.JSONSerializationObjects.CustomJsonConverters;
using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
    public class PreFooter
    {
        public string CdnEnv { get; set; }
        public string VersionIdentifier { get; set; }
        public string DateModified { get; set; }
        public bool ShowPostContent { get; set; }

        //We have to use custom serializers because showFeedback can be both bool and a string.. 
        [JsonConverter(typeof(FeedbackLinkConverter))]
        public FeedbackLink ShowFeedback { get; set; }

        //We have to use custom serializers because showShare can be both bool and a list of strings.. 
        [JsonConverter(typeof(ShareListConverter))]
        public ShareList ShowShare { get; set; }

        public string ScreenIdentifier { get; set; }
       
    }
}