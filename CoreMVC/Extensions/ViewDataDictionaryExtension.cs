using GoC.WebTemplate.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GoC.WebTemplate.CoreMVC.Extensions
{
    public static class ViewDataDictionaryExtension
    {
        /// <summary>
        /// Returns the strongly-type <see cref="Model"/> located in the <see cref="ViewPage.ViewData"/> property.
        /// </summary>
        /// <param name="viewData">The <see cref="Controller.ViewData"/> used to pass between the controller and the view.></param>
        /// <param name="key">The name of the <see cref="Controller.ViewData"/> entry that contains the <see cref="Model"/>.</param>
        /// <returns>The <see cref="Model"/> from the <see cref="Controller.ViewData"/> property.</returns>
        public static Model GetWebTemplateModel(this ViewDataDictionary viewData, string key = "WebTemplateModel")
            => (Model)viewData[key];
    }
}
