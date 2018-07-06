using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
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
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme = "gcweb",
            };

            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://s2tst-cdn-canada.sade-edap.prv/app/cls/wet/{sut.WebTemplateTheme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestAppHTTPGCIntranet([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme ="gcintranet",
            };

            sut.UseHTTPS = false;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://s2tst-cdn-canada.sade-edap.prv/app/cls/wet/{sut.WebTemplateTheme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }
        [Theory, AutoNSubstituteData]

        public void ESDCSADETestRnHTTPSGCWeb([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme ="gcweb"
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://s2tst-cdn-canada.sade-edap.prv/rn/cls/wet/{sut.WebTemplateTheme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestRnHTTPGCIntranet([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme = "GCIntranet"
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = false;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://s2tst-cdn-canada.sade-edap.prv/rn/cls/wet/{sut.WebTemplateTheme}/cdts/compiled/");
        }



        [Theory, AutoNSubstituteData]

        public void ESDCProdTestRnHTTPS([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://templates.service.gc.ca/rn/cls/wet/{sut.WebTemplateTheme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestRnHTTP([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = false;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://templates.service.gc.ca/rn/cls/wet/{sut.WebTemplateTheme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestAppHTTPS([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };

            sut.UseHTTPS = true;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://templates.service.gc.ca/app/cls/wet/{sut.WebTemplateTheme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestAppHTTP([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };

            sut.UseHTTPS = false;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://templates.service.gc.ca/app/cls/wet/{sut.WebTemplateTheme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }
        [Theory, AutoNSubstituteData]

        public void ProdSSLUrlTestApp([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = false
            };

            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://ssl-templates.services.gc.ca/app/cls/wet/{sut.WebTemplateTheme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }


        [Theory, AutoNSubstituteData]
        public void ProdSSLUrlTestRun([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = new CDTSEnvironment
            {
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = false
            };

            sut.WebTemplateVersion = string.Empty;

            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://ssl-templates.services.gc.ca/rn/cls/wet/{sut.WebTemplateTheme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]
        public void AkamaiURLTestRun([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {

            var currentEnv = new CDTSEnvironment
            {
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = true,
                IsEncryptionModifiable = false
            };

            sut.WebTemplateVersion = string.Empty;
            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://www.canada.ca/etc/designs/canada/cdts/{sut.WebTemplateTheme}/rn/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData] public void AkamaiURLTestApp([Frozen]IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {

            var currentEnv = new CDTSEnvironment
            {
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = true,
                IsEncryptionModifiable = false
            };

            environments[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://www.canada.ca/etc/designs/canada/cdts/{sut.WebTemplateTheme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }

    }
}