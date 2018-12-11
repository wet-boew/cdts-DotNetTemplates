namespace CDTS_Core.WebTemplateCore
{
    public class LeavingSecureSiteWarning
    {
        public bool Enabled
        {
            get;
            set;
        }

        public bool DisplayModalWindow
        {
            get;
            set;
        }

        public string RedirectURL
        {
            get;
            set;
        }

        public string ExcludedDomains
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }

}
