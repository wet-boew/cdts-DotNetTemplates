using System.Web;
using GoC.WebTemplate;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using Xunit.Sdk;

namespace CoreTest
{
    public class WebTemplateCustomization : CompositeCustomization
    {
        public WebTemplateCustomization(): base(new CoreCustomization(), new HttpContextCustomization(), new AutoNSubstituteCustomization())
        {
            
        }
    }
    public class CoreCustomization : ICustomization {
        public void Customize(IFixture fixture)
        {
            //Tell autofixture to ignore certain properties as they get set by the configuration file in the constructor
            fixture.Customize<Core>(c => c.Without(p => p.LeftMenuItems)
                                          .Without(p=> p.Environment));
        }
    }

    public class HttpContextCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Core>(c =>
            {
                //We must set the httpcontext here as we are unable to set it in the xunit test class since it won't be run before the object is created.
                //If the user wants a different querystring they should set it themselves in the test.
                HttpContext.Current = new HttpContext(
                    new HttpRequest(null, "http://foo.bar", string.Empty),
                    new HttpResponse(null));
                return c;
            });
        }
    }
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(new Fixture().Customize(new WebTemplateCustomization()))
        {
        }
    }

    public class InlineAutoNSubstituteDataAttribute : CompositeDataAttribute
    {
        public InlineAutoNSubstituteDataAttribute(params object[] values)
            : base(new DataAttribute[]
            {
                new InlineDataAttribute(values), new AutoNSubstituteDataAttribute()
            })
        {
        }
    }


}
