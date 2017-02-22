using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleCode.C3.Samples
{
    public partial class ExtendedBasePageSample : ExtendedBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSessionID.Text = this.SessionID;
            lblWeather.Text = this.GetWeather();

        }
    }
}