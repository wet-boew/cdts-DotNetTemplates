using System;
using GoC.WebTemplate.WebForm.Sample.Pages;

namespace GoC.WebTemplate.WebForm.Sample.SamplePages
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