using FluentAssertions;
using GoC.WebTemplate.Components;
using System;
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
      sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[{\"href\":\"http://testvalue\"}]");
    }

    [Theory, AutoNSubstituteData]
    public void HandleContactFooterLinkValue(Core sut)
    {
        sut.ContactLink = new FooterLink() { Href = "http://testvalue", NewWindow = false };
        sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[{\"newWindow\":false,\"href\":\"http://testvalue\"}]");
    }

    [Theory, AutoNSubstituteData]
    public void HandleContactLinkSetText(Core sut)
    {
        sut.ContactLink = new Link() { Text = "LinkText" };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory, AutoNSubstituteData]
    public void PrivacyLinkNotRenderedWhenURLIsNull(Core sut)
    {
      sut.PrivacyLinkURL = null;
      sut.ContactLink.Text = null;
      var json = sut.RenderAppFooter();
      json.ToString().Should().NotContain("privacyLink");
    }

    [Theory, AutoNSubstituteData]
    public void PrivacyLinkRenderedWhenURLIsProvided(Core sut)
    {
      sut.PrivacyLinkURL = "http://foo.bar";
      sut.ContactLink.Text = null;
      var json = sut.RenderAppFooter();
      json.ToString().Should().Contain("privacyLink");
    }

    [Theory, AutoNSubstituteData]
    public void TermsLinkNotRenderedWhenURLIsNull(Core sut)
    {
      sut.TermsConditionsLinkURL = null;
      sut.ContactLink.Text = null;
      var json = sut.RenderAppFooter();
      json.ToString().Should().NotContain("termsLink");
    }

    [Theory, AutoNSubstituteData]
    public void TermsLinkRenderedWhenURLIsProvided(Core sut)
    {
      sut.TermsConditionsLinkURL = "http://foo.bar";
      sut.ContactLink.Text = null;
      var json = sut.RenderAppFooter();
      json.ToString().Should().Contain("termsLink");
    }
        
  }
}