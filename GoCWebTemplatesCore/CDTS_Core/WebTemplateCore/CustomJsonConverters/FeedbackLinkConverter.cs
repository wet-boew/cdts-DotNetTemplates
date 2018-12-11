using System;
using CDTS_Core.WebTemplateCore.JsonSerializationObjects;
using Newtonsoft.Json;

namespace CDTS_Core.WebTemplateCore.CustomJsonConverters
{
    internal class FeedbackLinkConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            FeedbackLink feedbackLink = value as FeedbackLink;
            if (feedbackLink.Show)
            {
                if (string.IsNullOrWhiteSpace(feedbackLink.URL))
                {
                    serializer.Serialize(writer, (object)true);
                }
                else
                {
                    serializer.Serialize(writer, (object)feedbackLink.URL);
                }
            }
            else
            {
                serializer.Serialize(writer, (object)false);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}