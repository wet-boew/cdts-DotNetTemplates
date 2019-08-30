using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Entities;
using NSubstitute;
using Xunit;
using Xunit.Sdk;

namespace GoC.WebTemplate.Components.Test
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
            fixture.Customize<WebTemplateSettings>(
                c => c.OmitAutoProperties()
                      .With(p => p.Environment, "ITEM1")
            );

            //Tell autofixture to ignore certain properties as they get set by the configuration file in the constructor
            //Tell it also to ignore show sign in and sign out flags since it'll set them both to true.
            fixture.Customize<Model>(c => c.Without(p => p.ShowSignOutLink)
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

            fixture.Customize<IntranetTitle>(c => c.With(p => p.NewWindow, false));

            //Default to environments not having any fields be modifiable so that we are in a good known state to start
            fixture.Customize<CdtsEnvironment>(c => c.With(p => p.IsEncryptionModifiable, false)
                                                      .With(p => p.IsVersionRNCombined, false));
            //Since we need to have specific keys for the environments I have to make sure when you get a dictionary of environments
            //that the keys are deterministic.
            fixture.Register<IDictionary<string, ICdtsEnvironment>>(() =>
             {
                 var keys = new[] { "ITEM1", "ITEM2", "ITEM3" };
                 var values = fixture.Create<Generator<ICdtsEnvironment>>();
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
