using GoC.WebTemplate.Components;
using System.Web.Mvc;

namespace GoC.WebTemplate.MVC.Extensions
{
    public static class ViewDataDictionaryExtension
    {
        /// <summary>
        /// Returns the strongly-type <see cref="Model"/> located in the <see cref="ViewPage.ViewData"/> property.
        /// </summary>
        /// <param name="viewData">The <see cref="ViewPage.ViewData"/> used to pass between the controller and the view.></param>
        /// <param name="key">The name of the <see cref="ViewPage.ViewData"/> entry that contains the <see cref="Model"/>.</param>
        /// <returns>The <see cref="Model"/> from the <see cref="ViewPage.ViewData"/> property.</returns>
        public static Model GetWebTemplateModel(this ViewDataDictionary viewData, string key = "WebTemplateModel")
            => (Model)viewData[key];
    }
}