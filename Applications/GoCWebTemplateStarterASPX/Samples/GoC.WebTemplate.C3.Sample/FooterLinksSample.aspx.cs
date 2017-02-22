using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoC.WebTemplate;

public partial class FooterLinksSample : GoC.WebTemplate.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Contact Links
        this.WebTemplateMaster.WebTemplateCore.ContactLinks.Add(new Link("http://travel.gc.ca/", "Travel"));
        this.WebTemplateMaster.WebTemplateCore.ContactLinks.Add(new Link("http://healthycanadians.gc.ca/index-eng.php", "Health"));
        this.WebTemplateMaster.WebTemplateCore.ContactLinks.Add(new Link("http://jobs-emplois.gc.ca/index-eng.htm", "Jobs"));

        //News Links
        this.WebTemplateMaster.WebTemplateCore.NewsLinks.Add(new Link("http://www.cbc.ca/news/canada", "CBC"));
        this.WebTemplateMaster.WebTemplateCore.NewsLinks.Add(new Link("http://www.cnn.com/", "CNN"));

        //About Links
        this.WebTemplateMaster.WebTemplateCore.AboutLinks.Add(new Link("https://www.facebook.com", "Facebook"));
        this.WebTemplateMaster.WebTemplateCore.AboutLinks.Add(new Link("http://www.lapresse.ca/", "LaPresse"));

        //Note: For your solution, the values should be coming from your culture sensitive source ex: resource files, db etc...)

    }
}
