using System;

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
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Enabled = true;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Inactivity = 30000;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.ReactionTime = 10000;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Sessionalive = 30000;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Logouturl = "Logout.aspx";            
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshCallbackUrl = "SessionValidity.aspx";
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshOnClick = false;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.RefreshLimit = 3;
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.Method = "";
            this.WebTemplateMaster.WebTemplateCore.SessionTimeout.AdditionalData = "";
        }
    }
}