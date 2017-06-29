using FluentAssertions;
using GoC.WebTemplate;
using Xunit;

namespace CoreTest
{
    public class DeserializeEnvironmentsTest
    {

        [Fact]
        public void Foo()
        {
            var env = JsonSerializationHelper.DeserializeEnvironments("CDTSEnvironments.json");
            env.Count.Should().Be(6);
        }
    }
}