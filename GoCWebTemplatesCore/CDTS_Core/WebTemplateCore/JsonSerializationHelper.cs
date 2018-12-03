using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CDTS_Core.WebTemplateCore.JsonSerializationObjects;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CDTS_Core.WebTemplateCore
{
    public static class JsonSerializationHelper
    {
        private class EnvironmentContainer
        {
            public List<CDTSEnvironment> Environments
            {
                get;
                set;
            }
        }

        private static readonly JsonSerializerSettings Settings;

        public static HtmlString SerializeToJson(object obj)
        {
            return new HtmlString(JsonConvert.SerializeObject(obj, Settings));
        }

        public static IDictionary<string, ICDTSEnvironment> DeserializeEnvironments(string filename)
        {
            //IL_0008: Unknown result type (might be due to invalid IL or missing references)
            //IL_000e: Expected O, but got Unknown
            //IL_000e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0013: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            //IL_001e: Expected O, but got Unknown
            //IL_001e: Unknown result type (might be due to invalid IL or missing references)
            using (StreamReader streamReader = File.OpenText(filename))
            {
                JsonTextReader val = new JsonTextReader((TextReader)streamReader);
                try
                {
                    JsonSerializer val2 = new JsonSerializer();
                    val2.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    val2.NullValueHandling = NullValueHandling.Ignore;
                    return val2.Deserialize<EnvironmentContainer>(val).Environments.Cast<ICDTSEnvironment>().ToDictionary((ICDTSEnvironment x) => x.Name, (ICDTSEnvironment x) => x);
                }
                finally
                {
                    ((IDisposable)val)?.Dispose();
                }
            }
        }

        static JsonSerializationHelper()
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_0005: Unknown result type (might be due to invalid IL or missing references)
            //IL_0006: Unknown result type (might be due to invalid IL or missing references)
            //IL_0010: Expected O, but got Unknown
            //IL_0010: Unknown result type (might be due to invalid IL or missing references)
            //IL_001c: Expected O, but got Unknown
            JsonSerializerSettings val = new JsonSerializerSettings();
            val.ContractResolver = new CamelCasePropertyNamesContractResolver();
            val.NullValueHandling = NullValueHandling.Ignore;
            Settings = val;
        }
    }

}
