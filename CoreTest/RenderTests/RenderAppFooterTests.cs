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
        public void HandleEmptyContactLinkListAkamai(Core sut)
        {
            sut.CurrentEnvironment.Name = "AKAMAI";
            sut.ContactLinks = new List<Link>();
            sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void HandleEmptyContactLinkListPRODSSL(Core sut)
        {
            sut.CurrentEnvironment.Name = "PROD_SSL";
            sut.ContactLinks = new List<Link>();
            Action act = () => sut.RenderAppFooter();
            sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[]");
        }

        [Theory, AutoNSubstituteData]
        public void HandleEmptyContactLinkListESDCProd(Core sut)
        {
            sut.CurrentEnvironment.Name = "ESDC_PROD";
            sut.ContactLinks = new List<Link>();
            Action act = () => sut.RenderAppFooter();
            sut.RenderAppFooter().ToString().Should().Contain("\"contactLink\":[]");
        }
        [Theory, AutoNSubstituteData]
        public void ContactLinkRendered([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "AKAMAI",
                CanHaveContactLinkInAppTemplate = true
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
                Name = "AKAMAI",                
                CanHaveContactLinkInAppTemplate = true
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
            Name = "AKAMAI",
            CanHaveContactLinkInAppTemplate = true
        };
        environments[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" } };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>().WithMessage("Unable to edit Contact Link text in this environment");
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
            act.Should().Throw<InvalidOperationException>().WithMessage("Please use a CustomFooter to add a contact link in this environment");
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
            act.Should().Throw<InvalidOperationException>().WithMessage("Please use a CustomFooter to add a contact link in this environment");
        }

    [Theory, AutoNSubstituteData]
    public void MultipleContactLinksAKAMAI([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
    {
        var currentEnv = new CDTSEnvironment
        {
            Name = "AKAMAI",
            CanHaveContactLinkInAppTemplate = true
        };
        environments[sut.Environment] = currentEnv;
        sut.ContactLinks = new List<Link>() { new Link() { Href = "TestLink1", Text = "Link1" }, new Link() { Href = "TestLink2", Text = "Link2" } };
        Action act = () => sut.RenderAppFooter();
        act.Should().Throw<InvalidOperationException>().WithMessage("Having multiple contact links not allowed in this environment");
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
            Action act = () => sut.RenderAppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("Please use a CustomFooter to add a contact link in this environment");
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
            act.Should().Throw<InvalidOperationException>().WithMessage("Please use a CustomFooter to add a contact link in this environment");
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
        public void PrivacyLinkRenderedNewWindow(Core sut)
        {
            sut.PrivacyLinkURL = "http://foo.bar";
            sut.PrivacyLinkNewWindow = true;
            var json = sut.RenderAppFooter();
            json.ToString().Should().Contain("\"newWindow\":true");
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

        [Theory, AutoNSubstituteData]
        public void TermsLinkRendereNewWindow(Core sut)
        {
            sut.TermsConditionsLinkURL = "http://foo.bar";
            sut.TermsConditionsLinkNewWindow = true;
            var json = sut.RenderAppFooter();
            json.ToString().Should().Contain("\"newWindow\":true");
        }


        [Theory, AutoNSubstituteData]
        public void CustomFooterLinksCantBeUsedWithLimit([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "TEST_LIMIT_3",
                FooterSectionLimit = 3
            };
            environments[sut.Environment] = currentEnv;
            sut.CustomFooterLinks = new List<FooterLink> { new FooterLink { Href = "href", Text = "text" } };

            Action act = () => sut.RenderAppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("The CustomFooterLinks cannot be used in this enviornment, please use the FooterSections");
        }
        
        [Theory, AutoNSubstituteData]
        public void CustomFooterLinksRenders(Core sut)
        {
            sut.CustomFooterLinks = new List<FooterLink> { new FooterLink { Href = "href", Text = "text" } };
            var json = sut.RenderAppFooter();
            json.ToString().Should().Contain("\"footerSections\":[{\"newWindow\":false,\"href\":\"href\",\"text\":\"text\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void FooterSectionsCantBeUsedWithLimitZero([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "TEST_LIMIT_0",
                FooterSectionLimit = 0
            };
            environments[sut.Environment] = currentEnv;
            sut.FooterSections = GetTestableFooterSectionList();

            Action act = () => sut.RenderAppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage("The FooterSections cannot be used in this enviornment, please use the CustomFooterLinks");
        }
        
        [Theory, AutoNSubstituteData]
        public void FooterSectionsThrowsTooManyLimit([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "TEST_LIMIT_1",
                FooterSectionLimit = 1
            };
            environments[sut.Environment] = currentEnv;
            sut.FooterSections = GetTestableFooterSectionList();

            Action act = () => sut.RenderAppFooter();
            act.Should().Throw<InvalidOperationException>().WithMessage($"The maximum FooterSections allowed for this environment is {currentEnv.FooterSectionLimit}");
        }

        [Theory, AutoNSubstituteData]
        public void FooterSectionsRendersMultiple([Frozen]IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Name = "TEST_LIMIT_3",
                FooterSectionLimit = 3
            };
            environments[sut.Environment] = currentEnv;
            sut.FooterSections = GetTestableFooterSectionList();

            var json = sut.RenderAppFooter();
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