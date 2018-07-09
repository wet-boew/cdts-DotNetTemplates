using FluentAssertions;
using GoC.WebTemplate.Components;
using Xunit;


namespace CoreTest.RenderTests
{
    public class RenderUnilingualPreFooter
    {
        [Theory, AutoNSubstituteData]
        public void PageDetails(Core sut)
        {
            sut.RenderUnilingualPreFooter()
                .ToString()
                .Should()
                .Contain("\"pagedetails\":false");

        }
    }
}

