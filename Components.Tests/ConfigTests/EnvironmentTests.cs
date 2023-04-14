using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components.Configs.Cdts;
using GoC.WebTemplate.Components.Utils;
using GoC.WebTemplate.Components.Utils.Caching;
using Xunit;

namespace GoC.WebTemplate.Components.Test.ConfigTests
{
    public class EnvironmentTests
    {

        [Theory, AutoNSubstituteData]
        public void FileHasElevenEnvironments(ICdtsCacheProvider cdtsCacheProvider, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            var result = new CdtsEnvironmentCache(cdtsCacheProvider, cdtsSRIHashesCacheProvider).DeserializeEnvironments();
            result.Count.Should().Be(11);
        }

        [Theory, AutoNSubstituteData]
        public void LoadCDTSEnvironments(ICdtsCacheProvider cdtsCacheProvider, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            var result = new CdtsEnvironmentCache(cdtsCacheProvider, cdtsSRIHashesCacheProvider).DeserializeEnvironments();
            result.Should().NotBeNull();
        }

        [Theory, AutoNSubstituteData]
        public void SubThemeMustChangeWhenEnvironmentChanges([Frozen]ICdtsCacheProvider cdtsCacheProvider, [Frozen] ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider, Model sut)
        {
            var environments = new CdtsEnvironmentCache(cdtsCacheProvider, cdtsSRIHashesCacheProvider).GetContent();
            sut.Settings.Environment = "ITEM2";
            var subTheme = "foo";
            environments["ITEM1"].SubTheme = "IncorrectSubTheme";
            environments["ITEM2"].SubTheme = subTheme;
            sut.CdtsEnvironment.SubTheme.Should().Be(subTheme);
        }

        [Theory, AutoNSubstituteData]
        public void SubThemeMustReturnCorectValue([Frozen]ICdtsCacheProvider cdtsCacheProvider, [Frozen] ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider, Model sut)
        {
            sut.Settings.Environment = "ITEM1";

            var environments = new CdtsEnvironmentCache(cdtsCacheProvider, cdtsSRIHashesCacheProvider).GetContent();
            environments["ITEM1"].SubTheme = "CorrectSubTheme";
            environments["ITEM2"].SubTheme = "IncorrectSubTheme";
            sut.CdtsEnvironment.SubTheme.Should().Be("CorrectSubTheme");
        }
        
        [Theory, AutoNSubstituteData]
        public void AKAMAIDeserialize(ICdtsCacheProvider cdtsCacheProvider, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            var env = new CdtsEnvironmentCache(cdtsCacheProvider, cdtsSRIHashesCacheProvider).DeserializeEnvironments();
            env["AKAMAI"].Should().BeEquivalentTo(new CdtsEnvironment
            {
                Name = "AKAMAI",
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/",
                Theme = "gcweb",
                CDN = "prod",
                IsVersionRNCombined = true,
                IsEncryptionModifiable = false,
                AppendToTitle = " - Canada.ca",
                FooterSectionLimit = 0,
                CanHaveMultipleContactLinks = false,
                CanHaveContactLinkInAppTemplate = true,
                CanUseWebAnalytics = true
            });
        }

        [Theory, AutoNSubstituteData]
        public void PRODSSLDeserialize(ICdtsCacheProvider cdtsCacheProvider, ICdtsSRIHashesCacheProvider cdtsSRIHashesCacheProvider)
        {
            var env = new CdtsEnvironmentCache(cdtsCacheProvider, cdtsSRIHashesCacheProvider).DeserializeEnvironments();
            env["PROD_SSL"].Should().BeEquivalentTo(new CdtsEnvironment
            {
                Name = "PROD_SSL",
                Path = "https://cdts.service.canada.ca/{1}/cls/WET/{2}/{3}cdts/",
                Theme = "gcintranet",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = false,
                AppendToTitle = "",
                FooterSectionLimit = 3,
                CanHaveMultipleContactLinks = true,
                CanHaveContactLinkInAppTemplate = false
            });
        }

        [Theory, AutoNSubstituteData]
        public void IfThemeIsGCWebTrueIsReturned(CdtsEnvironment sut)
        {
            sut.Theme = CdtsEnvironment.CdtsThemes.GCWeb.ToString();
            sut.ThemeIsGCWeb().Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void IfThemeIsGCIntrantFalseIsReturned(CdtsEnvironment sut)
        {
            sut.Theme = CdtsEnvironment.CdtsThemes.GCIntranet.ToString();
            sut.ThemeIsGCWeb().Should().BeFalse();
        }

    }
}
