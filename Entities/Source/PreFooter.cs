using GoC.WebTemplate.Components.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace GoC.WebTemplate.Components.Entities
{
    public class PreFooter : IPreFooter
    {
        public string CdnEnv { get; set; }
        public string VersionIdentifier { get; set; }
        public string DateModified { get; set; }
        public bool ShowPostContent { get; set; }
        public Feedback ShowFeedback { get; set; }
        //We have to use custom serializers because showShare can be both bool and a list of strings.. 
        [JsonConverter(typeof(ShareListConverter))]
        public ShareList ShowShare { get; set; }
        public string ScreenIdentifier { get; set; }
        public List<Link> Contributors { get; set; }
    }
}