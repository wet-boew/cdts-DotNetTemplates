using System;
using System.Collections.Generic;
using FluentAssertions;
using GoC.WebTemplate;
using GoC.WebTemplate.Proxies;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ploeh.AutoFixture.Xunit2;
using WebTemplateCore.JSONSerializationObjects;
using Xunit;

namespace CoreTest
{
    public class DeserializeEnvironmentsTest
    {

        [Fact]
        public void Foo()
        {
            var env = JsonSerializationHelper.DeserializeEnvironments("CDTSEnvironments.json");
            env.Count.Should().Be(6);
        }
    }
    public class CDNPathGeneratorTests
    {

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestAppHTTPSGCWeb([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                CDN = "esdcnonprod",
                IsVersionRNCombined = false,
                IsThemeModifiable = true,
                IsSSLModifiable = true
            };

            sut.WebTemplateTheme = "GCWeb";
            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://s2tst-cdn-canada.sade-edap.prv/app/cls/wet/GCWeb/{sut.WebTemplateVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestAppHTTPGCIntranet([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                CDN = "esdcnonprod",
                IsVersionRNCombined = false,
                IsThemeModifiable = true,
                IsSSLModifiable = true
            };

            sut.WebTemplateTheme = "GCIntranet";
            sut.UseHTTPS = false;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://s2tst-cdn-canada.sade-edap.prv/app/cls/wet/GCIntranet/{sut.WebTemplateVersion}/cdts/compiled/");
        }
        [Theory, AutoNSubstituteData]

        public void ESDCSADETestRnHTTPSGCWeb([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                CDN = "esdcnonprod",
                IsVersionRNCombined = false,
                IsThemeModifiable = true,
                IsSSLModifiable = true
            };

            sut.WebTemplateTheme = "GCWeb";
            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be("https://s2tst-cdn-canada.sade-edap.prv/rn/cls/wet/GCWeb/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestRnHTTPGCIntranet([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                CDN = "esdcnonprod",
                IsVersionRNCombined = false,
                IsThemeModifiable = true,
                IsSSLModifiable = true
            };

            sut.WebTemplateTheme = "GCIntranet";
            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = false;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be("http://s2tst-cdn-canada.sade-edap.prv/rn/cls/wet/GCIntranet/cdts/compiled/");
        }



        [Theory, AutoNSubstituteData]

        public void ESDCProdTestRnHTTPS([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsThemeModifiable = false,
                IsSSLModifiable = true
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://templates.service.gc.ca/rn/cls/wet/GCIntranet/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestRnHTTP([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsThemeModifiable = false,
                IsSSLModifiable = true
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = false;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be("http://templates.service.gc.ca/rn/cls/wet/GCIntranet/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestAppHTTPS([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsThemeModifiable = false,
                IsSSLModifiable = true
            };

            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://templates.service.gc.ca/app/cls/wet/GCIntranet/{sut.WebTemplateVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestAppHTTP([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsThemeModifiable = false,
                IsSSLModifiable = true
            };

            sut.UseHTTPS = false;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://templates.service.gc.ca/app/cls/wet/GCIntranet/{sut.WebTemplateVersion}/cdts/compiled/");
        }
        [Theory, AutoNSubstituteData]

        public void ProdSSLUrlTestApp([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsThemeModifiable = false,
                IsSSLModifiable = false
            };

            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://ssl-templates.services.gc.ca/app/cls/wet/GCIntranet/{sut.WebTemplateVersion}/cdts/compiled/");
        }


        [Theory, AutoNSubstituteData]
        public void ProdSSLUrlTestRun([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsThemeModifiable = false,
                IsSSLModifiable = false
            };

            sut.WebTemplateVersion = string.Empty;

            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://ssl-templates.services.gc.ca/rn/cls/wet/GCIntranet/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]
        public void AkamaiURLTestRun([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {

            var currentEnv = new CDTSEnvironment
            {
                Path = "https://www.canada.ca/etc/designs/canada/cdts/GCWeb/{3}cdts/compiled/",
                CDN = "prod",
                IsVersionRNCombined = true,
                IsThemeModifiable = false,
                IsSSLModifiable = false
            };

            sut.WebTemplateVersion = string.Empty;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be("https://www.canada.ca/etc/designs/canada/cdts/GCWeb/rn/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData] public void AkamaiURLTestApp([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {

            var currentEnv = new CDTSEnvironment
            {
                Path = "https://www.canada.ca/etc/designs/canada/cdts/GCWeb/{3}cdts/compiled/",
                CDN = "prod",
                IsVersionRNCombined = true,
                IsThemeModifiable = false,
                IsSSLModifiable = false
            };

            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://www.canada.ca/etc/designs/canada/cdts/GCWeb/{sut.WebTemplateVersion}/cdts/compiled/");
        }

    }

    public class CDNPathValidationTests
    {
            
        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfThemeIsSetButEnvironmentDoesNotAllowIt([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];

            sut.WebTemplateTheme = "GCWeb";
            currentEnv.IsThemeModifiable = false;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void DoNotThrowExceptionIfThemeIsNotSetAndNotModifiable([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];

            sut.WebTemplateTheme = string.Empty;
            currentEnv.IsThemeModifiable = false;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldNotThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfThemeIsModifiableButNotSet([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];

            sut.WebTemplateTheme = string.Empty;
            currentEnv.IsThemeModifiable = true;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void DoNotThrowExceptionIfThemeIsSetAndModifiable([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            sut.WebTemplateTheme = "GCWeb";
            currentEnv.IsThemeModifiable = true;

            sut.UseHTTPS = null;
            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldNotThrow<InvalidOperationException>();
        }
        
        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfHTTPSIsNonModifiableButUseHTTPSIsSet([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.IsSSLModifiable = false;
            sut.UseHTTPS = true;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void DoNotThrowExceptionIfHTTPSIsNonModifiableAndUseHTTPSIsNull([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.IsSSLModifiable = false;
            sut.UseHTTPS = null;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldNotThrow<InvalidOperationException>();
        }

        }
    /// <summary>
    /// Tests that test the Core object in isolation.
    /// </summary>
    public class RenderTests 
    {

        [Theory, AutoNSubstituteData]
        public void DoNotRenderBreadCrumbsByDefault(Core sut)
        {
            sut.RenderTop().ToString().Should().NotContain("\"breadcrumbs\"");
        }

        [Theory, AutoNSubstituteData]
        public void RenderCustomSearchWhenSet(Core sut)
        {
            sut.CustomSearch="Foo";
            sut.RenderAppTop().ToString().Should().Contain("\"customSearch\":\"Foo\"");
        }

        [Theory, AutoNSubstituteData]
        public void LocalPathDoesNotRendersWhenNull([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            [Frozen]IConfigurationProxy proxy, Core sut)
        {
            environments[sut.Environment].LocalPath.ReturnsNull();
            sut.RenderRefTop().ToString().Should().NotContain("localPath");
        }

        [Theory, AutoNSubstituteData]
        public void LocalPathFormatsCorrectly([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            [Frozen]IConfigurationProxy proxy,
            Core sut)
        {
            environments[sut.Environment].LocalPath.Returns("{0}:{1}");
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
        public void WebSubThemeRenderedProperly(Core sut)
        {

            sut.RenderRefTop().ToString().Should().Contain($"\"subTheme\":\"{sut.WebTemplateSubTheme}\"");
        }
        /*
        //Current different types of environments
              "https://www.canada.ca/etc/designs/canada/cdts/GCWeb/{0}/cdts/compiled/",
              "https://ssl-templates.services.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
              "Path": "http{0}://templates.service.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
              "Path": "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
        */
        [Theory, AutoNSubstituteData]
        public void CdnEnvRenderedProperly(IDictionary<string, ICDTSEnvironment> environments, 
            CDTSEnvironment environment,
            IConfigurationProxy configurationProxy,
            ICurrentRequestProxy currentRequestProxy)
        {
            //Because of the wayh the core object is initialized in the constructor we'll occasionally  
            //have to create it ourselves.
            var sut = new Core(currentRequestProxy, configurationProxy, environments);
            
            environment.CDN = "prod";
            environments[sut.Environment] = environment;

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
