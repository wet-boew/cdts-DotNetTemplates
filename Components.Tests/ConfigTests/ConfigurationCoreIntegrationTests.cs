using FluentAssertions;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Utils.Caching;
using Xunit;

namespace GoC.WebTemplate.Components.Test.ConfigTests
{
    public class ConfigurationCoreIntegrationTests
    {
        [Theory, AutoNSubstituteData]
        public void SubThemeSetFromCDTSEnvironments(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, WebTemplateSettings templateSettings, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Model(fileContentCacheProvider, templateSettings, cdtsCacheProvider, cdtsSRIHashesCacheProvider);
            sut.CdtsEnvironment.SubTheme = "foobar";
            sut.Render.Setup().ToString().Should().Contain("\"subTheme\":\"foobar\"");
        }

    }
}