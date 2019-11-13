using FluentAssertions;
using GoC.WebTemplate.Components.Framework.Utils;
using System.Web;
using Xunit;

namespace GoC.WebTemplate.Components.Framework.Tests.Utils
{
    public class SessionControlerTests
    {
        [Fact]
        public void SessionCanBeAccessed()
        {
            SessionCreaterTestHelper.CreateSession();
            var sut = new SessionController(HttpContext.Current.Session);

            var result = sut.SessionExists();
            result.Should().BeTrue();
        }
    }
}
