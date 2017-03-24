namespace GoC.WebTemplate.ConfigSections
{
    public interface ILeavingSecureSiteWarningElementProxy
    {
        /// <summary>
        /// enabled
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// enabled
        /// </summary>
        bool DisplayModalWindow { get; }

        /// <summary>
        /// URL that users are sent to when "yes" is selected on the warning message.  URL to write application code clean up before redirecting to selected url.
        /// </summary>
        string RedirectURL { get; }

        /// <summary>
        /// domains that should not raise the warning message
        /// </summary>
        string ExcludedDomains { get; }
    }
}