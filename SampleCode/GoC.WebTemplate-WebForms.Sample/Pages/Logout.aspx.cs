using System;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //This sample page is referenced by the "logoutUrl" setting of the WET SessionTimeout control
            //It will not be displayed to the user, but is used to perform session clean up and any other clean up that is required before the user is loged out of your application.
            //This page will then redirect to the page you identify example the login page (in our case the BaseSettingsSample.aspx)

            //destroy users sessions
            this.Session.Abandon();

            //perform any other clean up that needs to occur for your application

            //redirect to the page of your preference
            Response.Redirect("BaseSettingsSample.aspx");

        }
    }
}