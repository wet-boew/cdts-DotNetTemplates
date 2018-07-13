using System;
using GoC.WebTemplate.WebForms;

namespace GoC.WebTemplate.WebForm.Sample.Pages
{
    public partial class SplashPageSample : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebTemplateMaster.WebTemplateCore.SplashPageInfo.EnglishHomeUrl = "http://www.canada.ca/en/index.html";
            WebTemplateMaster.WebTemplateCore.SplashPageInfo.FrenchHomeUrl = "http://www.canada.ca/fr/index.html";
            WebTemplateMaster.WebTemplateCore.SplashPageInfo.EnglishTermsUrl = "http://www.canada.ca/en/transparency/terms.html";
            WebTemplateMaster.WebTemplateCore.SplashPageInfo.FrenchTermsUrl = "http://www.canada.ca/fr/transparence/avis.html";
            WebTemplateMaster.WebTemplateCore.SplashPageInfo.EnglishName = "[My web asset]";
            WebTemplateMaster.WebTemplateCore.SplashPageInfo.FrenchName = "[Mon actif web]";
        }
        
    }
}