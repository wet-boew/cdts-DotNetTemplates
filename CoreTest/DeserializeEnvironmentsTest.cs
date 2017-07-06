using FluentAssertions;
using GoC.WebTemplate;
using WebTemplateCore.JSONSerializationObjects;
using Xunit;
using Xunit.Sdk;

namespace CoreTest
{
    public class DeserializeEnvironmentsTest
    {

        [Fact]
        public void FileHasEightEnvironments()
        {
            var env = JsonSerializationHelper.DeserializeEnvironments("CDTSEnvironments.json");
            env.Count.Should().Be(8);
        }

        [Fact]
        public void AKAMAIDeserialize()
        {
            var env = JsonSerializationHelper.DeserializeEnvironments("CDTSEnvironments.json");
            env["AKAMAI"].ShouldBeEquivalentTo(new CDTSEnvironment
            {
                Name = "AKAMAI",
                Path = "https://www.canada.ca/etc/designs/canada/cdts/{2}/{3}cdts/compiled/",
                Theme = "gcweb",
                CDN = "prod",
                IsVersionRNCombined = true,
                IsSSLModifiable = false,
                AppendToTitle = " - Canada.ca"


            });
        }

        [Fact]
        public void PRODSSLDeserialize()
        {
            var env = JsonSerializationHelper.DeserializeEnvironments("CDTSEnvironments.json");
            env["PROD_SSL"].ShouldBeEquivalentTo(new CDTSEnvironment
            {
                Name = "PROD_SSL",
                Path = "https://ssl-templates.services.gc.ca/{1}/cls/wet/{2}/{3}cdts/compiled/",
                Theme = "gcintranet",
                CDN = "prod",
                IsVersionRNCombined = false,
                IsSSLModifiable = false,
                AppendToTitle = ""


            });
        }
    }
}