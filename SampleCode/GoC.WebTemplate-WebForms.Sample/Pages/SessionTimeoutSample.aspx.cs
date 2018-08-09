using System;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class SessionTimeoutSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //We will set the server session timeout for this page only, to 1 min
            Session.Timeout = 1;
            
            //Let's display what is in the session on the page
            //We will populate the session with the time
            if (Session["stuff"] != null)
            {
                lblID.Text = (Session["stuff"]).ToString();
            }
            else
            {
                lblID.Text = "Data from session: It is empty, refresh page to have value";
                Session.Add("stuff", string.Concat("Data from session: ", DateTime.Now.ToString()));
            }
          
            //enable the sessionTimeout feature
            WebTemplateMaster.WebTemplateCore.SessionTimeout.Enabled = true;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.Inactivity = 30000;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.ReactionTime = 10000;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.Sessionalive = 30000;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.Logouturl = "Logout.aspx";            
            WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshCallbackUrl = "SessionValidity.aspx";
            WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshOnClick = false;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshLimit = 3;
            WebTemplateMaster.WebTemplateCore.SessionTimeout.Method = "";
            WebTemplateMaster.WebTemplateCore.SessionTimeout.AdditionalData = "";
        }
    }
}