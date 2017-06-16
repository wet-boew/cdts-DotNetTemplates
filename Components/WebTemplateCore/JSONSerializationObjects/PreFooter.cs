using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GoC.WebTemplate;
using Newtonsoft.Json;

namespace WebTemplateCore.JSONSerializationObjects
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

    internal class ShareListConverter : JsonConverter 
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var share = value as ShareList;
            //this converter should never be on a type that's not a ShareList so just throw exceptions.
            Debug.Assert(share != null, "share != null");
            if (share.Show)
            {
                if (share.Enums.Any())
                {
                    serializer.Serialize(writer, share.Enums.Select(site => site.ToString()));
                    return;
                }
                serializer.Serialize(writer, true);
                return;

            }
            serializer.Serialize(writer, false);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //We never deserialize so this does not need to be used
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            //We never deserialize so this does not need to be used
            throw new NotImplementedException();
        }
    }

    public class ShareList
    {
        public bool Show { get; set; }
        public List<Core.SocialMediaSites> Enums { get; set; } 
    }

    internal class FeedbackLinkConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var feedbackLink =  value as FeedbackLink;
            //this converter should never be on a type that's not a FeedbackLink so just throw exceptions.
            Debug.Assert(feedbackLink != null, "obj != null");
            if (feedbackLink.Show)
            {
                if (string.IsNullOrWhiteSpace(feedbackLink.URL))
                {
                    serializer.Serialize(writer, true);
                    return;
                }
                serializer.Serialize(writer, feedbackLink.URL);
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

    public class FeedbackLink
    {
        public bool Show { get; set; }
        public string URL { get; set; }
    }
}