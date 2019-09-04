using FluentAssertions;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Utils.Caching;
using System.Configuration;
using Xunit;

namespace GoC.WebTemplate.Components.Framework.Tests.Configs
{
    public class Settings
    {
        //TODO: re-implement tests using config data from a web.config or the like
        [Theory, AutoNSubstituteData]
        public void SearchBoxShownByDefault(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var settings = new WebTemplateSettings(ConfigurationManager.GetSection("GoC.WebTemplate") as GocWebTemplateConfigurationSection);
            var sut = new Model(fileContentCacheProvider, settings, cdtsCacheProvider);
            var json = sut.Render.AppTop();
            json.ToString().Should().Contain("\"search\":true");
        }

        [Theory, AutoNSubstituteData]
        public void LeavingSecureSiteWarningElementCapitilizationFix(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Model(fileContentCacheProvider, new WebTemplateSettings(ConfigurationManager.GetSection("GoC.WebTemplate") as GocWebTemplateConfigurationSection), cdtsCacheProvider);
            sut.LeavingSecureSiteWarning.RedirectURL.Should().Be("foo");
        }
    }
}
