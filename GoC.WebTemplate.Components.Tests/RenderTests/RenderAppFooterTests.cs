using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils.Caching;
using System;
using System.Collections.Generic;
using Xunit;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderAppFooterTests
    {
        [Theory, AutoNSubstituteData]
        public void HandleEmptyContactLinkListAkamai([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "AKAMAI"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>();
            sut.Render.AppFooter().ToString().Should().Contain("\"contactLink\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void HandleEmptyContactLinkListPRODSSL([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "PROD_SSL"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>();
            sut.Render.AppFooter().ToString().Should().Contain("\"contactLink\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void HandleEmptyContactLinkListESDCProd([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "ESDC_PROD"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>();
            sut.Render.AppFooter().ToString().Should().Contain("\"contactLink\":[]");
        }
        [Theory, AutoNSubstituteData]
        public void ContactLinkRendered([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "AKAMAI",
                CanHaveContactLinkInAppTemplate = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.ContactLinks = new List<Link>() { new Link() { Href = "http://testvalue" } };
            sut.Render.AppFooter().ToString().Should().Contain("\"contactLink\":[{\"href\":\"http://testvalue\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void ContactFooterLinkRendered([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            //environment.CanHaveContactLinkInAppTemplate = true;
            var currentEnv = new CdtsEnvironment
            {
                Name = "AKAMAI",
                CanHaveContactLinkInAppTemplate = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.ContactLinks = new List<Link>() { new FooterLink() { Href = "http://testvalue", NewWindow = false } };
            sut.Render.AppFooter().ToString().Should().Contain("\"contactLink\":[{\"href\":\"http://testvalue\"}]");
        }

    [Theory, AutoNSubstituteData]
    public void ContactLinkSetTextAKAMAI([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
    {
        var currentEnv = new CdtsEnvironment
        {
            Name = "AKAMAI",
            CanHaveContactLinkInAppTemplate = true
        };
        new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
        Action act = () => sut.Render.AppFooter();
        act.Should().Throw<InvalidOperationException>().WithMessage("Unable to edit Contact Link text in this environment");
    }

        [Theory, AutoNSubstituteData]
        public void ContactLinkSetTextPRODSSL([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "PROD_SSL"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
            Action act = () => sut.Render.AppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("Please use a CustomFooter to add a contact link in this environment");
        }

        [Theory, AutoNSubstituteData]
        public void ContactLinkSetTextESDCProd([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "ESDC_PROD"
            };
            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
            Action act = () => sut.Render.AppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("Please use a CustomFooter to add a contact link in this environment");
        }

    [Theory, AutoNSubstituteData]
    public void MultipleContactLinksAKAMAI([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
    {
        var currentEnv = new CdtsEnvironment
        {
            Name = "AKAMAI",
            CanHaveContactLinkInAppTemplate = true
        };
        new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
        Action act = () => sut.Render.AppFooter();
        act.Should().Throw<InvalidOperationException>().WithMessage("Having multiple contact links not allowed in this environment");
    }

        [Theory, AutoNSubstituteData]
        public void MultipleContactLinksPRODSSL([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "PROD_SSL"
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            Action act = () => sut.Render.AppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("Please use a CustomFooter to add a contact link in this environment");
        }

        [Theory, AutoNSubstituteData]
        public void MultipleContactLinksESDCProd([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "ESDC_PROD"
            };
            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
            Action act = () => sut.Render.AppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("Please use a CustomFooter to add a contact link in this environment");
        }

        [Theory, AutoNSubstituteData]
        public void PrivacyLinkNotRenderedWhenURLIsNull(Model sut)
        {
            sut.PrivacyLink = null;
            var json = sut.Render.AppFooter();
            json.ToString().Should().NotContain("privacyLink");
        }

        [Theory, AutoNSubstituteData]
        public void PrivacyLinkRenderedWhenURLIsProvided(Model sut)
        {
            sut.PrivacyLink.Href = "http://foo.bar";
            var json = sut.Render.AppFooter();
            json.ToString().Should().Contain("privacyLink");
        }

        [Theory, AutoNSubstituteData]
        public void TermsLinkNotRenderedWhenURLIsNull(Model sut)
        {
            sut.TermsConditionsLink = null;
            var json = sut.Render.AppFooter();
            json.ToString().Should().NotContain("termsLink");
        }

        [Theory, AutoNSubstituteData]
        public void TermsLinkRenderedWhenURLIsProvided(Model sut)
        {
            sut.TermsConditionsLink.Href = "http://foo.bar";
            var json = sut.Render.AppFooter();
            json.ToString().Should().Contain("termsLink");
        }

        [Theory, AutoNSubstituteData]
        public void CustomFooterLinksCantBeUsedWithLimit([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "TEST_LIMIT_3",
                FooterSectionLimit = 3
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.CustomFooterLinks = new List<Link> { new FooterLink { Href = "href", Text = "text" } };

            Action act = () => sut.Render.AppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("The CustomFooterLinks cannot be used in this enviornment, please use the FooterSections");
        }
        
        [Theory, AutoNSubstituteData]
        public void CustomFooterLinksRenders(Model sut)
        {
            sut.CustomFooterLinks = new List<Link> { new FooterLink { Href = "href", Text = "text" } };
            var json = sut.Render.AppFooter();
            json.ToString().Should().Contain("\"footerSections\":[{\"href\":\"href\",\"text\":\"text\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void FooterSectionsCantBeUsedWithLimitZero([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "TEST_LIMIT_0",
                FooterSectionLimit = 0
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.FooterSections = GetTestableFooterSectionList();

            Action act = () => sut.Render.AppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("The FooterSections cannot be used in this enviornment, please use the CustomFooterLinks");
        }
        
        [Theory, AutoNSubstituteData]
        public void FooterSectionsThrowsTooManyLimit([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "TEST_LIMIT_1",
                FooterSectionLimit = 1
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.FooterSections = GetTestableFooterSectionList();

            Action act = () => sut.Render.AppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage($"The maximum FooterSections allowed for this environment is {currentEnv.FooterSectionLimit}");
        }

        [Theory, AutoNSubstituteData]
        public void FooterSectionsRendersMultiple([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Name = "TEST_LIMIT_3",
                FooterSectionLimit = 3
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;
            sut.FooterSections = GetTestableFooterSectionList();

            var json = sut.Render.AppFooter();
            json.ToString().Should().Contain("\"footerSections\":[{\"sectionName\":\"CustomFooterSectionName\",\"customFooterLinks\":[{");
            json.ToString().Should().Contain("\"sectionName\":\"CustomFooterSectionName2\",\"customFooterLinks\":[{");
        }

        private List<FooterSection> GetTestableFooterSectionList()
        {
            return new List<FooterSection>
            {
                new FooterSection
                {
                    CustomFooterLinks = new List<FooterLink>
                    {
                        new FooterLink { Href = "href", Text = "text" }
                    },
                    SectionName = "CustomFooterSectionName"
                },
                new FooterSection
                {
                    CustomFooterLinks = new List<FooterLink>
                    {
                        new FooterLink { Href = "href2", Text = "text2" }
                    },
                    SectionName = "CustomFooterSectionName2"
                }
            };
        }

    }
}