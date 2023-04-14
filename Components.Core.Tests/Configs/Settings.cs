using FluentAssertions;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Utils.Caching;
using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit;

namespace GoC.WebTemplate.Components.Core.Tests.Configs
{
    public class Settings
    {
        //TODO: re-implement tests using config data from a web.config or the like
        [Theory, AutoNSubstituteData]
        public void SearchBoxShownByDefault(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            //We want to use the appsettings.json to test this so we don't use autonsubstitute to test it.
            var sut = GetModelWithActualSettings(fileContentCacheProvider, cdtsCacheProvider, cdtsSRIHashesCacheProvider);
            sut.Settings.WebAnalytics.Active = false;
            var json = sut.Render.AppSetup();
            json.ToString().Should().Contain("\"search\":true");
        }

        [Theory, AutoNSubstituteData]
        public void LeavingSecureSiteWarningElementCapitilizationFix(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            //We want to use the appsettings.json to test this so we don't use autonsubstitute to test it.
            var sut = GetModelWithActualSettings(fileContentCacheProvider, cdtsCacheProvider, cdtsSRIHashesCacheProvider);
            sut.Settings.LeavingSecureSiteWarning.RedirectUrl.Should().Be("foo");
        }

        [Theory, AutoNSubstituteData]
        public void WebAnaliticsGetsActiveValueFromConfig(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = GetModelWithActualSettings(fileContentCacheProvider, cdtsCacheProvider, cdtsSRIHashesCacheProvider);
            sut.Settings.WebAnalytics.Active.Should().BeTrue();
        }

        private Model GetModelWithActualSettings(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            var configs = new GocWebTemplateConfigurationSection();

            var configBuilder =
                new ConfigurationBuilder() //force file directory because test run from the bin folder not the project root directory
                    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\appsettings.json"), optional: false, reloadOnChange: true)
                    .Build();

            configBuilder.GetSection("GoCWebTemplate").Bind(configs);

            var settings = new WebTemplateSettings(configs);

            return new Model(fileContentCacheProvider, settings, cdtsCacheProvider, cdtsSRIHashesCacheProvider);
        }
    }
}
