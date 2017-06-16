using System.Collections.Generic;
using GoC.WebTemplate;

namespace WebTemplateCore.JSONSerializationObjects
{
    public class ShareList
    {
        public bool Show { get; set; }
        public List<Core.SocialMediaSites> Enums { get; set; } 
    }
}