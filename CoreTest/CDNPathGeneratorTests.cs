using System.Collections.Generic;
using FluentAssertions;
using GoC.WebTemplate;
using Ploeh.AutoFixture.Xunit2;
using WebTemplateCore.JSONSerializationObjects;
using Xunit;

namespace CoreTest
{
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
}