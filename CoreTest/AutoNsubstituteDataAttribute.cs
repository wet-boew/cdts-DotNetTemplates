using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.Components.Proxies;
using NSubstitute;
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
            fixture.Customize<IConfigurationProxy>(c => c.FromFactory(() =>
            {
                var config = Substitute.For<IConfigurationProxy>();
                //Set Proxy Configurations here
                config.Environment.Returns("ITEM1");
                return config;
            }));

            //Tell autofixture to ignore certain properties as they get set by the configuration file in the constructor
            //Tell it also to ignore show sign in and sign out flags since it'll set them both to true.
            fixture.Customize<Core>(c => c.Without(p => p.ShowSignOutLink)
                                          .Without(p => p.ShowSignInLink)
            //We want custom search to start out as default(string)
                                          .Without(p => p.CustomSearch)
                                          //The Site Menu URL should be null
                                          .Without(p => p.CustomSiteMenuURL)
                                          .Without(p => p.MenuLinks)
            //We also create want to ignore some of the lists as they should start out empty.
            //Normally you don't need to ignore so much but this object breaks normal .Net conventions by using the constructor
            //to set properties, instead of just using the properties
                                          .Without(p => p.HTMLHeaderElements)
                                          .Without(p => p.HTMLBodyElements)
                                          .Without(p => p.Breadcrumbs)
                                          .Without(p => p.SharePageMediaSites)
                                          .Without(p => p.LeftMenuItems)
                                          .Without(p => p.ContactLinks)
                                          .Without(p => p.CustomFooterLinks)
                                          .Without(p => p.FooterSections)
            //Default to the ESDCProd environment if we let autofixture build this it would be the property name and a GUID
                                          .With(p => p.Environment, "ITEM1")
            //Default to UseHTTPS being Null since we are going to set the environments to default to IsSSLModifiable to false
                                          .Without(p => p.UseHTTPS)
            //Default set ShowPostContent to false so Autofixture doesn't alternate the value to true in RenderPreFooterTest, 
            //and RenderPreFooterWithNullsTest.
                                          .With(p => p.ShowPostContent, false)
            //Default set _core.ShowSharePageLink to true so Autofixture doesn't alternate the value to true in RenderPreFooterTest, 
            //and RenderPreFooterWithNullsTest.
                                          .With(p => p.ShowSharePageLink, true)
                                          .With(p => p.LoadJQueryFromGoogle, false));
            //Default to environments not having any fields be modifiable so that we are in a good known state to start
            fixture.Customize<CDTSEnvironment>(c => c.With(p => p.IsEncryptionModifiable, false)
                                                      .With(p => p.IsVersionRNCombined, false));

            fixture.Customize<WebAnalytics>(c => c.With(p => p.Active, false));
            //Since we need to have specific keys for the environments I have to make sure when you get a dictionary of environments
            //that the keys are deterministic.
            fixture.Register<IDictionary<string, ICDTSEnvironment>>(() =>
             {
                 var keys = new[] { "ITEM1", "ITEM2", "ITEM3" };
                 var values = fixture.Create<Generator<ICDTSEnvironment>>();
                 return keys.Zip(values, Tuple.Create).ToDictionary(x => x.Item1, x => x.Item2);
             });

        }
    }

    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(() => new Fixture().Customize(new WebTemplateCustomization()))
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
