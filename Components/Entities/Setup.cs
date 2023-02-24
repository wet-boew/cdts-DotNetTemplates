using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoC.WebTemplate.Components.Entities
{
    /**
     * Objects of this class are meant to be serialized to a JSON object to be
     * passed as parameter to the 'wet.builder.setup' JavaScript function in the
     * template pages.
     */
    public class Setup
    {
        public string CdnEnv { get; set; }
        public Mode Mode { get; set; }
        public SetupBase Base {get; set;}
        public ITop Top { get; set; }
        public IPreFooter PreFooter { get; set; }
        public IFooter Footer { get; set; }
        public SecMenu SecMenu { get; set; }
        public Splash Splash { get; set; }
        public List<string> OnCDTSPageFinalized { get; set; }
    }
}
public enum Mode
{
    Common,
    App,
    Server,
    Splash
}
