using System;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
{
    public partial class SessionValidity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //This sample page is referenced by the "refreshCallbackUrl" setting of the WET SessionTimeout control
            //It's first function is to verify that the users session is still alive
            //It's second function is to reset the timer of the server session to zero.

            //returns "true" if the original session is still alive
            Response.Write((!Session.IsNewSession).ToString().ToLower()); 
            Response.End();
        }
    }
}