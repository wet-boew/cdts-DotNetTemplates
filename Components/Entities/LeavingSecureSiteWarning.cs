// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Entities
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
#pragma warning disable CA1056
        /// <summary>
        /// URL to redirect to when sercuresitewarning is enabled and user clicked a link that leaves the secure session
        /// Set by application via web.config
        /// Can be set by application programmatically
        /// </summary>
        public string RedirectURL { get; set; }
#pragma warning restore CA1056
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

        /// <summary>
        /// Specify the text for the button (Cancel) the users will get if they want to close the exitMsg window. 
        /// Set by application programmatically
        /// </summary>
        public string CancelMessage { get; set; }

        /// <summary>
        /// Specify the text for the button (Yes) the users will get if they want to close the exitMsg window. 
        /// Set by application programmatically
        /// </summary>
        public string YesMessage { get; set; }

        /// <summary>
        /// Specify the text that will warn the user that the link will open in a new window. 
        /// Set by application programmatically
        /// </summary>
        public string TargetWarning { get; set; }

        /// <summary>
        /// Determines if the popup window should be displayed with the warning message if the user navigates outside the secure session (for links that open in a window)
        /// Set by application via web.config
        /// or Set by application programmatically
        /// </summary>
        public bool DisplayModalForNewWindow { get; set; }
    }
}