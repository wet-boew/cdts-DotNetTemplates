using System;

namespace GoC.WebTemplate.WebForms
{
    public partial class TestSplashTemplate : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebTemplateMaster.WebTemplateModel.SplashPageInfo.EnglishHomeUrl = "http://www.canada.ca/en/index.html";
            WebTemplateMaster.WebTemplateModel.SplashPageInfo.FrenchHomeUrl = "http://www.canada.ca/fr/index.html";
            WebTemplateMaster.WebTemplateModel.SplashPageInfo.EnglishTermsUrl = "http://www.canada.ca/en/transparency/terms.html";
            WebTemplateMaster.WebTemplateModel.SplashPageInfo.FrenchTermsUrl = "http://www.canada.ca/fr/transparence/avis.html";
            WebTemplateMaster.WebTemplateModel.SplashPageInfo.EnglishName = "[My web asset]";
            WebTemplateMaster.WebTemplateModel.SplashPageInfo.FrenchName = "[Mon actif web]";
        }
    }
}