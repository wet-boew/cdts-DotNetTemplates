using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Entities;
using NSubstitute;
using Xunit;
using Xunit.Sdk;

namespace GoC.WebTemplate.Components.Test
{
    public class WebTemplateCustomization : CompositeCustomization
    {
        public WebTemplateCustomization() : base(new CoreCustomization(), new AutoNSubstituteCustomization())
        {

        }
    }
    public class CoreCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            //TODO: Why are we settings these default values? If some tests depend on specific values, they should set it themselves... Either we auto-set everything or nothing.
            fixture.Register<IWebTemplateSettings>(() => fixture.Create<WebTemplateSettings>());
            fixture.Customize<WebTemplateSettings>(c => c
                .Without(p => p.UseHttps)
                .Without(p => p.LoadScriptsFromGoogle)
                .With(p => p.ShowPostContent, false));

            //Tell autofixture to ignore certain properties as they get set by the configuration file in the constructor
            //Tell it also to ignore show sign in and sign out flags since it'll set them both to true.
            //TODO: Why are we settings these default values? If some tests depend on specific values, they should set it themselves... Either we auto-set everything or nothing.
            fixture.Customize<Model>(c => c
                .Without(p => p.ShowSignOutLink)
                .Without(p => p.ShowSignInLink)
                .Without(p => p.CustomSearch)
                .Without(p => p.CustomSiteMenuURL)
                .Without(p => p.MenuLinks)
                .Without(p => p.HTMLHeaderElements)
                .Without(p => p.HTMLBodyElements)
                .Without(p => p.Breadcrumbs)
                .Without(p => p.SharePageMediaSites)
                .Without(p => p.LeftMenuItems)
                .Without(p => p.ContactLinks)
                .Without(p => p.CustomFooterLinks)
                .Without(p => p.FooterSections));

            fixture.Customize<IntranetTitle>(c => c.With(p => p.NewWindow, false));

            //Default to environments not having any fields be modifiable so that we are in a good known state to start
            fixture.Customize<CdtsEnvironment>(c => c.With(p => p.IsEncryptionModifiable, false)
                                                      .With(p => p.IsVersionRNCombined, false));

            //When auto generated Active should default to false, changed to true if grabing from config
            fixture.Customize<WebAnalytics>(c => c.With(p => p.Active, false));

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
