using System;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
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
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.Enabled = true;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.Inactivity = 30000;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.ReactionTime = 10000;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.SessionAlive = 30000;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.LogoutUrl = "Logout.aspx";            
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.RefreshCallBackUrl = "SessionValidity.aspx";
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.RefreshOnClick = false;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.RefreshLimit = 3;
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.Method = "";
            WebTemplateMaster.WebTemplateModel.Settings.SessionTimeout.AdditionalData = "";
        }
    }
}