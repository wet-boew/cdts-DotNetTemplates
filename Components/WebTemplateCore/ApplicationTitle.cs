namespace GoC.WebTemplate
{
    /// <summary>
    /// The settings object for the Application title that will be displayed in the header above the top menu
    /// </summary>
    public class ApplicationTitle
    {
        /// <summary>
        /// The title that will be displayed in the header above the top menu.
        /// Set programmatically
        /// </summary>
        /// <remarks>only available for intranet themes, and Application Template</remarks>
        public string Text { get; set; }

        /// <summary>
        /// The url of the title that will be displayed in the header above the top menu.
        /// Set programmatically
        /// </summary>
        /// <remarks>
        /// only available for intranet themes
        /// value is optional, if no value is supplied the theme will determine the url
        /// </remarks>
        public string URL { get; set; }
    }
}