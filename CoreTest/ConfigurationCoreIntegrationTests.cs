using FluentAssertions;
using GoC.WebTemplate;
using GoC.WebTemplate.ConfigSections;
using GoC.WebTemplate.Proxies;
using NSubstitute;
using Xunit;

namespace CoreTest
{
    /// <summary>
    /// Tests that test the Configurations Object and how it integrates with the core object.
    /// </summary>
    public class ConfigurationCoreIntegrationTests
    {

        [Theory, AutoNSubstituteData]
        public void SearchBoxShownByDefault(ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy());
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"search\":true");
        }

        [Theory, AutoNSubstituteData]
        public void SiteMenuShownByDefault(ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy());
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"siteMenu\":true");
        }

        [Theory, AutoNSubstituteData]
        public void GlobalNavFalseByDefault(ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy());
            var json = sut.RenderAppFooter();
            json.ToString().Should().Contain("\"globalNav\":false");
        }


        [Theory, AutoNSubstituteDataAttribute]

        public void LeavingSecureSiteWarningElementCapitilizationFix(ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy());
            sut.LeavingSecureSiteWarning.RedirectURL.Should().Be("foo");
        }

        [Theory, AutoNSubstituteData]
        public void CDNUrlDefaultsToValueSetInConfig(ICurrentRequestProxy fakeRequestProxy)
        {
            //AKAMAI is selected as the environment in the  config file.
            var sut = new Core(fakeRequestProxy, new ConfigurationProxy());
            sut.CDNURL.Should().Be("{0}:{1}:{2}");
        }

        [Theory, AutoNSubstituteData]
        public void CDNPathShouldBeBasedOnEnvironment(ICurrentRequestProxy fakeRequestProxy)
        {
            //AKAMAI is selected as the environment in the  config file.
            var sut = new Core(fakeRequestProxy, new ConfigurationProxy());
            sut.Environment = Core.CDTSEnvironments.ESDCPROD.ToString();
            sut.CDNURL.Should().Be("{0}:{1}:{2}:{3}");
        }
    }
}