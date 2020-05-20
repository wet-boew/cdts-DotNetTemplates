using FluentAssertions;
using GoC.WebTemplate.Components.Entities;
using NSubstitute;
using System;
using Xunit;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderTopTests
    {
        [Theory, AutoNSubstituteData]
        public void BreadCrumbEmptyAcronymShouldNotRender(Model sut)
        {
            sut.Breadcrumbs.Add(new Breadcrumb
            {
                Title = "Foo.bar",
                Acronym = ""
            });

            sut.Render.Top().ToString().Should().NotContain("\"acronym\":\"\"");
        }

        [Theory, AutoNSubstituteData]
        public void BreadCrumbEmptyHrefShouldNotRender(Model sut)
        {
            sut.Breadcrumbs.Add(new Breadcrumb
            {
                Title = "Foo.bar",
                Href = ""
            });
            sut.Render.Top().ToString().Should().NotContain("\"href\":\"\"");
        }

        [Theory, AutoNSubstituteData]
        public void BreadCrumbConstructorPropertiesRender(Model sut)
        {
            sut.Breadcrumbs.Add(new Breadcrumb { Href = "www.href.com", Title = "Herf Title", Acronym = "HHH" });
            sut.Render.Top().ToString().Should().ContainAll(new string[] { "\"href\":\"www.href.com\"", "\"title\":\"Herf Title\"", "\"acronym\":\"HHH\"" });
        }


        [Theory, AutoNSubstituteData]
        public void DoNotRenderBreadCrumbsByDefault(Model sut)
        {
            sut.Render.Top().ToString().Should().NotContain("\"breadcrumbs\"");
        }

        [Theory, AutoNSubstituteData]
        public void IntranetTitleShouldNotRenderWhenNullInTop(Model sut)
        {
            sut.IntranetTitle = null;
            sut.Render.Top().ToString().Should().NotContain("\"intranetTitle\":[null]");
        }

        [Theory, AutoNSubstituteData]
        public void IntranetTitleTop(Model sut)
        {
            sut.IntranetTitle = new IntranetTitle { Text = "foo", Href = "bar", Acronym = "plat" };
            sut.Render.Top().ToString().Should().Contain("\"intranetTitle\":[{\"href\":\"bar\",\"text\":\"foo\",\"acronym\":\"plat\"}]");
        }



        [Theory, AutoNSubstituteData]
        public void TopSecMenuFalseInTopWhenLeftMenuItems(Model sut)
        {
            sut.Render.Top().ToString().Should().Contain("\"topSecMenu\":false");
        }

        [Theory, AutoNSubstituteData]
        public void TopSecMenuTrueInTopWhenLeftMenuItems(Model sut)
        {
            sut.LeftMenuItems.Add(new MenuSection
            {
                Href = "foo",
                Text = "bar"
            });
            sut.Render.Top().ToString().Should().Contain("\"topSecMenu\":true");
        }

        [Theory, AutoNSubstituteData]
        public void CustomSearchRenders(Model sut)
        {
            sut.CustomSearch = new CustomSearch
            {
                Action = "#",
                Placeholder = "Custom Search Placeholder",
                Method = "get"
            };
            var result = sut.Render.Top().ToString();
            result.Should().Contain("\"customSearch\":[{\"action\":\"#\",\"placeholder\":\"Custom Search Placeholder\",\"method\":\"get\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void GcToolsMoalRendersInIntranet(Model sut)
        {
            sut.Settings.GcToolsModal = true;
            sut.CdtsEnvironment.Theme = "gcintranet";

            var result = sut.Render.Top().ToString();
            result.Should().Contain("\"GCToolsModal\":true");
        }

        [Theory, AutoNSubstituteData]
        public void GcToolsMoalThrowsExeptionInGcWeb(Model sut)
        {
            sut.Settings.GcToolsModal = true;
            sut.CdtsEnvironment.ThemeIsGCWeb().Returns(true);

            Action act = () => sut.Render.Top();
            act.Should().Throw<NotSupportedException>();
        }

    }
}