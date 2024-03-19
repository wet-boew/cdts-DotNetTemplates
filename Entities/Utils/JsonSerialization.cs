using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoC.WebTemplate.Entities.Utils
{
    public static class JsonSerializationHelper
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false
                }
            },
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            StringEscapeHandling = StringEscapeHandling.EscapeHtml
        };

        /// <summary>
        /// Basic json serialization using the settings that work with the CDTS google closure templates
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeToJson(object value)
        {
            return JsonConvert.SerializeObject(value, Settings);
        }
    }
}