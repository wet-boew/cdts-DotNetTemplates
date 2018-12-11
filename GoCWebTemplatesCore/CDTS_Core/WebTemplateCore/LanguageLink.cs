using System;
using System.Threading;

namespace CDTS_Core.WebTemplateCore
{
    public class LanguageLink
    {
        private readonly string _twoLetterCulture;

        public string Href
        {
            get;
            set;
        }

        public string Lang
        {
            get
            {
                if (string.Compare(_twoLetterCulture, "en", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return "fr";
                }
                return "en";
            }
        }

        public string Text
        {
            get
            {
                if (string.Compare(_twoLetterCulture, "en", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return "Français";
                }
                return "English";
            }
        }

        public LanguageLink()
        {
            _twoLetterCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        }
    }
}
