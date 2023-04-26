using GoC.WebTemplate.Components.Test;

using Xunit;

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

            Assert.Contains("v999_999_999", sut.CSSPath);
            Assert.DoesNotContain("integrity", sut.CSSPath);
            Assert.DoesNotContain("integrity", sut.Builder.BuildWetJsPathAttributes("en"));
        }

        [Theory, AutoNSubstituteData]
        public void TestHasSRIForExistingVersion(Model sut)
        {
            sut.Settings.SRIEnabled = true;
            sut.Settings.Version = "v4_0_47";
            sut.CdtsEnvironment.Theme = "gcweb";
            sut.CdtsEnvironment.Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/";

            Assert.Contains("v4_0_47", sut.CSSPath);
            Assert.Contains("integrity", sut.CSSPath);
            Assert.Contains("integrity", sut.Builder.BuildWetJsPathAttributes("en"));
        }

        [Theory, AutoNSubstituteData]
        public void TestNoSRIForGCIntranetRunVersion(Model sut)
        {
            sut.Settings.SRIEnabled = true;
            sut.Settings.Version = "";
            sut.CdtsEnvironment.Theme = "gcintranet";
            sut.CdtsEnvironment.Path = "https://cdts.service.canada.ca/{1}/cls/WET/{2}/{3}cdts/";

            Assert.Contains("rn", sut.CSSPath);
            Assert.DoesNotContain("integrity", sut.CSSPath);
            Assert.DoesNotContain("integrity", sut.Builder.BuildWetJsPathAttributes("en"));
        }

        [Theory, AutoNSubstituteData]
        public void TestNoSRIWhenDisabled(Model sut)
        {
            sut.Settings.SRIEnabled = false;

            Assert.DoesNotContain("integrity", sut.CSSPath);
            Assert.DoesNotContain("integrity", sut.Builder.BuildWetJsPathAttributes("en"));
        }
    }
}
