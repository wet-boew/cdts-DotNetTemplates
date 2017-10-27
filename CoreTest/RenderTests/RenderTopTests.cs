using FluentAssertions;
using GoC.WebTemplate;
using Xunit;

namespace CoreTest.RenderTests
{
  public class RenderTopTests
  {
        
    [Theory, AutoNSubstituteData]
    public void IntranetTitleShouldNotRenderWhenNullInTop(Core sut)
    {
      sut.IntranetTitle = null;
      sut.RenderTop().ToString().Should().NotContain("\"intranetTitle\":[null]");
    }
        
    [Theory, AutoNSubstituteData]
    public void BreadCrumbEmptyAcronymShouldNotRender(Core sut)
    {
      sut.Breadcrumbs.Add(new Breadcrumb
      {
        Title="Foo.bar",
        Acronym=""
      });
      sut.RenderTop().ToString().Should().NotContain("\"acronym\":\"\"");
    }
        
    [Theory, AutoNSubstituteData]
    public void BreadCrumbEmptyHrefShouldNotRender(Core sut)
    {
      sut.Breadcrumbs.Add(new Breadcrumb
      {
        Title="Foo.bar",
        Href =""
      });
      sut.RenderTop().ToString().Should().NotContain("\"href\":\"\"");
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
    public void TopSecMenuFalseInTopWhenLeftMenuItems(Core sut)
    {
      sut.RenderTop().ToString().Should().Contain("\"topSecMenu\":false");
    }
        
    [Theory, AutoNSubstituteData]
    public void IntranetTitleTop(Core sut)
    {
      sut.IntranetTitle = new Link {Text = "foo", Href = "bar"};
      sut.RenderTop().ToString().Should().Contain("\"intranetTitle\":[{\"href\":\"bar\",\"text\":\"foo\"}]");
    }
        
        
    [Theory, AutoNSubstituteData]
    public void DoNotRenderBreadCrumbsByDefault(Core sut)
    {
      sut.RenderTop().ToString().Should().NotContain("\"breadcrumbs\"");
    }
        
        
       
  }
}