//**********************************************************************
// Documentation:
//
// SDS Wiki Page for WebTemplate: http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network/GoC_.NET_template_guide
// ESDC CDN, Demo Pages, Static Files: http://www.gcpedia.gc.ca/wiki/Content_Delivery_Network
// CDN Framework Implementation Guide: http://www.gcpedia.gc.ca/gcwiki/images/4/41/Templating_v3.0_-_GoC_version.docx
//'**********************************************************************
using System;
using System.Collections.Generic;
using System.Web;
using System.Threading;
using System.Globalization;
using System.Text;
using System.Web.Caching;
using System.Reflection;
using GoC.WebTemplate.Proxies;

// TODO
//consider debug logging?
// in transactional mode, the left menu is not displayed, footer menu not displayed

namespace GoC.WebTemplate
{
    public partial class WebTemplateMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.WebTemplateCore = new Core(new CurrentRequestProxy(), new ConfigurationProxy());
        }

        public Core WebTemplateCore { get; set; }

        /// <summary>
        /// property to hold the version of the template. it will be put as a comment in the html of the master pages. this will help us troubleshoot issues with clients using the template
        /// </summary>
        public string WebTemplateVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
    }
}