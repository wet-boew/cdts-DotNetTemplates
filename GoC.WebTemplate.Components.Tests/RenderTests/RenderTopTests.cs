﻿using FluentAssertions;
using GoC.WebTemplate.Components.Entities;
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
                Link = "foo",
                Name = "bar"
            });
            sut.Render.Top().ToString().Should().Contain("\"topSecMenu\":true");
        }
    }
}