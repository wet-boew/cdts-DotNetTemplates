using CDTS_Core.WebTemplate;

namespace CDTS_Core.WebTemplateCore.Proxies
{
    public class LeaveSecureSiteWarningElementProxy : ILeavingSecureSiteWarningElementProxy
    {
        private readonly LeavingSecureSiteWarningElement _leavingSecureSiteWarningElement;

        public bool Enabled => _leavingSecureSiteWarningElement.Enabled;

        public bool DisplayModalWindow => _leavingSecureSiteWarningElement.DisplayModalWindow;

        public string RedirectURL => _leavingSecureSiteWarningElement.RedirectURL;

        public string ExcludedDomains => _leavingSecureSiteWarningElement.ExcludedDomains;

        public LeaveSecureSiteWarningElementProxy(LeavingSecureSiteWarningElement leavingSecureSiteWarningElement)
        {
            _leavingSecureSiteWarningElement = leavingSecureSiteWarningElement;
        }
    }

}
