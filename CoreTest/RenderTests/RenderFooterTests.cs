using System;
using FluentAssertions;
using GoC.WebTemplate.Components;
using Xunit;

namespace CoreTest.RenderTests
{
  public class RenderFooterTests
  {

    [Theory, AutoNSubstituteData]
    public void ContactLinksShoulNotRenderWhenNull(Core sut)
    {
      sut.ContactLink = null;
      sut.RenderFooter().ToString().Should().NotContain("\"contactLinks\"");
    }
    
    [Theory, AutoNSubstituteData]
    public void ContactLinksShoulNotRenderWhenEmpty(Core sut)
    {
      sut.ContactLink = new Link();
      sut.RenderFooter().ToString().Should().NotContain("\"contactLinks\"");
    }
    
    [Theory, AutoNSubstituteData]
    public void ContactLinksShouldNotBeEmptyWhenValueIsSet(Core sut)
    {
      sut.ContactLink = new Link("foo","bar");
      sut.RenderFooter().ToString().Should().NotContain("\"contactLinks\":[{}]")
        .And.Contain("\"contactLinks\":[{\"href\":\"foo\",\"text\":\"bar\"}]");

    }
       
    [Theory, AutoNSubstituteData]
    public void ShouldNotEncodeURL(Core sut)
    {
      sut.ContactLink = new Link("http://localhost:8080/foo.html",string.Empty);
      var htmlstring = sut.RenderFooter();
      htmlstring.ToString().Should().Contain("http://localhost:8080/foo.html");
    }
    [Theory, AutoNSubstituteData]
    public void ExceptionWhenCallingRenderFooterLinks(Core sut)
    {
      sut.ContactLink = null;
      Action execute = () => {
        var ignore = sut.RenderFooter();
      };
      execute.Should().NotThrow<NullReferenceException>();

    }
        
    [Theory, AutoNSubstituteData]
    public void HandleEmptyContactLinkList(Core sut)
    {
      sut.ContactLink = null;
      sut.RenderFooter().ToString().Should().NotContain("\"contactLinks\"");
    }
        
    [Theory, AutoNSubstituteData]
    public void HandleContactLinkValue(Core sut)
    {
      sut.ContactLink = new Link() { Href="http://testvalue" };
      sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"http://testvalue\"}]");
    }
  }
}