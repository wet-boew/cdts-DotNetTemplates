using FluentAssertions;
using Xunit;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderUnilingualPreFooter
    {
        [Theory, AutoNSubstituteData]
        public void PageDetails(Model sut)
        {
            sut.Render.UnilingualPreFooter().ToString().Should().Contain("\"cdnEnv\":\"\"");
        }
    }
}

