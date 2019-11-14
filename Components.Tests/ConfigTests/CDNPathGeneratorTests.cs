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
            var expectedVersion = "v1";
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = expectedVersion;
            sut.Settings.UseHttps = true;
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme = expectedEnvironmentTheme,
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://s2tst-cdn-canada.sade-edap.prv/app/cls/wet/{expectedEnvironmentTheme}/{expectedVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestAppHTTPGCIntranet([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedVersion = "v1";
            var expectedEnvironmentTheme = "gcintranet";

            sut.Settings.Version = expectedVersion;
            sut.Settings.UseHttps = false;
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme = expectedEnvironmentTheme
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://s2tst-cdn-canada.sade-edap.prv/app/cls/wet/{expectedEnvironmentTheme}/{expectedVersion}/cdts/compiled/");
        }
        [Theory, AutoNSubstituteData]

        public void ESDCSADETestRnHTTPSGCWeb([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = string.Empty;
            sut.Settings.UseHttps = true;
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme = expectedEnvironmentTheme
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://s2tst-cdn-canada.sade-edap.prv/rn/cls/wet/{expectedEnvironmentTheme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCSADETestRnHTTPGCIntranet([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedEnvironmentTheme = "GCIntranet";

            sut.Settings.Version = string.Empty;
            sut.Settings.UseHttps = false;
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true,
                Theme = expectedEnvironmentTheme
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://s2tst-cdn-canada.sade-edap.prv/rn/cls/wet/{expectedEnvironmentTheme}/cdts/compiled/");
        }



        [Theory, AutoNSubstituteData]

        public void ESDCProdTestRnHTTPS([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = string.Empty;
            sut.Settings.UseHttps = true;
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = expectedEnvironmentTheme,
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://templates.service.gc.ca/rn/cls/wet/{expectedEnvironmentTheme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestRnHTTP([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = string.Empty;
            sut.Settings.UseHttps = false;
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = expectedEnvironmentTheme,
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://templates.service.gc.ca/rn/cls/wet/{expectedEnvironmentTheme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestAppHTTPS([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedVersion = "v1";
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = expectedVersion;
            sut.Settings.UseHttps = true;
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = expectedEnvironmentTheme,
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://templates.service.gc.ca/app/cls/wet/{expectedEnvironmentTheme}/{expectedVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]

        public void ESDCProdTestAppHTTP([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedVersion = "v1";
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = expectedVersion;
            sut.Settings.UseHttps = false;
            var currentEnv = new CdtsEnvironment
            {
                Path = "http{0}://templates.service.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = expectedEnvironmentTheme,
                IsVersionRNCombined = false,
                IsEncryptionModifiable = true
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"http://templates.service.gc.ca/app/cls/wet/{expectedEnvironmentTheme}/{expectedVersion}/cdts/compiled/");
        }
        [Theory, AutoNSubstituteData]

        public void ProdSSLUrlTestApp([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedVersion = "v1";
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = expectedVersion;
            var currentEnv = new CdtsEnvironment
            {
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = expectedEnvironmentTheme,
                IsVersionRNCombined = false,
                IsEncryptionModifiable = false
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://ssl-templates.services.gc.ca/app/cls/wet/{expectedEnvironmentTheme}/{expectedVersion}/cdts/compiled/");
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

            sut.Settings.Version = string.Empty;
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://ssl-templates.services.gc.ca/rn/cls/wet/{sut.CdtsEnvironment.Theme}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]
        public void AkamaiURLTestRun([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var expectedVersion = "rn";
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = string.Empty;
            var currentEnv = new CdtsEnvironment
            {
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
                Theme = expectedEnvironmentTheme,
                IsVersionRNCombined = true,
                IsEncryptionModifiable = false
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = currentEnv;

            sut.CDNPath.Should()
                .Be($"https://www.canada.ca/etc/designs/canada/cdts/{expectedEnvironmentTheme}/{expectedVersion}/cdts/compiled/");
        }

        [Theory, AutoNSubstituteData]
        public void AkamaiURLTestApp([Frozen]ICdtsCacheProvider cdtsCacheProvider, ICdtsEnvironment env, Model sut)
        {
            var expectedVersion = "v1";
            var expectedEnvironmentTheme = "gcweb";

            sut.Settings.Version = expectedVersion;
            env = new CdtsEnvironment
            {
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
                Theme = expectedEnvironmentTheme,
                IsVersionRNCombined = true,
                IsEncryptionModifiable = false
            };
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment] = env;

            sut.CDNPath.Should()
                .Be($"https://www.canada.ca/etc/designs/canada/cdts/{expectedEnvironmentTheme}/{expectedVersion}/cdts/compiled/");
        }

    }
}