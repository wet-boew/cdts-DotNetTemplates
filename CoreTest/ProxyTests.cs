using System.Web;
using FluentAssertions;
using GoC.WebTemplate.Proxies;
using Xunit;

namespace CoreTest
{
    public class ProxyTests
    {
        [Fact]
        public void CurrentRequestProxyReturnsCurrentRequest()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest(null, "http://foo.bar", "foo=bar"),
                new HttpResponse(null));
            var sut = new CurrentRequestProxy();
            sut.QueryString.Should().Be("foo=bar");
        }
    }
}