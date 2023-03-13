using GoC.WebTemplate.Components.Utils;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;

namespace GoC.WebTemplate.Components.Entities
{
    public class PreFooter : IPreFooter
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

        internal class FeedbackLinkConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var feedbackLink = value as FeedbackLink;
                //this converter should never be on a type that's not a FeedbackLink so just throw exceptions.
                Debug.Assert(feedbackLink != null, "obj != null");
                if (feedbackLink.Show)
                {
                    if (string.IsNullOrWhiteSpace(feedbackLink.Url))
                    {
                        serializer.Serialize(writer, true);
                        return;
                    }
                    serializer.Serialize(writer, Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.StartsWith(Constants.FRENCH_ACCRONYM, StringComparison.OrdinalIgnoreCase) && 
                                                 !string.IsNullOrEmpty(feedbackLink.UrlFr) ? 
                                                 feedbackLink.UrlFr : feedbackLink.Url);
                    return;
                }

                serializer.Serialize(writer, false);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                //We never deserialie so we shouldn't have to implement this.
                throw new NotImplementedException();
            }

            public override bool CanConvert(Type objectType)
            {
                //We never deserialize so this does not need to be used
                throw new NotImplementedException();
            }
        }
    }
}