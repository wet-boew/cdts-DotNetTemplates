using System;
using FluentAssertions;
using GoC.WebTemplate;
using Xunit;

namespace CoreTest
{

    /// <summary>
    /// Tests that test the Core object in isolation.
    /// </summary>
    public class RenderTests 
    {

        [Theory, AutoNSubstituteData]
        public void RenderCustomSearchWhenSet(Core sut)
        {
            sut.CustomSearch="Foo";
            sut.RenderAppTop().ToString().Should().Contain("\"customSearch\":\"Foo\"");
        }
        [Theory, AutoNSubstituteData]
        public void DoNotRenderCustomSearchByDefault(Core sut)
        {
            sut.RenderAppTop().ToString().Should().NotContain("\"customSearch\"");
        }

        [Theory, AutoNSubstituteData]
        public void ShouldNotEncodeURL(Core sut)
        {
            sut.ContactLinkURL = "http://localhost:8080/foo.html";
            var htmlstring = sut.RenderFooterLinks(false);
            htmlstring.ToString().Should().Contain("http://localhost:8080/foo.html");
        }

        [Theory, AutoNSubstituteData]
        public void CustomSearchIsRendered(Core sut)
        {
            sut.CustomSearch = "foo";
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"customSearch\":\"foo\"");

        }

        [Theory, AutoNSubstituteData]
        public void ExceptionWhenCallingRenderFooterLinks(Core sut)
        {
            sut.ContactLinkURL = null;
            Action execute = () => sut.RenderFooterLinks(false);
            execute.ShouldNotThrow<NullReferenceException>();

        }
        [Theory, AutoNSubstituteData]
        public void DefaultToNonObsoleteURLForContactLinks(Core sut)
        {
            sut.ContactLinks.Add(new Link {Href = "foo"});
            sut.ContactLinkURL = "bar";
            sut.RenderAppFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"bar\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void HandleEmptyContactLinkList(Core sut)
        {
            sut.ContactLinkURL = "bar";
            sut.RenderAppFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"bar\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void UseContactLinksListIfContactLinkURLIsNull(Core sut)
        {
            sut.ContactLinks.Add(new Link {Href = "foo"});
            sut.ContactLinkURL = default(string);
            sut.RenderAppFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"foo\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void RenderAppFooterMustNotCrashWithNullContactLinks(Core sut)
        {
            sut.ContactLinks = null;
            // ReSharper disable once MustUseReturnValue
            Action execute = () => sut.RenderAppFooter();
            execute.ShouldNotThrow<ArgumentNullException>();
        }

        [Theory, AutoNSubstituteData]
        public void RenderAppTopMustNotCrashWithNullBreadCrumbs(Core sut)
        {
            sut.Breadcrumbs = null;
            // ReSharper disable once MustUseReturnValue
            Action execute = () => sut.RenderAppTop();
            execute.ShouldNotThrow<ArgumentNullException>();
        }
        [Theory, AutoNSubstituteData]
        public void DoNotAddCanadaCaToTitlesIfItIsAlreadyThere(Core sut)
        {
            sut.HeaderTitle = "Foo - Canada.ca";
            sut.WebTemplateTheme = "GCWeb";
            sut.HeaderTitle.Should().Be("Foo - Canada.ca");
        }
        [Theory, AutoNSubstituteData]
        public void AddCanadaCaToAllTitlesOnPagesImplementingGCWebTheme(Core sut)
        {
            sut.HeaderTitle = "Foo";
            sut.WebTemplateTheme = "GCWeb";
            sut.HeaderTitle.Should().Be("Foo - Canada.ca");
        }

        [Theory, AutoNSubstituteData]
        public void AddCanadaCaToAllTitlesOnPagesWhenTitleIsNullImplementingGCWebTheme(Core sut)
        {
            sut.HeaderTitle = null;
            sut.WebTemplateTheme = "GCWeb";
            sut.HeaderTitle.Should().Be("- Canada.ca");
        }
        [Theory, AutoNSubstituteData]
        public void DontAddCanadaCaToAllTitlesOnPagesImplementingGCWebTheme(Core sut)
        {
            sut.HeaderTitle = "Foo";
            sut.WebTemplateTheme = "Intranet";
            sut.HeaderTitle.Should().Be("Foo");
        }
        [Theory, AutoNSubstituteData]
        public void SiteMenuPathShouldNotRenderWhenNull(Core sut)
        {
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("menuPath");
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

        [Theory, AutoNSubstituteData]
        public void SignInLinkNotRenderedWhenFlagisFalse(Core sut)
        {

            sut.ShowSignInLink = false;
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("signIn");
        }

        [Theory, AutoNSubstituteData]
        public void SignInLinkNotRenderedWhenLinkIsNull(Core sut)
        {

            sut.ShowSignInLink = true;
            sut.SignInLinkURL = null;
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("signIn");
        }

        [Theory, AutoNSubstituteData]
        public void SignOutLinkNotRenderedWhenFlagisFalse(Core sut)
        {

            sut.ShowSignOutLink = false;
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("signOut");
        }

        [Theory, AutoNSubstituteData]
        public void SignOutLinkNotRenderedWhenLinkIsNull(Core sut)
        {

            sut.ShowSignOutLink = true;
            sut.ShowSignInLink = false;
            sut.SignOutLinkURL = null;
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("signOut");
        }

        [Theory, AutoNSubstituteData]
        public void SignInAndSignOutLinkBothOn(Core sut)
        {
            sut.ShowSignOutLink = true;
            sut.ShowSignInLink = true;
            // ReSharper disable once MustUseReturnValue
            Action act = () =>  sut.RenderAppTop();
            act.ShouldThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void RenderLeftMenuTest( Core sut)
        {
            sut.LeftMenuItems.Add(new MenuSection("SectionName", "SectionLink", new[] {new Link("Href", "Text")}));

            var result = sut.RenderLeftMenu();

            result.ToString().Should().Be("sections: [ {sectionName: 'SectionName', sectionLink: 'SectionLink', menuLinks: [{href: 'Href', text: 'Text'},]},]");
        }

        [Theory, AutoNSubstituteData]
        public void RenderEmptyLeftMenu(Core sut)
        {
            var result = sut.RenderLeftMenu();
            result.ToString().Should().BeEmpty();
        }


    }
}
