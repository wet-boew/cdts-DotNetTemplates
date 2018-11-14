using System;
using FluentAssertions;
using GoC.WebTemplate.Components;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using GoC.WebTemplate.Components.JSONSerializationObjects;

namespace CoreTest.RenderTests
{
    public class RenderFooterTests
    {

        [Theory, AutoNSubstituteData]
        public void ContactLinksShoulNotRenderWhenNull(Core sut)
        {
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinksShoulNotRenderWhenEmpty(Core sut)
        {
            sut.ContactLinks = new List<Link>();
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinksHrefMustBeSpecifieds(Core sut)
        {
            sut.ContactLinks = new List<Link> { new Link() };
            Action act = () => sut.RenderFooter();
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinksShouldNotBeEmptyWhenValueIsSet(Core sut)
        {
            sut.ContactLinks = new List<Link> { new Link() { Href = "foo" } };
            sut.RenderFooter().ToString().Should().NotContain("\"contactLinks\":[{}]")
              .And.Contain("\"contactLinks\":[{\"href\":\"foo\"}]");

        }

        [Theory, AutoNSubstituteData]
        public void ContactLinkSetTextAKAMAI([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "AKAMAI"
            };
            environments[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Text = "LinkText" } };
            Action act = () => sut.RenderFooter();
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
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"}]");
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
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterContactLinkSetTextAKAMAI([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "AKAMAI"
            };
            environments[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Text = "LinkText" } };
            Action act = () => sut.RenderTransactionalFooter();
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterContactLinkSetTextPRODSSL([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "PROD_SSL"
            };
            environments[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterContactLinkSetTextESDCProd([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "ESDC_PROD"
            };
            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"}]");
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
            Action act = () => sut.RenderFooter();
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
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"},{\"href\":\"TestLink2\",\"text\":\"Link2\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void MultipleContactLinksESDCProd([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "ESDC_PROD"
            };
            environments[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"},{\"href\":\"TestLink2\",\"text\":\"Link2\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterMultipleContactLinksAKAMAI([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "AKAMAI"
            };
            environments[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            Action act = () => sut.RenderTransactionalFooter();
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterMultipleContactLinksPRODSSL([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "PROD_SSL"
            };
            environments[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            sut.RenderTransactionalFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"},{\"href\":\"TestLink2\",\"text\":\"Link2\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterMultipleContactLinksESDCProd([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "ESDC_PROD"
            };
            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            sut.RenderTransactionalFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"},{\"href\":\"TestLink2\",\"text\":\"Link2\"}]");
        }


        [Theory, AutoNSubstituteData]
        public void ShouldNotEncodeURL(Core sut)
        {
            sut.ContactLinks = new List<Link>() { new Link("http://localhost:8080/foo.html", string.Empty) };
            var htmlstring = sut.RenderFooter();
            htmlstring.ToString().Should().Contain("http://localhost:8080/foo.html");
        }
        [Theory, AutoNSubstituteData]
        public void ExceptionWhenCallingRenderFooterLinks(Core sut)
        {
            Action execute = () =>
            {
                var ignore = sut.RenderFooter();
            };
            execute.Should().NotThrow<NullReferenceException>();

        }

        [Theory, AutoNSubstituteData]
        public void HandleEmptyContactLinkList(Core sut)
        {
            sut.ContactLinks = new List<Link>();
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void HandleContactLinkValue(Core sut)
        {
            sut.ContactLinks = new List<Link>() { new Link() { Href = "http://testvalue" } };
            sut.RenderFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"http://testvalue\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void RenderTransactionalFooter(Core sut)
        {
            sut.ContactLinks = new List<Link>() { new Link
            {
                Href = "www.fakehref.com",
                Acronym = "accrrrnm"
            } };
            sut.PrivacyLinkURL = "dummyprivacylinkurl";
            sut.TermsConditionsLinkURL = "thisIsMyFunTemrsLink";

            var result = sut.RenderTransactionalFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"subTheme\":\"\",\"showFooter\":false,\"contactLinks\":[{\"href\":\"www.fakehref.com\",\"acronym\":\"accrrrnm\"}],\"privacyLink\":\"dummyprivacylinkurl\",\"termsLink\":\"thisIsMyFunTemrsLink\",\"showFeatures\":false}");
        }
    }
}