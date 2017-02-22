using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;
using GoC.WebTemplate;

namespace GoC.WebTemplate
{
    public partial class BasePage : System.Web.UI.Page
    {
        public GoC.WebTemplate.WebTemplateMasterPage WebTemplateMaster
        {
            get
            {
                System.Web.UI.MasterPage temp = base.Master;

                while (! (temp is GoC.WebTemplate.WebTemplateMasterPage))
                {
                    temp = temp.Master;

                }
                return (GoC.WebTemplate.WebTemplateMasterPage)temp;
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
            string culture = this.Request.QueryString.Get(Constants.QUERYSTRING_CULTURE_KEY);

            if ((string.IsNullOrEmpty(culture)))
            {
                if (this.Context.Session != null)
                {
                    //culture not found in querystring, check session
                    culture = Convert.ToString(Session[Constants.SESSION_CULTURE_KEY], CultureInfo.CurrentCulture);

                    if ((string.IsNullOrEmpty(culture)))
                    {
                        //culture not found in session, use default language
                        culture = Constants.ENGLISH_CULTURE;
                        this.Session[Constants.SESSION_CULTURE_KEY] = culture;
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

                if (this.Context.Session != null) this.Session[Constants.SESSION_CULTURE_KEY] = culture;
            }

            //If we have a culture, set it
            if (!string.IsNullOrEmpty(culture))
            {
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(culture);
                base.InitializeCulture();
            }             
        }
    }
}