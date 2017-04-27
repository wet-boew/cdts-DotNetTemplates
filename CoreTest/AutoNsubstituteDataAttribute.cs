using System;
using System.Collections.Generic;
using System.Linq;
using GoC.WebTemplate;
using GoC.WebTemplate.ConfigSections;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using Xunit.Sdk;

namespace CoreTest
{
    public class WebTemplateCustomization : CompositeCustomization
    {
        public WebTemplateCustomization(): base(new CoreCustomization(),  new AutoNSubstituteCustomization())
        {
            
        }
    }
    public class CoreCustomization : ICustomization {
        public void Customize(IFixture fixture)
        {
            //Tell autofixture to ignore certain properties as they get set by the configuration file in the constructor
            //In particular ignore the sitemenu since we want it to default to true;
            fixture.Customize<Core>(c => c.Without(p => p.ShowSiteMenu)
            //Tell it also to ignore show sign in and sign out flags since it'll set them both to true.
                                          .Without(p => p.ShowSignOutLink)
                                          .Without(p => p.ShowSignInLink)
            //The Site Menu URL should be null
                                          .Without(p => p.CustomSiteMenuURL)
            //We also create want to ignore some of the lists as they should start out empty.
            //Normally you don't need to ignore so much but this object breaks normal .Net conventions by using the constructor
            //to set properties, instead of just using the properties
                                          .Without(p => p.HTMLHeaderElements)
                                          .Without(p => p.HTMLBodyElements)
                                          .Without(p => p.ContactLinks)
                                          .Without(p => p.NewsLinks)
                                          .Without(p => p.Breadcrumbs)
                                          .Without(p => p.SharePageMediaSites)
                                          .Without(p=> p.LeftMenuItems)
            //Default to the ESDCProd environment if we let autofixture build this it would be the property name and a GUID
                                          .With(p => p.Environment, "ESDCProd"));
            //Since we need to have specific keys for the environments I have to make sure when you get a dictionary of environments
            //that the keys are deterministic.
            fixture.Register<IDictionary<string,ICDTSEnvironmentElementProxy>>(() =>
            {
                var keys = new[] {"Akamai", "ESDCProd", "Prod", "NonProd", "QAT"};
                var values = fixture.Create<Generator<ICDTSEnvironmentElementProxy>>();
                return keys.Zip(values, Tuple.Create).ToDictionary(x => x.Item1, x => x.Item2);
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
