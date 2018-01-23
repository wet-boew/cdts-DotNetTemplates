using GoC.WebTemplate;

namespace WebTemplateCore.JSONSerializationObjects
{
    public class CDTSEnvironment : ICDTSEnvironment
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string CDN { get; set; }
        public string SubTheme { get; set; }
        public bool IsVersionRNCombined { get; set; }
        public bool IsEncryptionModifiable { get; set; }
        public string LocalPath { get; set; }
        public string Theme { get; set; }
        public string AppendToTitle { get; set; }
    }
}