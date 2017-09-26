using System.Collections.Generic;
using FluentAssertions;
using GoC.WebTemplate;
using GoC.WebTemplate.ConfigSections;
using GoC.WebTemplate.Proxies;
using NSubstitute;
using WebTemplateCore.JSONSerializationObjects;
using WebTemplateCore.Proxies;
using Xunit;

namespace CoreTest
{
    /// <summary>
    /// Tests that test the Configurations Object and how it integrates with the core object.
    /// </summary>
    public class ConfigurationCoreIntegrationTests
    {
        [Theory, AutoNSubstituteData]
        public void SubThemeSetProgrammticallyOverridesWebConfig(IDictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            var sut = new Core(fakeCurrentRequestProxy, fakeCacheProxy, new ConfigurationProxy(), environments);
            sut.WebTemplateSubTheme = "foobar";
            sut.RenderTop().ToString().Should().Contain("\"subTheme\":\"foobar\"");
        }
        [Theory, AutoNSubstituteData]
        public void SearchBoxShownByDefault(IDictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, fakeCacheProxy, new ConfigurationProxy(), environments);
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"search\":true");
        }

        [Theory, AutoNSubstituteData]
        public void LeavingSecureSiteWarningElementCapitilizationFix(IDictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            var sut = new Core(fakeCurrentRequestProxy,fakeCacheProxy, new ConfigurationProxy(), environments);
            sut.LeavingSecureSiteWarning.RedirectURL.Should().Be("foo");
        }

    }
}