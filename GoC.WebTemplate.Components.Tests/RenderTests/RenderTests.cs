using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils.Caching;
using NSubstitute;
using Xunit;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    /// <summary>
    /// Tests that test the Model object in isolation.
    /// </summary>
    public class RenderTests
    {

        [Theory, AutoNSubstituteData]
        public void DoNotAddCanadaCaToTitlesIfItIsAlreadyThere([Frozen] ICdtsCacheProvider cdtsCacheProvider,
            ICdtsEnvironment env,
            Model sut)
        {

            env.AppendToTitle.Returns(" - Canada.ca");
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = env;

            sut.HeaderTitle = "Foo - Canada.ca";
            sut.HeaderTitle.Should().Be("Foo - Canada.ca");
        }

        [Theory, AutoNSubstituteData]
        public void AddCanadaCaToAllTitlesOnPagesImplementingGCWebTheme([Frozen]ICdtsCacheProvider cdtsCacheProvider, ICdtsEnvironment env, Model sut)
        {
            env.AppendToTitle.Returns(" - Canada.ca");
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = env;
            sut.HeaderTitle = "Foo";

            sut.HeaderTitle.Should().Be("Foo - Canada.ca");
        }

        [Theory, AutoNSubstituteData]
        public void AddCanadaCaToAllTitlesOnPagesWhenTitleIsNullImplementingGCWebTheme([Frozen]ICdtsCacheProvider cdtsCacheProvider, ICdtsEnvironment env, Model sut)
        {
            env.AppendToTitle.Returns(" - Canada.ca");
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = env;

            sut.HeaderTitle = null;

            sut.HeaderTitle.Should().Be(" - Canada.ca");
        }

        [Theory, AutoNSubstituteData]
        public void DontAddCanadaCaToAllTitlesOnPagesImplementingGCWebTheme([Frozen] ICdtsCacheProvider cdtsCacheProvider,
            ICdtsEnvironment env,
            Model sut)
        {
            env.AppendToTitle.Returns("");
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = env;

            sut.HeaderTitle = "Foo";

            sut.HeaderTitle.Should().Be("Foo");
        }

        [Theory, AutoNSubstituteData]
        public void RenderLeftMenu(Model sut)
        {
            sut.LeftMenuItems.Add(new MenuSection
            {
                Name = "SectionName",
                Link = "SectionLink",
                Items = new List<Link> {
                    new Link { Href = "Href", Text = "Text" },
                    new MenuItem {
                        Href = "Href",
                        Text = "Text",
                        SubItems = new List<MenuItem> {
                            new MenuItem { Href = "subHerf", Text = "subText" }
                        }
                    },
                    new MenuItem { Href = "Herf", Text = "Text", NewWindow = true }
                }
            });
            var result = sut.Render.LeftMenu();

            result.ToString().Should().Be("{\"sections\":[{\"sectionName\":\"SectionName\",\"sectionLink\":\"SectionLink\",\"menuLinks\":[{\"href\":\"Href\",\"text\":\"Text\"},{\"href\":\"Href\",\"text\":\"Text\",\"subLinks\":[{\"subhref\":\"subHerf\",\"subtext\":\"subText\"}]},{\"href\":\"Herf\",\"text\":\"Text\",\"newWindow\":true}]}]}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderEmptyLeftMenu(Model sut)
        {
            var result = sut.Render.LeftMenu();
            result.ToString().Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void RenderSessionTimeoutControl(Model sut)
        {
            sut.SessionTimeout.Enabled = true;
            var result = sut.Render.SessionTimeoutControl();
            result.ToString().Should().ContainAll("class='wb-sessto'", "inactivity", "reactionTime", "sessionalive", "logouturl", "refreshCallbackUrl", "refreshOnClick", "refreshLimit", "method", "additionalData");
        }

        [Theory, AutoNSubstituteData]
        public void RenderServerTop(Model sut)
        {
            var result = sut.Render.ServerTop();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderServerBottom(Model sut)
        {
            var result = sut.Render.ServerBottom();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderServerRefTop(Model sut)
        {
            var result = sut.Render.ServerRefTop();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderServerRefFooter(Model sut)
        {
            var result = sut.Render.ServerRefFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderHeaderTitle(Model sut)
        {
            sut.HeaderTitle = "MasterfullHeaderTitle";
            var result = sut.Render.HeaderTitle();
            result.ToString().Should().Be("MasterfullHeaderTitle");
        }

        [Theory, AutoNSubstituteData]
        public void RenderHtmlBodyElementsEmpty(Model sut)
        {
            var result = sut.Render.HtmlBodyElements();
            result.ToString().Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void RenderHtmlHeaderElementsEmpty(Model sut)
        {
            var result = sut.Render.HtmlHeaderElements();
            result.ToString().Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void RenderHtmlBodyElements(Model sut)
        {
            sut.HTMLBodyElements = new List<string> { "Fake Body Element", "Other Item" };
            var result = sut.Render.HtmlBodyElements();
            result.ToString().Should().Be("Fake Body Element\r\nOther Item\r\n");
        }

        [Theory, AutoNSubstituteData]
        public void RenderHtmlHeaderElements(Model sut)
        {
            sut.HTMLHeaderElements = new List<string> { "Fake Header Element", "Other Item" };
            var result = sut.Render.HtmlHeaderElements();
            result.ToString().Should().Be("Fake Header Element\r\nOther Item\r\n");
        }
    }
}
