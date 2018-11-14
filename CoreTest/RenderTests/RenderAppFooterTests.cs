using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using System;
using System.Collections.Generic;
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
    public void HandleContactLinkValue([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "AKAMAI"
        };
        environments[sut.Environment] = currentEnv;

        sut.ContactLink = new Link() { Href="http://testvalue" };
        sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[{\"href\":\"http://testvalue\"}]");
    }

    [Theory, AutoNSubstituteData]
    public void HandleContactFooterLinkValue([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "AKAMAI"
        };
        environments[sut.Environment] = currentEnv;

        sut.ContactLink = new FooterLink() { Href = "http://testvalue", NewWindow = false };
        sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[{\"newWindow\":false,\"href\":\"http://testvalue\"}]");
    }

    [Theory, AutoNSubstituteData]
    public void HandleContactLinkSetTextAKAMAI([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "AKAMAI"
        };
        environments[sut.Environment] = currentEnv;
        sut.ContactLink = new Link() { Text = "LinkText" };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory, AutoNSubstituteData]
    public void HandleContactLinkSetTextOtherEnvironment([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "Name"
        };
        environments[sut.Environment] = currentEnv;
        sut.ContactLink = new Link() { Text = "LinkText" };
        Action act = () => sut.RenderAppFooter();
        act.Should().NotThrow();
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