using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Utils.Caching;
using System;
using System.Configuration;
using System.Reflection;
using System.Web;

namespace GoC.WebTemplate.WebForms
{
    public partial class WebTemplateMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            WebTemplateModel = 
                new Model(
                    new FileContentCacheProvider(HttpRuntime.Cache), 
                    new WebTemplateSettings(ConfigurationManager.GetSection("GoC.WebTemplate") as GocWebTemplateConfigurationSection), 
                    new CdtsCacheProvider(HttpRuntime.Cache)
                );
            
            //update the culture based on the query string or what is stored in session
            WebTemplateModel.LanguageLink.Href = ModelBuilder.BuildLanguageLinkURL(HttpUtility.ParseQueryString(Request.QueryString.ToString()));

            //update the herf link depending on the current culture keeping the rest of the querystring intact
            WebTemplateModel.Settings.SessionTimeout.CheckWithServerSessionTimeout(Session);
        }

        public IModel WebTemplateModel { get; set; }

        /// <summary>
        /// property to hold the version of the template. it will be put as a comment in the html of the master pages. this will help us troubleshoot issues with clients using the template
        /// </summary>
        public string WebTemplateVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
    }
}