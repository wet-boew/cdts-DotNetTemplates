using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.CoreMVC.Controllers;

namespace GoC.WebTemplate.MVC.Sample.Controllers
{
    public class ExtendedBaseController : WebTemplateBaseController
    {
        public string UserName = "Mr. Fancy Pants";

        public ExtendedBaseController(IModelAccessor modelAccessor)
            : base(modelAccessor)
        {
            //Set a the common title for everypage here
            WebTemplateModel.HeaderTitle = "Title set for everypage!";
        }
        
        public string GetWeather()
        {
            // get data from source...
            // do some calculation...
            // etc....
            return "Sunny";
        }
    }
}