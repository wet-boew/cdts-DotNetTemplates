using FluentAssertions;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils.Caching;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Xunit;

namespace GoC.WebTemplate.Components.Framework.Tests
{
    public class SessionTests
    {
        [Theory, AutoNSubstituteData]
        public void SessionStateTimeoutDoesOverride(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, WebTemplateSettings settings)
        {
            SetHttpContextSessionTimeoutTo(10);
            var sut = new Model(fileContentCacheProvider, settings, cdtsCacheProvider);            
            sut.Settings.SessionTimeout.SessionAlive = 1200000; //assume value set from config 
            
            sut.Settings.SessionTimeout.CheckWithServerSessionTimeout(HttpContext.Current.Session);

            sut.Settings.SessionTimeout.SessionAlive.Should().Be(HttpContext.Current.Session.Timeout * 60000);
        }

        [Theory, AutoNSubstituteData]
        public void SessionStateTimeoutDoesNotOverride(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, WebTemplateSettings settings)
        {
            SetHttpContextSessionTimeoutTo(20);
            var sut = new Model(fileContentCacheProvider, settings, cdtsCacheProvider);
            sut.Settings.SessionTimeout.SessionAlive = 60000; //assume value set from config 

            sut.Settings.SessionTimeout.CheckWithServerSessionTimeout(HttpContext.Current.Session);

            sut.Settings.SessionTimeout.SessionAlive.Should().Be(60000);
        }

#pragma warning disable xUnit1013 // Public method should be marked as test
        public void SetHttpContextSessionTimeoutTo(int timeout)
#pragma warning restore xUnit1013 // Public method should be marked as test
        {
            //building a fake session and setting the session timeout
            var context = new HttpContext(new HttpRequest("foo", "http://www.text.com", "cue"), new HttpResponse(new StringWriter()));
            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(), timeout, true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc, false);
            SessionStateUtility.AddHttpSessionStateToContext(context, sessionContainer);
            HttpContext.Current = context;
        }

    }
}
