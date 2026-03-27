using GoC.WebTemplate.Components.Test;

using NUnit.Framework;

namespace GoC.WebTemplate.Components.Tests.UtilTests
{
    public class SRITest
    {
        [Theory, AutoNSubstituteData]
        public void TestNoSRIForUnknownVersion(Model sut)
        {
            sut.Settings.SRIEnabled = true;
            sut.Settings.Version = "v999_999_999";
            sut.CdtsEnvironment.Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/";

            Assert.That(sut.CSSPath, Does.Contain("v999_999_999"));
            Assert.That(sut.CSSPath, Does.Not.Contain("integrity"));
            Assert.That(sut.Builder.BuildWetJsPathAttributes("en"), Does.Not.Contain("integrity"));
        }

        [Theory, AutoNSubstituteData]
        public void TestHasSRIForExistingVersion(Model sut)
        {
            sut.Settings.SRIEnabled = true;
            sut.Settings.Version = "v5_0_5";
            sut.CdtsEnvironment.Theme = "gcweb";
            sut.CdtsEnvironment.Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/";

            Assert.That(sut.CSSPath, Does.Contain("v5_0_5"));
            //Assert.Contains("integrity", sut.CSSPath); NOTE: For version v5_0_5, the CSS path does not have SRI
            Assert.That(sut.Builder.BuildWetJsPathAttributes("en"), Does.Contain("integrity"));
        }

        [Theory, AutoNSubstituteData]
        public void TestNoSRIForGCIntranetRunVersion(Model sut)
        {
            sut.Settings.SRIEnabled = true;
            sut.Settings.Version = "";
            sut.CdtsEnvironment.Theme = "gcintranet";
            sut.CdtsEnvironment.Path = "https://cdts.service.canada.ca/{1}/cls/WET/{2}/{3}cdts/";

            Assert.That(sut.CSSPath, Does.Contain("rn"));
            Assert.That(sut.CSSPath, Does.Not.Contain("integrity"));
            Assert.That(sut.Builder.BuildWetJsPathAttributes("en"), Does.Not.Contain("integrity"));
        }

        [Theory, AutoNSubstituteData]
        public void TestNoSRIWhenDisabled(Model sut)
        {
            sut.Settings.SRIEnabled = false;

            Assert.That(sut.CSSPath, Does.Not.Contain("integrity"));
            Assert.That(sut.Builder.BuildWetJsPathAttributes("en"), Does.Not.Contain("integrity"));
        }
    }
}
