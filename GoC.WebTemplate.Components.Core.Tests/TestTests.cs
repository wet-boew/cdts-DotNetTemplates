using Xunit;
using FluentAssertions;

namespace GoC.WebTemplate.Components.Core.Tests
{
    public class TestTests
    {
        [Fact]
        public void PassingTest()
        {
            var num = 4;
            num.Should().Be(4);
        }
    }
}
