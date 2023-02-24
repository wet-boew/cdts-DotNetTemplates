using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoC.WebTemplate.Components.Entities
{
    public class SetupBase
    {
        public string SubTheme { get; set; }
        public string JqueryEnv { get; set; }
        public ExitSecureSite ExitSecureSite { get; set; }
        public List<WebAnalytics> WebAnalytics { get; set; }
    }
}
