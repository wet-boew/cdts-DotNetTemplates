using System;
using System.Globalization;
using System.Threading;

namespace GoC.WebTemplate.Components.Utils
{
    public class LanguageSwitcher
    {
        private readonly ISessionController _sessionController;

        public LanguageSwitcher(ISessionController sessionControler)
        {
            _sessionController = sessionControler;
        }

        /// <summary>
        /// Uses the QueryString to check for the language variable and updates the Thread for the correct language
        /// 
        /// The method will set the Thread culture to the value that is provided, in the following order
        ///     -In the querystring from the parameter "GoCTemplateCulture"
        ///     -In the session from the key "GoC.Template.Culture"
        ///     -from the current Thread
        /// </summary>
        /// <param name="culture">The culture result from the query string from the browser</param>
        public void UpdateCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                if (_sessionController.SessionExists())
                {
                    //culture not found in querystring, check session
                    culture = Convert.ToString(_sessionController.GetSession(Constants.SESSION_CULTURE_KEY), CultureInfo.CurrentCulture);

                    if (string.IsNullOrEmpty(culture))
                    {
                        //culture not found in session, use default language
                        culture = Constants.ENGLISH_CULTURE;
                        _sessionController.SetSession(Constants.SESSION_CULTURE_KEY, culture);
                    }
                }
                else
                {
                    culture = Constants.ENGLISH_CULTURE;
                }
            }
            else
            {
                //culture found in querystring, use it
                culture = culture.StartsWith(Constants.ENGLISH_ACCRONYM, StringComparison.CurrentCultureIgnoreCase) ? Constants.ENGLISH_CULTURE : Constants.FRENCH_CULTURE;
                if (_sessionController.SessionExists())
                    _sessionController.SetSession(Constants.SESSION_CULTURE_KEY, culture);
            }

            //If we have a culture, set it
            if (!string.IsNullOrEmpty(culture))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            }
        }
    }
}
