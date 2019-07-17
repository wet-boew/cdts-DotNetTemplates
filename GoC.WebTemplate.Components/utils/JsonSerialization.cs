using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using GoC.WebTemplate.Components.Configs;
using Microsoft.AspNetCore.Html;

namespace GoC.WebTemplate.Components.Utils
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
            public List<CdtsEnvironment> Environments { get; set; }
        }

        /// <summary>
        /// Deserialize the CDTSEnvironment objects, this is public incase someone wants to implement their
        /// own caching implementation
        /// </summary>
        /// <returns>A dictionary of environments with the ICDTSEnvironment.Name being the key.</returns>
        public static IDictionary<string, ICdtsEnvironment> DeserializeEnvironments()
        {
            const string resouceName = @"GoC.WebTemplate.Components.CDTSEnvironments.json";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resouceName))
            {
                if (stream == null)
                    throw new MissingManifestResourceException($"Can not fine resource {resouceName}.");

                using (var reader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var serializer = new JsonSerializer
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    var environments = serializer.Deserialize<EnvironmentContainer>(jsonReader);
                    return environments.Environments.Cast<ICdtsEnvironment>().ToDictionary(x => x.Name, x => x);
                }
            }
        }
    }
}