using System;
using System.Diagnostics;
using System.Linq;
using GoC.WebTemplate.Components.Entities;
using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.Utils
{
    internal class WebAnalyticsEnvironmentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WebAnalytics.EnvironmentOption);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (serializer == null) throw new ArgumentException("The property serializer is required.");
            var environment = (WebAnalytics.EnvironmentOption)value;
            serializer.Serialize(writer, environment.ToString());
        }
    }
    internal class ShareListConverter : JsonConverter 
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var share = value as Entities.ShareList;
            if (serializer == null) throw new ArgumentException("The property serializer is required.");
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
}