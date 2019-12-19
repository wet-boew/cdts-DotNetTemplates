using GoC.WebTemplate.Components.Entities;
using Newtonsoft.Json;
using System;

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
}
