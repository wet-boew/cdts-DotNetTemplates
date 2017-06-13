 // ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{
    public class LeavingSecureSiteWarning
    {
        /// <summary>
        /// Determines if the a warning should be displayed if the user navigates outside the secure session
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Determines if the popup window should be displayed with the warning message if the user navigates outside the secure session
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool DisplayModalWindow { get; set; }
        /// <summary>
        /// URL to redirect to when sercuresitewarning is enabled and user clicked a link that leaves the secure session
        /// Set by application via web.config
        /// Can be set by application programmatically
        /// </summary>
        public string RedirectURL { get; set; }
        /// <summary>
        /// A comma delimited list of domains that would be excluded from raising the warning
        /// Set by application via web.config
        /// Can be set by application programmatically
        /// </summary>
        public string ExcludedDomains { get; set; }
        /// <summary>
        /// The warning message to be displayed to the user when clicking a link that leaves the secure session
        /// Set by application programmatically
        /// </summary>
        public string Message { get; set; }
    }
}