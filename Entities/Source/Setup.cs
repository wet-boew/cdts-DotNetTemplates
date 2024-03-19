using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoC.WebTemplate.Components.Entities
{
    /// <summary>
    /// Objects of this class are meant to be serialized to a JSON object to be passed as 
    /// parameter to the 'wet.builder.setup' JavaScript function in the template pages.
    /// </summary>
    public class Setup
    {
        public string CdnEnv { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Mode Mode { get; set; }
        public SetupBase Base {get; set;}
        public ITop Top { get; set; }
        public IPreFooter PreFooter { get; set; }
        public IFooter Footer { get; set; }
        [JsonProperty("secmenu")]
        public object SecMenu { get; set; }
        public Splash Splash { get; set; }
        public List<string> OnCDTSPageFinalized { get; set; }
    }

    public enum Mode
    {
        [EnumMember(Value = "common")]
        COMMON,
        [EnumMember(Value = "app")]
        APP,
        [EnumMember(Value = "server")]
        SERVER,
        [EnumMember(Value = "splash")]
        SPLASH
    }
}

