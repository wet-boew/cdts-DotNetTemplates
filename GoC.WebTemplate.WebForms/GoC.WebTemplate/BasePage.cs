using System;
using GoC.WebTemplate.Components.Framework.Utils;
using GoC.WebTemplate.Components.Utils;

namespace GoC.WebTemplate.WebForms
{
    public class BasePage : System.Web.UI.Page
    {
        public WebTemplateMasterPage WebTemplateMaster
        {
            get
            {
                try
                {
                    // ReSharper disable once SuggestVarOrType_SimpleTypes - needs to be declared
                    System.Web.UI.MasterPage temp = Master;

                    while (!(temp is WebTemplateMasterPage))
                    {
                        // ReSharper disable once PossibleNullReferenceException - handled by try
                        temp = temp.Master;

                    }
                    return (WebTemplateMasterPage)temp;
                }
                catch (Exception ex)
                {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw new ArgumentNullException("Please reference a WebTemplate Master Page in the .aspx", ex);
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                }
            }
        }

        /// <summary>
        /// This method runs during every page request.
        /// It will also update the culture in session if needed    
        /// </summary>
        protected override void InitializeCulture()
        {
            var langSwitcher = new LanguageSwitcher(new SessionController(System.Web.HttpContext.Current.Session));
            langSwitcher.UpdateCulture(Request.QueryString.Get(Constants.QUERYSTRING_CULTURE_KEY));

            base.InitializeCulture();
        }
    }
}