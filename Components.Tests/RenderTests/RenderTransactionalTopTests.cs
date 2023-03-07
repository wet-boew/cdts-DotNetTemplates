using FluentAssertions;
using GoC.WebTemplate.Components.Entities;
using NSubstitute;
using System;
using Xunit;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderTransactionalTopTests
  {
    [Theory, AutoNSubstituteData]
    public void TopSecMenuTrueInTransactionalTopWhenLeftMenuItems(Model sut)
    {
      sut.LeftMenuItems.Add(new MenuSection
      {
        Href = "foo",
        Text = "bar"
      });
      sut.Render.TransactionalSetup().ToString().Should().Contain("\"topSecMenu\":true");
    }
       
    [Theory, AutoNSubstituteData]
    public void TopSecMenuFalseInTransactionalTopWhenLeftMenuItems(Model sut)
    {
      sut.Render.TransactionalSetup().ToString().Should().Contain("\"topSecMenu\":false");
    }

    [Theory, AutoNSubstituteData]
    public void TopSecSiteMenuFalseInTransactionalTop(Model sut)
    {
        sut.Render.TransactionalSetup().ToString().Should().Contain("\"siteMenu\":false");
    }

    [Theory, AutoNSubstituteData]
    public void TopSecSearchFalseInTransactionalTop(Model sut)
    {
        sut.Settings.ShowSearch = false;
        sut.Render.TransactionalSetup().ToString().Should().Contain("\"search\":false");
    }

    [Theory, AutoNSubstituteData]
    public void IntranetTitleShouldNotRenderWhenNullInTransactionalTop(Model sut)
    {
      sut.IntranetTitle = null;
      sut.Render.TransactionalSetup().ToString().Should().NotContain("\"intranetTitle\":[null]");

    }
    [Theory, AutoNSubstituteData]
    public void IntranetTitleTransacationalTop(Model sut)
    {
        sut.IntranetTitle = new IntranetTitle { Text = "foo", Href = "bar", Acronym = "plat" };
        sut.Render.TransactionalSetup().ToString().Should().Contain("\"intranetTitle\":[{\"href\":\"bar\",\"text\":\"foo\",\"acronym\":\"plat\"}]");
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
        var result = sut.Render.TransactionalSetup().ToString();
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
