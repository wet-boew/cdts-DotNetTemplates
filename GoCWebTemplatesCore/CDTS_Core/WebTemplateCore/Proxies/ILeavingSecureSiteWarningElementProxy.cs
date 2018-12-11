namespace CDTS_Core.WebTemplateCore.Proxies
{
    public interface ILeavingSecureSiteWarningElementProxy
    {
        bool Enabled
        {
            get;
        }

        bool DisplayModalWindow
        {
            get;
        }

        string RedirectURL
        {
            get;
        }

        string ExcludedDomains
        {
            get;
        }
    }

}
