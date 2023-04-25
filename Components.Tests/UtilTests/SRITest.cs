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
            sut.CSSPath.Contains("v999_999_999");
            Assert.DoesNotContain("integrity", sut.CSSPath);
            Assert.DoesNotContain("integrity", sut.Builder.BuildWetJsPathAttributes("en"));
        }

        [Theory, AutoNSubstituteData]
        public void TestHasSRIForExistingVersion(Model sut)
        {
            sut.Settings.SRIEnabled = true;
            sut.Settings.Version = "v4_0_47";
            sut.CdtsEnvironment.Theme = "gcweb";

            sut.CSSPath.Contains("v4_0_47");
            Assert.Contains("integrity", sut.CSSPath);
            Assert.Contains("integrity", sut.Builder.BuildWetJsPathAttributes("en"));
        }

        [Theory, AutoNSubstituteData]
        public void TestNoSRIForGCIntranetRunVersion(Model sut)
        {
            sut.Settings.SRIEnabled = true;
            sut.Settings.Version = "";
            sut.CdtsEnvironment.Theme = "gcintranet";

            sut.CSSPath.Contains("v4_0_47");
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
