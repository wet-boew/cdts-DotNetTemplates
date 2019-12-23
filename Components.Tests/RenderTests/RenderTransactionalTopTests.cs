using FluentAssertions;
using GoC.WebTemplate.Components.Entities;
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
      sut.Render.TransactionalTop().ToString().Should().Contain("\"topSecMenu\":true");
    }
       
    [Theory, AutoNSubstituteData]
    public void TopSecMenuFalseInTransactionalTopWhenLeftMenuItems(Model sut)
    {
      sut.Render.TransactionalTop().ToString().Should().Contain("\"topSecMenu\":false");
    }
        
    [Theory, AutoNSubstituteData]
    public void IntranetTitleShouldNotRenderWhenNullInTransactionalTop(Model sut)
    {
      sut.IntranetTitle = null;
      sut.Render.TransactionalTop().ToString().Should().NotContain("\"intranetTitle\":[null]");

    }
    [Theory, AutoNSubstituteData]
    public void IntranetTitleTransacationalTop(Model sut)
    {
        sut.IntranetTitle = new IntranetTitle { Text = "foo", Href = "bar", Acronym = "plat" };
        sut.Render.TransactionalTop().ToString().Should().Contain("\"intranetTitle\":[{\"href\":\"bar\",\"text\":\"foo\",\"acronym\":\"plat\"}]");
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
            var result = sut.Render.TransactionalTop().ToString();
            result.Should().Contain("\"customSearch\":[{\"action\":\"#\",\"placeholder\":\"Custom Search Placeholder\",\"method\":\"get\"}]");
        }


    }
}