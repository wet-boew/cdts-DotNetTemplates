using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils.Caching;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderRefTopTests
    {
        [Theory, AutoNSubstituteData]
        public void LocalPathDoesNotRendersWhenNull([Frozen]ICdtsCacheProvider cdtsCacheProvider,
#pragma warning disable xUnit1026 // proxy needs to be frozen here even though it is not noticably used, it is being used on the creation though AutoNSubstituteData
      [Frozen]WebTemplateSettings settings,
#pragma warning restore xUnit1026
      Model sut)
        {
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment].LocalPath.ReturnsNull();
            sut.Render.RefTop(false).ToString().Should().NotContain("localPath");
        }

        [Theory, AutoNSubstituteData]
        public void LocalPathFormatsCorrectly([Frozen]ICdtsCacheProvider cdtsCacheProvider,
#pragma warning disable xUnit1026 // // proxy needs to be frozen here even though it is not noticably used, it is being used on the creation though AutoNSubstituteData
            [Frozen]WebTemplateSettings settings,
#pragma warning restore xUnit1026
            Model sut)
        {
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment].LocalPath.Returns("{0}:{1}");
            sut.Render.RefTop(false).ToString().Should().Contain($"\"localPath\":\"{sut.CdtsEnvironment.SubTheme}:{sut.Settings.Version}");
        }
        [Theory, AutoNSubstituteData]
        public void LocalPathRendersWhenNotNull([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment];
            currentEnv.LocalPath = "foo";
            sut.Render.RefTop(false).ToString().Should().Contain("\"localPath\":\"foo\"");
        }
        [Theory, AutoNSubstituteData]
        public void JQueryExternalRendersWhenLoadJQueryFromGoogleIsTrue(Model sut)
        {
            sut.Settings.LoadScriptsFromGoogle = true;

            sut.Render.RefTop(false).ToString().Should().Contain("\"jqueryEnv\":\"external\"");
        }
        [Theory, AutoNSubstituteData]
        public void JQueryExternalDoesNotRenderWhenLoadJQueryFromGoogleIsFalse(Model sut)
        {
            sut.Settings.LoadScriptsFromGoogle = false;

            sut.Render.RefTop(false).ToString().Should().NotContain("jqueryEnv");
        }
        [Theory, AutoNSubstituteData]
        public void WebSubThemeRenderedProperly(Model sut)
        {

            sut.Render.RefTop(false).ToString().Should().Contain($"\"subTheme\":\"{sut.CdtsEnvironment.SubTheme}\"");
        }

        [Theory]
        [InlineAutoNSubstituteData(true)]
        public void IsApplicationSetFromParam(bool isApplication, Model sut)
        {
            sut.Render.RefTop(isApplication).ToString().Should().Contain($"\"isApplication\":{isApplication.ToString().ToLower()}");
        }

        /*
            //Current different types of environments
                  "https://www.canada.ca/etc/designs/canada/cdts/GCWeb/{0}/cdts/compiled/",
                  "https://ssl-templates.services.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                  "Path": "http{0}://templates.service.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                  "Path": "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
            */
        [Theory, AutoNSubstituteData]
        public void CdnEnvRenderedProperly([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment].CDN = "prod";
            sut.Render.RefTop(false).ToString().Should().Contain("\"cdnEnv\":\"prod\"");
        }

        [Theory, AutoNSubstituteData]
        public void WebAnalyticsRenders(Model sut)
        {
            sut.Settings.WebAnalytics.Active = true;
            sut.Settings.WebAnalytics.Custom = null;
            sut.Settings.WebAnalytics.Environment = WebAnalytics.EnvironmentOption.staging;
            sut.Settings.WebAnalytics.Version = 1;
            var result = sut.Render.RefTop(false);
            result.ToString().Should().Contain("\"webAnalytics\":[{\"environment\":\"staging\",\"version\":1}]");
            result.ToString().Should().NotContain("custom");
        }

        [Theory, AutoNSubstituteData]
        public void WebAnalyticsRendersVersion3(Model sut)
        {
            sut.Settings.WebAnalytics.Active = true;
            sut.Settings.WebAnalytics.Custom = "launch-EN0cf6c2810a2b48f8a4c36502a1b09541.min.js";
            var result = sut.Render.RefTop(false);
            result.ToString().Should().Contain("\"custom\":\"launch-EN0cf6c2810a2b48f8a4c36502a1b09541.min.js\"}]");
        }
    }
}