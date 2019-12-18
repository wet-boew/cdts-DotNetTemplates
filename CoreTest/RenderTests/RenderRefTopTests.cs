using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.Components.Proxies;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace CoreTest.RenderTests
{
    public class RenderRefTopTests
    {
        [Theory, AutoNSubstituteData]
        public void LocalPathDoesNotRendersWhenNull([Frozen]IDictionary<string, ICDTSEnvironment> environments,
#pragma warning disable xUnit1026 // proxy needs to be frozen here even though it is not noticably used, it is being used on the creation though AutoNSubstituteData
      [Frozen]IConfigurationProxy proxy,
#pragma warning restore xUnit1026
      Core sut)
        {
            environments[sut.Environment].LocalPath.ReturnsNull();
            sut.RenderRefTop(false).ToString().Should().NotContain("localPath");
        }

        [Theory, AutoNSubstituteData]
        public void LocalPathFormatsCorrectly([Frozen]IDictionary<string, ICDTSEnvironment> environments,
#pragma warning disable xUnit1026 // // proxy needs to be frozen here even though it is not noticably used, it is being used on the creation though AutoNSubstituteData
      [Frozen]IConfigurationProxy proxy,
#pragma warning restore xUnit1026
      Core sut)
        {
            environments[sut.Environment].LocalPath.Returns("{0}:{1}");
            sut.RenderRefTop(false).ToString().Should().Contain($"\"localPath\":\"{sut.WebTemplateTheme}:{sut.WebTemplateVersion}");
        }
        [Theory, AutoNSubstituteData]
        public void LocalPathRendersWhenNotNull([Frozen] IDictionary<string, ICDTSEnvironment> environments, Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.LocalPath = "foo";
            sut.RenderRefTop(false).ToString().Should().Contain("\"localPath\":\"foo\"");
        }
        [Theory, AutoNSubstituteData]
        public void JQueryExternalRendersWhenLoadJQueryFromGoogleIsTrue(Core sut)
        {
            sut.LoadJQueryFromGoogle = true;

            sut.RenderRefTop(false).ToString().Should().Contain("\"jqueryEnv\":\"external\"");
        }
        [Theory, AutoNSubstituteData]
        public void JQueryExternalDoesNotRenderWhenLoadJQueryFromGoogleIsFalse(Core sut)
        {
            sut.LoadJQueryFromGoogle = false;

            sut.RenderRefTop(false).ToString().Should().NotContain("jqueryEnv");
        }
        [Theory, AutoNSubstituteData]
        public void WebSubThemeRenderedProperly(Core sut)
        {

            sut.RenderRefTop(false).ToString().Should().Contain($"\"subTheme\":\"{sut.WebTemplateSubTheme}\"");
        }

        [Theory]
        [InlineAutoNSubstituteData(true)]
        [InlineAutoNSubstituteData(false)]
        public void IsApplicationSetFromParam(bool isApplication, Core sut)
        {
            sut.RenderRefTop(isApplication).ToString().Should().Contain($"\"isApplication\":{isApplication.ToString().ToLower()}");
        }

        /*
            //Current different types of environments
                  "https://www.canada.ca/etc/designs/canada/cdts/GCWeb/{0}/cdts/compiled/",
                  "https://ssl-templates.services.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                  "Path": "http{0}://templates.service.gc.ca/{1}/cls/wet/GCIntranet/{3}cdts/compiled/",
                  "Path": "http{0}://s2tst-cdn-canada.sade-edap.prv/{1}/cls/wet/{2}/{3}cdts/compiled/",
            */
        [Theory, AutoNSubstituteData]
        public void CdnEnvRenderedProperly([Frozen]IDictionary<string, ICDTSEnvironment> environments,
          Core sut)
        {
            environments[sut.Environment].CDN = "prod";
            sut.RenderRefTop(false).ToString().Should().Contain("\"cdnEnv\":\"prod\"");
        }
        
        [Theory, AutoNSubstituteData]
        public void WebAnaliticsRenders(Core sut)
        {
            sut.WebAnalytics.Active = true;
            sut.WebAnalytics.Environment = "staging";
            sut.WebAnalytics.Version = 1;
            var result = sut.RenderRefTop(false);
            result.ToString().Should().Contain("\"webAnalytics\":[{\"environment\":\"staging\",\"version\":1}]");
        }

    }
}