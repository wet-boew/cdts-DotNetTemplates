namespace GoC.WebTemplate.Components.Configs
{
    public class LeaveSecureSiteWarningElementProxy : ILeaveSecureSiteWarningElementProxy
    {
        private readonly LeavingSecureSiteWarningElement _leavingSecureSiteWarningElement;

        public LeaveSecureSiteWarningElementProxy(LeavingSecureSiteWarningElement leavingSecureSiteWarningElement)
        {
            _leavingSecureSiteWarningElement = leavingSecureSiteWarningElement;
        }

        public bool Enabled => _leavingSecureSiteWarningElement.Enabled;
        public bool DisplayModalWindow => _leavingSecureSiteWarningElement.DisplayModalWindow;
        public string RedirectURL => _leavingSecureSiteWarningElement.RedirectURL;
        public string ExcludedDomains => _leavingSecureSiteWarningElement.ExcludedDomains;
    }
}