using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoC.WebTemplate.Components.Entities
{
    public class ExitSecureSite
    {
        public bool ExitScript { get; set; }
        public bool DisplayModal { get; set; }
        public string MsgBoxHeader { get; set; }
        public string ExitURL { get; set; }
        public string ExitMsg { get; set; }
        public string CancelMsg { get; set; }
        public string YesMsg { get; set; }
        public string ExitDomains { get; set; }
        public string TargetWarning { get; set; }
        public bool? DisplayModalForNewWindow { get; set; }
    }
}
