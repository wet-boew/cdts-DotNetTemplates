using GoC.WebTemplate.Components.Test;
using GoC.WebTemplate.Components.Utils;

using Xunit;

namespace GoC.WebTemplate.Components.Tests.UtilTests
{
    public class CSSPathTest
    {
        /// <summary>
        /// BuildCSSPath
        /// </summary>
        [Theory, AutoNSubstituteData]
        public void TestBasicGCWeb(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildCSSPath();

            result.EndsWith("/cdts-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestBasicGCIntranet(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildCSSPath();

            result.EndsWith("/cdts-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestBasicGCIntranetESDC(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";
            sut.CdtsEnvironment.SubTheme = "esdc";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildCSSPath();

            result.EndsWith("/cdts-esdc-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestBasicGCIntranetECCC(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";
            sut.CdtsEnvironment.SubTheme = "eccc";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildCSSPath();

            result.EndsWith("/cdts-eccc-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestBasicUnknownTheme(Model sut)
        {
            sut.CdtsEnvironment.Theme = "whatever";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildCSSPath();

            result.EndsWith("/cdts-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestBasicGCIntranetUnknownSubtheme(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";
            sut.CdtsEnvironment.Theme = "whatever";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildCSSPath();

            result.EndsWith("/cdts-styles.css");
        }

        /// <summary>
        /// BuildAppCSSPath
        /// </summary>
        [Theory, AutoNSubstituteData]
        public void TestAppGCWeb(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildAppCSSPath();

            result.EndsWith("/cdts-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestAppGCIntranet(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildCSSPath();

            result.EndsWith("/cdts-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestAppGCIntranetESDC(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";
            sut.CdtsEnvironment.SubTheme = "esdc";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildAppCSSPath();

            result.EndsWith("/cdts-esdc-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestAppGCIntranetECCC(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";
            sut.CdtsEnvironment.SubTheme = "eccc";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildAppCSSPath();

            result.EndsWith("/cdts-eccc-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestAppUnknownTheme(Model sut)
        {
            sut.CdtsEnvironment.Theme = "whatever";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildAppCSSPath();

            result.EndsWith("/cdts-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestAppGCIntranetUnknownSubtheme(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";
            sut.CdtsEnvironment.Theme = "whatever";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildAppCSSPath();

            result.EndsWith("/cdts-styles.css");
        }

        /// <summary>
        /// BuildSplashCSSPath
        /// </summary>
        [Theory, AutoNSubstituteData]
        public void TestSplashGCWeb(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildSplashCSSPath();

            result.EndsWith("/cdts-splash-styles.css");
        }

        [Theory, AutoNSubstituteData]
        public void TestSplashGCIntranet(Model sut)
        {
            sut.CdtsEnvironment.Theme = "gcintranet";

            ModelBuilder modelBuilder = new ModelBuilder(sut);
            var result = modelBuilder.BuildSplashCSSPath();

            result.EndsWith("/cdts-splash-styles.css");
        }
    }
}
