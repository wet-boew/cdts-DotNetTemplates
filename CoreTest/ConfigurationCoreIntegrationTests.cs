using System.Collections.Generic;
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
        public void SearchBoxShownByDefault(IDictionary<string, ICDTSEnvironment> environments,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy(), environments);
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"search\":true");
        }

        [Theory, AutoNSubstituteData]
        public void SiteMenuShownByDefault(IDictionary<string, ICDTSEnvironment> environments,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy(), environments);
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"siteMenu\":true");
        }

        [Theory, AutoNSubstituteData]
        public void GlobalNavFalseByDefault(IDictionary<string, ICDTSEnvironment> environments,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy(),environments);
            var json = sut.RenderAppFooter();
            json.ToString().Should().Contain("\"globalNav\":false");
        }


        [Theory, AutoNSubstituteData]

        public void LeavingSecureSiteWarningElementCapitilizationFix(IDictionary<string, ICDTSEnvironment> environments,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            var sut = new Core(fakeCurrentRequestProxy, new ConfigurationProxy(), environments);
            sut.LeavingSecureSiteWarning.RedirectURL.Should().Be("foo");
        }

    }
}