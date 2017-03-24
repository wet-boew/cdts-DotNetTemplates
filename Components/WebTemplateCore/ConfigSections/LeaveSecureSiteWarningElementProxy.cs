namespace GoC.WebTemplate.ConfigSections
{
    public class LeaveSecureSiteWarningElementProxy : ILeavingSecureSiteWarningElementProxy
    {
        private readonly leavingSecureSiteWarningElement _leavingSecureSiteWarningElement;

        public LeaveSecureSiteWarningElementProxy(leavingSecureSiteWarningElement leavingSecureSiteWarningElement)
        {
            _leavingSecureSiteWarningElement = leavingSecureSiteWarningElement;
        }

        public bool Enabled => _leavingSecureSiteWarningElement.Enabled;
        public bool DisplayModalWindow => _leavingSecureSiteWarningElement.DisplayModalWindow;
        public string RedirectURL => _leavingSecureSiteWarningElement.RedirectURL;
        public string ExcludedDomains => _leavingSecureSiteWarningElement.ExcludedDomains;
    }
}