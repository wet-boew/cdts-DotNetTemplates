using System;
using FluentAssertions;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Utils.Caching;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderFooterTests
    {

        [Theory, AutoNSubstituteData]
        public void ContactLinksShoulNotRenderWhenNull(Model sut)
        {
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinksShoulNotRenderWhenEmpty(Model sut)
        {
            sut.ContactLinks = new List<Link>();
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinksHrefMustBeSpecifieds(Model sut)
        {
            sut.ContactLinks = new List<Link> { new Link() };
            Action act = () => sut.Render.Footer();
            act.Should().Throw<InvalidOperationException>().WithMessage("Href must be specified");
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinksShouldNotBeEmptyWhenValueIsSet(Model sut)
        {
            sut.ContactLinks = new List<Link> { new Link() { Href = "foo" } };
            sut.Render.Footer().ToString().Should().NotContain("\"contactLinks\":[{}]")
              .And.Contain("\"contactLinks\":[{\"href\":\"foo\"}]");

        }

        [Theory, AutoNSubstituteData]
        public void ContactLinkSetTextAKAMAI([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "AKAMAI"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Text = "LinkText" } };
            Action act = () => sut.Render.Footer();
            act.Should().Throw<InvalidOperationException>().WithMessage("Unable to edit Contact Link text in this environment");
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinkSetTextPRODSSL([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "PROD_SSL",
                CanHaveMultipleContactLinks = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinkSetTextESDCProd([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "ESDC_PROD",
                CanHaveMultipleContactLinks = true
            };
            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterContactLinkSetTextAKAMAI([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "AKAMAI"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Text = "LinkText" } };
            Action act = () => sut.Render.TransactionalFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("Unable to edit Contact Link text in this environment");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterContactLinkSetTextPRODSSL([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "PROD_SSL",
                CanHaveMultipleContactLinks = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterContactLinkSetTextESDCProd([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "ESDC_PROD",
                CanHaveMultipleContactLinks = true
            };
            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void MultipleContactLinksAKAMAI([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "AKAMAI"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            Action act = () => sut.Render.Footer();
            act.Should().Throw<InvalidOperationException>().WithMessage("Having multiple contact links not allowed in this environment");
        }


        [Theory, AutoNSubstituteData]
        public void MultipleContactLinksPRODSSL([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "PROD_SSL",
                CanHaveMultipleContactLinks = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"},{\"href\":\"TestLink2\",\"text\":\"Link2\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void MultipleContactLinksESDCProd([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "ESDC_PROD",
                CanHaveMultipleContactLinks = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"},{\"href\":\"TestLink2\",\"text\":\"Link2\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterMultipleContactLinksAKAMAI([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "AKAMAI"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            Action act = () => sut.Render.TransactionalFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("Having multiple contact links not allowed in this environment");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterMultipleContactLinksPRODSSL([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "PROD_SSL",
                CanHaveMultipleContactLinks = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            sut.Render.TransactionalFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"},{\"href\":\"TestLink2\",\"text\":\"Link2\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void TransactionalFooterMultipleContactLinksESDCProd([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "ESDC_PROD",
                CanHaveMultipleContactLinks = true
            };
            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            sut.Render.TransactionalFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"TestLink1\",\"text\":\"Link1\"},{\"href\":\"TestLink2\",\"text\":\"Link2\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void ShouldNotEncodeURL(Model sut)
        {
            sut.ContactLinks = new List<Link>() { new Link { Href = "http://localhost:8080/foo.html" } };
            var htmlstring = sut.Render.Footer();
            htmlstring.ToString().Should().Contain("http://localhost:8080/foo.html");
        }

        [Theory, AutoNSubstituteData]
        public void ExceptionWhenCallingRenderFooterLinks(Model sut)
        {
            Action execute = () =>
            {
                var ignore = sut.Render.Footer();
            };
            execute.Should().NotThrow<NullReferenceException>();

        }

        [Theory, AutoNSubstituteData]
        public void HandleEmptyContactLinkList(Model sut)
        {
            sut.ContactLinks = new List<Link>();
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void HandleContactLinkValue(Model sut)
        {
            sut.ContactLinks = new List<Link>() { new Link() { Href = "http://testvalue" } };
            sut.Render.Footer().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"http://testvalue\"}]");
        }
    }
}