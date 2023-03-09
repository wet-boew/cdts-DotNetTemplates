using GoC.WebTemplate.Components.Test;

using FluentAssertions;
using Xunit;

namespace GoC.WebTemplate.Components.Tests.RenderTests
{
    /// <summary>
    /// This contains tests for RenderSetup in relation with the top-most attributes,
    /// see the other RenderXXX classes for further tests.
    /// </summary>
    public class RenderSetupTests
    {
        [Theory, AutoNSubstituteData]
        public void TestRenderSetup(Model sut)
        {
            var json = sut.Render.Setup().ToString();
            //cdnEnv must be present
            json.Should().Contain("\"cdnEnv\":\"");
            //mode should NOT be present (we rely on CDTS's default in "normal" setup)
            json.Should().NotContain("\"mode\":\"");
        }

        /// <summary>
        /// Tests that apostrophes (') are escaped
        /// </summary>
        /// <param name="sut"></param>
        [Theory, AutoNSubstituteData]
        public void TestRenderSetupApos(Model sut)
        {
            sut.ScreenIdentifier = "This is an 'identifier'";
            sut.Render.Setup().ToString().Should().Contain("\"screenIdentifier\":\"This is an 'identifier'\"");
        }

        [Theory, AutoNSubstituteData]
        public void TestRenderAppSetup(Model sut)
        {
            var json = sut.Render.AppSetup().ToString(); ;
            //cdnEnv must be present
            json.Should().Contain("\"cdnEnv\":\"");
            //mode should be "app"
            json.Should().Contain("\"mode\":\"app\"");
        }

        [Theory, AutoNSubstituteData]
        public void TestRenderServerSetup(Model sut)
        {
            var json = sut.Render.ServerSetup().ToString(); ;
            //cdnEnv must be present
            json.Should().Contain("\"cdnEnv\":\"");
            //mode should be "server"
            json.Should().Contain("\"mode\":\"server\"");
        }

        [Theory, AutoNSubstituteData]
        public void TestRenderSplashSetup(Model sut)
        {
            var json = sut.Render.SplashSetup().ToString(); ;
            //cdnEnv must be present
            json.Should().Contain("\"cdnEnv\":\"");
            //mode should be "splash"
            json.Should().Contain("\"mode\":\"splash\"");
            //splash must be present
            json.Should().Contain("\"splash\":{");
        }
    }
}
