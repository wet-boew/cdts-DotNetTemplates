using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebTemplateCore.JSONSerializationObjects;

namespace GoC.WebTemplate
{
    public static class JsonSerializationHelper
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Basic json serialization using the settings that work with the CDTS google closure templates
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HtmlString SerializeToJson(object obj)
        {
            return new HtmlString(JsonConvert.SerializeObject(obj, Settings));
        }

        //Because of how JSonDesrialization works we need to have a container class for the environments.
        private class EnvironmentContainer
        {
            public List<CDTSEnvironment> Environments { get; set; }
        }

        /// <summary>
        /// Deserialize the CDTSEnvironment objects, this is public incase someone wants to implement their
        /// own caching implementation
        /// </summary>
        /// <param name="filename">The filename to use, we are using CDTSEnvironments.json</param>
        /// <returns>A dictionary of environments with the ICDTSEnvironment.Name being the key.</returns>
        public static IDictionary<string, ICDTSEnvironment> DeserializeEnvironments(string filename) 
        {
            using (var file = File.OpenText(filename))
            using (var reader = new JsonTextReader(file))
            {
                var serializer = new JsonSerializer{
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };
                var environments = serializer.Deserialize<EnvironmentContainer>(reader);
                return environments.Environments.Cast<ICDTSEnvironment>().ToDictionary(x => x.Name, x => x);

            }
        }

    }
}