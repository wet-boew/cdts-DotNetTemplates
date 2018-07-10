using System;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class ExtendedBasePageSample : ExtendedBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSessionID.Text = SessionID;
            lblWeather.Text = GetWeather();

        }
    }
}