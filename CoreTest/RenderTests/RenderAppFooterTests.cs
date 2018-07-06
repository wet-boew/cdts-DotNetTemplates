using FluentAssertions;
using GoC.WebTemplate.Components;
using Xunit;

namespace CoreTest.RenderTests
{
  public class RenderAppFooterTests
  {
    [Theory, AutoNSubstituteData]
    public void HandleEmptyContactLinkList(Core sut)
    {
      sut.ContactLink = null;
      sut.RenderAppFooter().ToString().Should().NotContain("\"contactLink\":");
    } 
        
    [Theory, AutoNSubstituteData]
    public void HandleContactLinkValue(Core sut)
    {
      sut.ContactLink = new Link() { Href="http://testvalue" };
      sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":\"http://testvalue\"");
    }
        
    [Theory, AutoNSubstituteData]
    public void PrivacyLinkNotRenderedWhenURLIsNull(Core sut)
    {
      sut.PrivacyLinkURL = null;
      var json = sut.RenderAppFooter();
      json.ToString().Should().NotContain("privacyLink");
    }

    [Theory, AutoNSubstituteData]
    public void PrivacyLinkRenderedWhenURLIsProvided(Core sut)
    {
      sut.PrivacyLinkURL = "http://foo.bar";
      var json = sut.RenderAppFooter();
      json.ToString().Should().Contain("privacyLink");
    }

    [Theory, AutoNSubstituteData]
    public void TermsLinkNotRenderedWhenURLIsNull(Core sut)
    {
      sut.TermsConditionsLinkURL = null;
      var json = sut.RenderAppFooter();
      json.ToString().Should().NotContain("termsLink");
    }

    [Theory, AutoNSubstituteData]
    public void TermsLinkRenderedWhenURLIsProvided(Core sut)
    {
      sut.TermsConditionsLinkURL = "http://foo.bar";
      var json = sut.RenderAppFooter();
      json.ToString().Should().Contain("termsLink");
    }
        
  }
}