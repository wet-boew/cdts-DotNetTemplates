using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace GoC.WebTemplate.Components.Utils
{
    internal class BreadcrumbListConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var breadcrumbs = value as Entities.BreadcrumbList;
            if (serializer == null) throw new ArgumentException("The property serializer is required.");
            //this converter should never be on a type that's not a BreadcrumbList so just throw exceptions.
            Debug.Assert(breadcrumbs != null, "breadcrumbs != null");
            if (breadcrumbs.Show)
            {
                serializer.Serialize(writer, breadcrumbs.Breadcrumbs);
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
