using System;
using System.Linq;
using FluentAssertions;
using GoC.WebTemplate;
using GoC.WebTemplate.ConfigSections;
using GoC.WebTemplate.Proxies;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ploeh.AutoFixture.Xunit2;
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
        public void LocalPathDoesNotRendersWhenNull([Frozen]IConfigurationProxy proxy, Core sut)
        {
            proxy.CDTSEnvironments[proxy.Environment].LocalPath.ReturnsNull();
            sut.RenderRefTop().ToString().Should().NotContain("localPath");
        }

        [Theory, AutoNSubstituteData]
        public void LocalPathFormatsCorrectly([Frozen]IConfigurationProxy proxy, Core sut)
        {
            proxy.CDTSEnvironments[Arg.Any<string>()].LocalPath.Returns("{0}:{1}");
            sut.RenderRefTop().ToString().Should().Contain($"\"localPath\":\"{sut.WebTemplateTheme}:{sut.WebTemplateVersion}");
        }


        public void LocalPathRendersWhenNotNull(Core sut)
        {
            sut.RenderRefTop().ToString().Should().Contain("localPath");
        }
        [Theory, AutoNSubstituteData]
        public void JQueryExternalRendersWhenLoadJQueryFromGoogleIsTrue(Core sut)
        {
            sut.LoadJQueryFromGoogle = true;

            sut.RenderRefTop().ToString().Should().Contain("\"jqueryEnv\":\"external\"");
        }
        [Theory, AutoNSubstituteData]
        public void JQueryExternalDoesNotRenderWhenLoadJQueryFromGoogleIsFalse(Core sut)
        {
            sut.LoadJQueryFromGoogle = false;

            sut.RenderRefTop().ToString().Should().NotContain("jqueryEnv");
        }

        [Theory, AutoNSubstituteData]
        public void WebSubThemeRenderedProperly(IConfigurationProxy configurationProxy,
            ICurrentRequestProxy currentRequestProxy)
        {

            configurationProxy.SubTheme.Returns("foo");

            //Because of the wayh the core object is initialized in the constructor we'll occasionally  
            //have to create it ourselves.
            var sut = new Core(currentRequestProxy, configurationProxy);

            sut.RenderRefTop().ToString().Should().Contain("\"subTheme\":\"foo\"");
        }
        [Theory, AutoNSubstituteData]
        public void CdnEnvRenderedProperly(ICDTSEnvironmentElementProxy elementProxy,
            IConfigurationProxy configurationProxy,
            ICurrentRequestProxy currentRequestProxy)
        {

            elementProxy.Env.Returns("prod");
            configurationProxy.CDTSEnvironments[Arg.Any<string>()].Returns(elementProxy);
            //Because of the wayh the core object is initialized in the constructor we'll occasionally  
            //have to create it ourselves.
            var sut = new Core(currentRequestProxy, configurationProxy);
            sut.RenderRefTop().ToString().Should().Contain("\"cdnEnv\":\"prod\"");
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
        public void HandleEmptyContactLinkList(Core sut)
        {
            sut.ContactLinkURL = "bar";
            sut.RenderAppFooter().ToString().Should().Contain("\"contactLinks\":[{\"href\":\"bar\"}]");
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
