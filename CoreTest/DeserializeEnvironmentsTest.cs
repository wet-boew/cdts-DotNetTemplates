using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using Xunit;

namespace CoreTest
{
    public class DeserializeEnvironmentsTest
    {

        [Fact]
        public void FileHasElevenEnvironments()
        {
            var env = JsonSerializationHelper.DeserializeEnvironments("CDTSEnvironments.json");
            env.Count.Should().Be(11);
        }

        [Fact]
        public void AKAMAIDeserialize()
        {
            var env = JsonSerializationHelper.DeserializeEnvironments("CDTSEnvironments.json");
            env["AKAMAI"].Should().BeEquivalentTo(new CDTSEnvironment
            {
                Name = "AKAMAI",
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                CDN = "prod",
                IsVersionRNCombined = true,
                IsEncryptionModifiable = false,
                AppendToTitle = " - Canada.ca",
                FooterSectionLimit = 0,
                CanHaveMultiContactLinks = false,
                CanHaveContactLinkInAppTemplate = true
            });
        }

        [Fact]
        public void PRODSSLDeserialize()
        {
            var env = JsonSerializationHelper.DeserializeEnvironments("CDTSEnvironments.json");
            env["PROD_SSL"].Should().BeEquivalentTo(new CDTSEnvironment
            {
                Name = "PROD_SSL",
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcintranet",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsEncryptionModifiable = false,
                AppendToTitle = "",
                FooterSectionLimit = 3,
                CanHaveMultiContactLinks = true,
                CanHaveContactLinkInAppTemplate = false
            });
        }
    }
}