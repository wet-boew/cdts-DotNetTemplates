using System;
using System.Diagnostics;
using Newtonsoft.Json;
using GoC.WebTemplate.Components.Entities;

namespace GoC.WebTemplate.Components.Utils
{
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
}