using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using NSubstitute;
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
        public void RenderLeftMenu(Core sut)
        {
            sut.LeftMenuItems.Add(new MenuSection("SectionName", "SectionLink", new[] { new Link("Href", "Text"), new MenuItem("Href", "Text", new[] { new MenuItem("subHerf", "subText") }), new MenuItem("Herf", "Text", true) }));

            var result = sut.RenderLeftMenu();

            result.ToString().Should().Be("{\"sections\":[{\"sectionName\":\"SectionName\",\"sectionLink\":\"SectionLink\",\"menuLinks\":[{\"href\":\"Href\",\"text\":\"Text\"},{\"href\":\"Href\",\"text\":\"Text\",\"subLinks\":[{\"subhref\":\"subHerf\",\"subtext\":\"subText\"}]},{\"href\":\"Herf\",\"text\":\"Text\",\"newWindow\":true}]}]}");
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
            result.ToString().Should().ContainAll("class='wb-sessto'", "inactivity", "reactionTime", "sessionAlive", "logoutUrl", "refreshCallBackUrl", "refreshOnClick", "refreshLimit", "method", "additionalData");
        }

        [Theory, AutoNSubstituteData]
        public void RenderServerTop(Core sut)
        {
            var result = sut.RenderServerTop();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderServerBottom(Core sut)
        {
            var result = sut.RenderServerBottom();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderServerRefTop(Core sut)
        {
            var result = sut.RenderServerRefTop();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderServerRefFooter(Core sut)
        {
            var result = sut.RenderServerRefFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderHeaderTitle(Core sut)
        {
            sut.HeaderTitle = "MasterfullHeaderTitle";
            var result = sut.RenderHeaderTitle();
            result.ToString().Should().Be("MasterfullHeaderTitle");
        }

        [Theory, AutoNSubstituteData]
        public void RenderHtmlBodyElementsEmpty(Core sut)
        {
            var result = sut.RenderHtmlBodyElements();
            result.ToString().Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void RenderHtmlHeaderElementsEmpty(Core sut)
        {
            var result = sut.RenderHtmlHeaderElements();
            result.ToString().Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void RenderHtmlBodyElements(Core sut)
        {
            sut.HTMLBodyElements = new List<string> { "Fake Body Element", "Other Item"};
            var result = sut.RenderHtmlBodyElements();
            result.ToString().Should().Be("Fake Body Element\r\nOther Item\r\n");
        }

        [Theory, AutoNSubstituteData]
        public void RenderHtmlHeaderElements(Core sut)
        {
            sut.HTMLHeaderElements = new List<string> { "Fake Header Element", "Other Item" };
            var result = sut.RenderHtmlHeaderElements();
            result.ToString().Should().Be("Fake Header Element\r\nOther Item\r\n");
        }
    }
}
