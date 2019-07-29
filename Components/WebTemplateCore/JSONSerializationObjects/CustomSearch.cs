using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoC.WebTemplate.Components.JSONSerializationObjects
{
    public class CustomSearch
    {
        /// <summary>
        /// If customSearch is set a path to the action attribute needs to be given.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// This controls the text for the search label, placeholder and hidden heading. If not set, the text "search" will be used for all the text.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Optional. Used to replace the default id for the search input field, also used in the `for` attribute for the search label. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// "get" or "post"
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Optional. Used to create hidden form inputs.
        /// </summary>
        [JsonConverter(typeof(HiddenInputConverter))]
        public List<KeyValuePair<string, string>> HiddenInput { get; set; }

        internal class HiddenInputConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(List<KeyValuePair<string, string>>);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var inputs = (List<KeyValuePair<string, string>>)value;
                var newInputs = new List<Dictionary<string, string>>();
                foreach (var input in inputs)
                {
                    newInputs.Add(new Dictionary<string, string>
                    {
                        { "name", input.Key },
                        { "value", input.Value }
                    });
                }
                if (serializer != null)
                serializer.Serialize(writer, newInputs);
            }
        }
    }
}
