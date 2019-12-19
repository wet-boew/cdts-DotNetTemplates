using System.Collections.Generic;

namespace GoC.WebTemplate.Components.Entities
{
    internal class RefTop
    {
        public string CdnEnv { get; set; } 
        public string SubTheme { get; set; }
        public string JqueryEnv { get; set; }
        public string LocalPath { get; set; }
        public bool IsApplication { get; set; }
        public List<WebAnalytics> WebAnalytics { get; set; }
    }
}