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
      sut.CurrentEnvironment.Name = "AKAMAI";
      sut.ContactLinks = new List<Link>();
      sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[]");
    } 
        
    [Theory, AutoNSubstituteData]
    public void ContactLinkRendered([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "AKAMAI"
        };
        environments[sut.Environment] = currentEnv;

        sut.ContactLinks = new List<Link>() { new Link() { Href = "http://testvalue" } };
        sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[{\"href\":\"http://testvalue\"}]");
    }

    [Theory, AutoNSubstituteData]
    public void ContactFooterLinkRendered([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "AKAMAI"
        };
        environments[sut.Environment] = currentEnv;

        sut.ContactLinks = new List<Link>() { new FooterLink() { Href = "http://testvalue", NewWindow = false } };
        sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[{\"newWindow\":false,\"href\":\"http://testvalue\"}]");
    }

    [Theory, AutoNSubstituteData]
    public void ContactLinkSetTextAKAMAI([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "AKAMAI"
        };
        environments[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory, AutoNSubstituteData]
    public void ContactLinkSetTextPRODSSL([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "PROD_SSL"
        };
        environments[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory, AutoNSubstituteData]
    public void ContactLinkSetTextESDCProd([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "ESDC_PROD"
        };
        sut.UseHTTPS = true;
        environments[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory, AutoNSubstituteData]
    public void MultipleContactLinksAKAMAI([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "AKAMAI"
        };
        environments[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory, AutoNSubstituteData]
    public void MultipleContactLinksPRODSSL([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "PROD_SSL"
        };
        environments[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() {Href = "TestLink1", Text = "Link1"}, new Link() { Href = "TestLink2", Text = "Link2" } };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>();
        }

    [Theory, AutoNSubstituteData]
    public void MultipleContactLinksESDCProd([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "ESDC_PROD"
        };
        sut.UseHTTPS = true;
        environments[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>();
        }

    [Theory, AutoNSubstituteData]
    public void PrivacyLinkNotRenderedWhenURLIsNull(Core sut)
    {
      sut.CurrentEnvironment.Name = "AKAMAI";  
      sut.PrivacyLinkURL = null;
      var json = sut.RenderAppFooter();
      json.ToString().Should().NotContain("privacyLink");
    }

    [Theory, AutoNSubstituteData]
    public void PrivacyLinkRenderedWhenURLIsProvided(Core sut)
    {
      sut.CurrentEnvironment.Name = "AKAMAI";  
      sut.PrivacyLinkURL = "http://foo.bar";
      var json = sut.RenderAppFooter();
      json.ToString().Should().Contain("privacyLink");
    }

    [Theory, AutoNSubstituteData]
    public void TermsLinkNotRenderedWhenURLIsNull(Core sut)
    {
      sut.CurrentEnvironment.Name = "AKAMAI";  
      sut.TermsConditionsLinkURL = null;
      var json = sut.RenderAppFooter();
      json.ToString().Should().NotContain("termsLink");
    }

    [Theory, AutoNSubstituteData]
    public void TermsLinkRenderedWhenURLIsProvided(Core sut)
    {
      sut.CurrentEnvironment.Name = "AKAMAI";
      sut.TermsConditionsLinkURL = "http://foo.bar";
      var json = sut.RenderAppFooter();
      json.ToString().Should().Contain("termsLink");
    }
        
  }
}