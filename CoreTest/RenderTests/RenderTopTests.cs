using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using Xunit;

namespace CoreTest.RenderTests
{
    public class RenderTopTests
    {
        [Theory, AutoNSubstituteData]
        public void BreadCrumbEmptyAcronymShouldNotRender(Core sut)
        {
            sut.Breadcrumbs.Add(new Breadcrumb
            {
                Title = "Foo.bar",
                Acronym = ""
            });
            sut.RenderTop().ToString().Should().NotContain("\"acronym\":\"\"");
        }

        [Theory, AutoNSubstituteData]
        public void BreadCrumbEmptyHrefShouldNotRender(Core sut)
        {
            sut.Breadcrumbs.Add(new Breadcrumb
            {
                Title = "Foo.bar",
                Href = ""
            });
            sut.RenderTop().ToString().Should().NotContain("\"href\":\"\"");
        }

        [Theory, AutoNSubstituteData]
        public void BreadCrumbConstructorPropertiesRender(Core sut)
        {
            sut.Breadcrumbs.Add(new Breadcrumb("www.href.com", "Herf Title", "HHH"));
            sut.RenderTop().ToString().Should().ContainAll(new string[] {"\"href\":\"www.href.com\"", "\"title\":\"Herf Title\"", "\"acronym\":\"HHH\"" });
        }


        [Theory, AutoNSubstituteData]
        public void DoNotRenderBreadCrumbsByDefault(Core sut)
        {
            sut.RenderTop().ToString().Should().NotContain("\"breadcrumbs\"");
        }

        [Theory, AutoNSubstituteData]
        public void IntranetTitleShouldNotRenderWhenNullInTop(Core sut)
        {
            sut.IntranetTitle = null;
            sut.RenderTop().ToString().Should().NotContain("\"intranetTitle\":[null]");
        }

        [Theory, AutoNSubstituteData]
        public void IntranetTitleTop(Core sut)
        {
            sut.IntranetTitle = new IntranetTitle { Text = "foo", Href = "bar", Acronym = "plat", BoldText="boldtext"};
            sut.RenderTop().ToString().Should().Contain("\"intranetTitle\":[{\"boldText\":\"boldtext\",\"href\":\"bar\",\"text\":\"foo\",\"acronym\":\"plat\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TopSecMenuFalseInTopWhenLeftMenuItems(Core sut)
        {
            sut.RenderTop().ToString().Should().Contain("\"topSecMenu\":false");
        }

        [Theory, AutoNSubstituteData]
        public void TopSecMenuTrueInTopWhenLeftMenuItems(Core sut)
        {
            sut.LeftMenuItems.Add(new MenuSection
            {
                Link = "foo",
                Name = "bar"
            });
            sut.RenderTop().ToString().Should().Contain("\"topSecMenu\":true");
        }

        [Theory, AutoNSubstituteData]
        public void CustomSearchRenders(Core sut)
        {
            sut.CustomSearch = new CustomSearch
            {
                Action = "#",
                Placeholder = "Custom Search Placeholder",
                Method = "get"
            };
            var result = sut.RenderTop().ToString();
            result.Should().Contain("\"customSearch\":[{\"action\":\"#\",\"placeholder\":\"Custom Search Placeholder\",\"method\":\"get\"}]");
        }
    }
}