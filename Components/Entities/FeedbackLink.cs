namespace GoC.WebTemplate.Components.Entities
{
    public class FeedbackLink
    {

        /// <summary>
        /// Determines if the FeedBack link of the footer is to be displayed
        /// Set by application via web.config 'ShowFeedbackLink' or programmatically
        /// </summary>
        public bool Show { get; set; }

        /// <summary>
        /// URL to be used for the feedback link
        /// Set by application via web.config 'FeedbackLinkUrl' or programmatically
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// URL to be used for the feedback link when in french culture
        /// Set by application via web.config 'FeedbackLinkUrlFr' or programmatically
        /// If it is empty will asume FeebackLinkurl is bilingual or also non-existant
        /// </summary>
        public string UrlFr { get; set; }

        /// <summary>
        /// Set a display name of your contact link within the feedback tool
        /// Set by application via web.config 'FeedbackLinkText' or programmatically
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// URL to be used for the contact link within the feedback tool
        /// Set by application via web.config 'FeedbackLinkHref' or programmatically
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Define the theme of your page
        /// Set by application via web.config 'FeedbackLinkTheme' or programmatically
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// Define the section where your page resides
        /// Set by application via web.config 'FeedbackLinkSection' or programmatically
        /// </summary>
        public string Section { get; set; }
    }
}