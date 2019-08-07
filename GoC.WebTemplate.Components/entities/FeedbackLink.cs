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
    }
}