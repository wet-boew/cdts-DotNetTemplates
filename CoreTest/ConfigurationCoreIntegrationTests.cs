using FluentAssertions;
using GoC.WebTemplate;
using GoC.WebTemplate.Proxies;
using Xunit;

namespace CoreTest
{
    /// <summary>
    /// Tests that test the Configurations Object and how it integrates with the core object.
    /// </summary>
    public class ConfigurationCoreIntegrationTests
    {
        
        [Theory, AutoNSubstituteData]
        public void SiteMenuShownByDefault(ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy());
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"siteMenu\":true");
        }
    }
}