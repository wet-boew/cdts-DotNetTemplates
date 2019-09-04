using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Utils.Caching;
using Xunit;

namespace GoC.WebTemplate.Components.Test.ConfigTests
{
    public class CDNPathGeneratorTests
    {

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestAppHTTPSGCWeb([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme = "gcweb",
            };

            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://s2tst-cdn-canada.sade-edap.prv/app/cls/wet/{sut.CdtsEnvironment.Theme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestAppHTTPGCIntranet([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme ="gcintranet",
            };

            sut.UseHTTPS = false;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://s2tst-cdn-canada.sade-edap.prv/app/cls/wet/{sut.CdtsEnvironment.Theme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }
        [Theory, AutoNSubstituteData]

        public void ESDCSADETestRnHTTPSGCWeb([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme ="gcweb"
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://s2tst-cdn-canada.sade-edap.prv/rn/cls/wet/{sut.CdtsEnvironment.Theme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestRnHTTPGCIntranet([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme = "GCIntranet"
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = false;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://s2tst-cdn-canada.sade-edap.prv/rn/cls/wet/{sut.CdtsEnvironment.Theme}/cdts/compiled/");
        }



        [Theory, AutoNSubstituteData]

        public void ESDCProdTestRnHTTPS([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://templates.service.gc.ca/rn/cls/wet/{sut.CdtsEnvironment.Theme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestRnHTTP([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };

            sut.WebTemplateVersion = string.Empty;
            sut.UseHTTPS = false;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://templates.service.gc.ca/rn/cls/wet/{sut.CdtsEnvironment.Theme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestAppHTTPS([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };

            sut.UseHTTPS = true;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://templates.service.gc.ca/app/cls/wet/{sut.CdtsEnvironment.Theme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestAppHTTP([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };

            sut.UseHTTPS = false;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://templates.service.gc.ca/app/cls/wet/{sut.CdtsEnvironment.Theme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }
        [Theory, AutoNSubstituteData]

        public void ProdSSLUrlTestApp([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = false
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://ssl-templates.services.gc.ca/app/cls/wet/{sut.CdtsEnvironment.Theme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }


        [Theory, AutoNSubstituteData]
        public void ProdSSLUrlTestRun([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironment
            {
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = false
            };

            sut.WebTemplateVersion = string.Empty;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://ssl-templates.services.gc.ca/rn/cls/wet/{sut.CdtsEnvironment.Theme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]
        public void AkamaiURLTestRun([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {

            var currentEnv = new CdtsEnvironment
            {
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = true,
                IsEncryptionModifiable = false
            };

            sut.WebTemplateVersion = string.Empty;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://www.canada.ca/etc/designs/canada/cdts/{sut.CdtsEnvironment.Theme}/rn/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]
        public void AkamaiURLTestApp([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {

            var currentEnv = new CdtsEnvironment
            {
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                IsVersionRNCombined = true,
                IsEncryptionModifiable = false
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://www.canada.ca/etc/designs/canada/cdts/{sut.CdtsEnvironment.Theme}/{sut.WebTemplateVersion}/cdts/compiled/");
        }

    }
}