using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using GoC.WebTemplate;

public partial class TransactionalSample : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //set the Terms and Condition Link
        this.WebTemplateMaster.WebTemplateCore.TermsConditionsLink_URL = "http://www.tsn.ca";
        //set the Privacy link
        this.WebTemplateMaster.WebTemplateCore.PrivacyLink_URL = "http://www.lapresse.ca";
    }
}
