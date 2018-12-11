using System;
using System.Linq;
using CDTS_Core.WebTemplateCore.JsonSerializationObjects;
using Newtonsoft.Json;

namespace CDTS_Core.WebTemplateCore.CustomJsonConverters
{
    internal class ShareListConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ShareList shareList = value as ShareList;
            if (shareList.Show)
            {
                if (shareList.Enums.Any())
                {
                    serializer.Serialize(writer, (object)(from site in shareList.Enums
                        select site.ToString()));
                }
                else
                {
                    serializer.Serialize(writer, (object)true);
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
