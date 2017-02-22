using System.Configuration;

// ReSharper disable once CheckNamespace
namespace GOC.WebTemplate.ConfigSections
{
    public class CDTSEnvironmentElement : ConfigurationElement
    {

        [ConfigurationProperty("localPath", IsRequired=false)]
        public string LocalPath
        {
            get { return base["localPath"] as string; }
            set { base["key"] = value; }

        }
        [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                return base["key"] as string;
            }
            set
            {
                base["key"] = value;
            }
        }
        [ConfigurationProperty("path", IsRequired = true)]
        public string Path
        {
            get
            {
                return base["path"] as string;
            }
            set
            {
                base["path"] = value;
            }
        }
        [ConfigurationProperty("env", IsRequired = true)]
        public string Env
        {
            get
            {
                return base["env"] as string;
            }
            set
            {
                base["env"] = value;
            }
        }
    }
}