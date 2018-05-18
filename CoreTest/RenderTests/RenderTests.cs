using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate;
using NSubstitute;
//using Ploeh.AutoFixture.Xunit2;
using WebTemplateCore.JSONSerializationObjects;
using Xunit;

namespace CoreTest.RenderTests
{
    /// <summary>
    /// Tests that test the Core object in isolation.
    /// </summary>
    public class RenderTests 
    {

        [Theory, AutoNSubstituteData]
        public void DoNotAddCanadaCaToTitlesIfItIsAlreadyThere([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            ICDTSEnvironment env,
            Core sut)
        {

            env.AppendToTitle.Returns(" - Canada.ca");
            environments[sut.Environment] = env;

            sut.HeaderTitle = "Foo - Canada.ca";
            sut.HeaderTitle.Should().Be("Foo - Canada.ca");
        }
        
        [Theory, AutoNSubstituteData]
        public void AddCanadaCaToAllTitlesOnPagesImplementingGCWebTheme([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            ICDTSEnvironment env,
            Core sut)
        {
            env.AppendToTitle.Returns(" - Canada.ca");
            environments[sut.Environment] = env;
            sut.HeaderTitle = "Foo";

            sut.HeaderTitle.Should().Be("Foo - Canada.ca");
        }


        
        
        [Theory, AutoNSubstituteData]
        public void AddCanadaCaToAllTitlesOnPagesWhenTitleIsNullImplementingGCWebTheme([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            ICDTSEnvironment env,
            Core sut)
        {
            env.AppendToTitle.Returns(" - Canada.ca");
            environments[sut.Environment] = env;

            sut.HeaderTitle = null;

            sut.HeaderTitle.Should().Be(" - Canada.ca");
        }
        
        [Theory, AutoNSubstituteData]
        public void DontAddCanadaCaToAllTitlesOnPagesImplementingGCWebTheme([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            ICDTSEnvironment env,
            Core sut)
        {
            env.AppendToTitle.Returns("");
            environments[sut.Environment] = env;

            sut.HeaderTitle = "Foo";

            sut.HeaderTitle.Should().Be("Foo");
        }

        [Theory, AutoNSubstituteData]
        public void RenderLeftMenuTest( Core sut)
        {
            sut.LeftMenuItems.Add(new MenuSection("SectionName", "SectionLink", new[] {new Link("Href", "Text")}));

            var result = sut.RenderLeftMenu();

            result.ToString().Should().Be("sections: [ {sectionName: 'SectionName', sectionLink: 'SectionLink', menuLinks: [{href: 'Href', text: 'Text'},]},]");
        }

        [Theory, AutoNSubstituteData]
        public void RenderEmptyLeftMenu(Core sut)
        {
            var result = sut.RenderLeftMenu();
            result.ToString().Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void RenderSessionTimeoutControl(Core sut)
        {
            sut.SessionTimeout.Enabled = true;
            var result = sut.RenderSessionTimeoutControl();
            result.ToString().Should().ContainAll("class='wb-sessto'", "inactivity", "reactionTime", "sessionAlive", "logoutUrl", "refreshCallbackUrl", "refreshOnClick","refreshLimit","method","additionalData");
        }
    }
}
