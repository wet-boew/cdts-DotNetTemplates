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
        public void FileHasElevenEnvironments(ICdtsCacheProvider cdtsCacheProvider)
        {
            var result = new CdtsEnvironmentCache(cdtsCacheProvider).DeserializeEnvironments();
            result.Count.Should().Be(11);
        }

        [Theory, AutoNSubstituteData]
        public void LoadCDTSEnvironments(ICdtsCacheProvider cdtsCacheProvider)
        {
            var result = new CdtsEnvironmentCache(cdtsCacheProvider).DeserializeEnvironments();
            result.Should().NotBeNull();
        }

        [Theory, AutoNSubstituteData]
        public void SubThemeMustChangeWhenEnvironmentChanges([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var environments = new CdtsEnvironmentCache(cdtsCacheProvider).GetContent();
            sut.Settings.Environment = "ITEM2";
            var subTheme = "foo";
            environments["ITEM1"].SubTheme = "IncorrectSubTheme";
            environments["ITEM2"].SubTheme = subTheme;
            sut.CdtsEnvironment.SubTheme.Should().Be(subTheme);
        }

        [Theory, AutoNSubstituteData]
        public void SubThemeMustReturnCorectValue([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            sut.Settings.Environment = "ITEM1";

            var environments = new CdtsEnvironmentCache(cdtsCacheProvider).GetContent();
            environments["ITEM1"].SubTheme = "CorrectSubTheme";
            environments["ITEM2"].SubTheme = "IncorrectSubTheme";
            sut.CdtsEnvironment.SubTheme.Should().Be("CorrectSubTheme");
        }
        
        [Theory, AutoNSubstituteData]
        public void AKAMAIDeserialize(ICdtsCacheProvider cdtsCacheProvider)
        {
            var env = new CdtsEnvironmentCache(cdtsCacheProvider).DeserializeEnvironments();
            env["AKAMAI"].Should().BeEquivalentTo(new CdtsEnvironment
            {
                Name = "AKAMAI",
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
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
        public void PRODSSLDeserialize(ICdtsCacheProvider cdtsCacheProvider)
        {
            var env = new CdtsEnvironmentCache(cdtsCacheProvider).DeserializeEnvironments();
            env["PROD_SSL"].Should().BeEquivalentTo(new CdtsEnvironment
            {
                Name = "PROD_SSL",
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
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

    }
}
