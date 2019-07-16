using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace GoC.WebTemplate.Components.Utils
{
    internal class ShareListConverter : JsonConverter 
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var share = value as Entities.ShareList;
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