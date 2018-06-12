using System;
using System.Threading;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate
{
    public class BasePage : System.Web.UI.Page
    {
        public WebTemplateMasterPage WebTemplateMaster
        {
            get
            {
                // ReSharper disable once SuggestVarOrType_SimpleTypes - needs to be declared
                System.Web.UI.MasterPage temp = Master;

                while (!(temp is WebTemplateMasterPage))
                {
                    try
                    {
                        // ReSharper disable once PossibleNullReferenceException - handled by try
                        temp = temp.Master;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Please refrence a WebTemplate Master Page in the .aspx", ex);
                    }

                }
                return (WebTemplateMasterPage)temp;
            }
        }

        /// <summary>
        /// This method runs during every page request.
        /// The method will set the Thread culture to the value that is provided, in the following order
        ///     -In the querystring from the parameter "GoCTemplateCulture"
        ///     -In the session from the key "GoC.Template.Culture"
        ///     -from the current Thread
        /// It will also update the culture in session if needed    
        /// </summary>
        protected override void InitializeCulture()
        {
            //Get culture from Querystring
            var culture = Request.QueryString.Get(Constants.QUERYSTRING_CULTURE_KEY);

            if ((string.IsNullOrEmpty(culture)))
            {
                if (Context.Session != null)
                {
                    //culture not found in querystring, check session
                    culture = Convert.ToString(Session[Constants.SESSION_CULTURE_KEY], CultureInfo.CurrentCulture);

                    if ((string.IsNullOrEmpty(culture)))
                    {
                        //culture not found in session, use default language
                        culture = Constants.ENGLISH_CULTURE;
                        Session[Constants.SESSION_CULTURE_KEY] = culture;
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

                if (Context.Session != null)
                    Session[Constants.SESSION_CULTURE_KEY] = culture;
            }

            //If we have a culture, set it
            if (!string.IsNullOrEmpty(culture))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
                base.InitializeCulture();
            }
        }
    }
}