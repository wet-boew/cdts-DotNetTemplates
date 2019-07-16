using System;
using System.Threading;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components.Entities
{
    //We don't inherit from link as the Text is readonly and not settable
    public class LanguageLink  
    {
        private readonly string _twoLetterCulture;


        public LanguageLink()
        {
        
            _twoLetterCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        }
        /// <summary>
        /// URL to be used for the language toggle
        /// Set/built by Template
        /// Can be set by application programmatically
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Read only property, used to populate the Lang attribute of the language toggle link
        /// Value is defaulted by Template
        /// </summary>
        public string Lang
        {
            get
            {
                if (string.Compare(_twoLetterCulture, Constants.ENGLISH_ACCRONYM, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return Constants.FRENCH_ACCRONYM;
                }
                return Constants.ENGLISH_ACCRONYM;
            }
        }

        /// <summary>
        /// Read only property, used to populate the text attribute of the language toggle link
        /// Value is defaulted by Template
        /// </summary>
        public string Text
        {
            get
            {
                if (string.Compare(_twoLetterCulture, Constants.ENGLISH_ACCRONYM, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return Constants.LANGUAGE_LINK_FRENCH_TEXT;
                }
                return Constants.LANGUAGE_LINK_ENGLISH_TEXT;
            }
        }

    }
}