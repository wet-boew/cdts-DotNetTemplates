using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.Components.Proxies;
using Xunit;

namespace CoreTest
{
    /// <summary>
    /// Tests that test the Configurations Object and how it integrates with the core object.
    /// </summary>
    public class ConfigurationCoreIntegrationTests
    {
        [Theory, AutoNSubstituteData]
        public void SubThemeSetFromCDTSEnvironments([Frozen]ICDTSEnvironment fakeEnvironment,
            IDictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            fakeEnvironment.SubTheme = "foobar";
            
            var sut = new Core(fakeCurrentRequestProxy, fakeCacheProxy, new ConfigurationProxy(), environments);
            sut.RenderTop().ToString().Should().Contain("\"subTheme\":\"foobar\"");
        }
        [Theory, AutoNSubstituteData]
        public void SearchBoxShownByDefault(IDictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //We want to use the app.config to test this so we don't use autonsubstitute to test it.
            var sut = new Core(fakeCurrentRequestProxy, fakeCacheProxy, new ConfigurationProxy(), environments);
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"search\":true");
        }

        [Theory, AutoNSubstituteData]
        public void LeavingSecureSiteWarningElementCapitilizationFix(IDictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            var sut = new Core(fakeCurrentRequestProxy,fakeCacheProxy, new ConfigurationProxy(), environments);
            sut.LeavingSecureSiteWarning.RedirectURL.Should().Be("foo");
        }

        [Theory, AutoNSubstituteData]
        public void SessionStateTimeoutDoesOverride(Dictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy)
        {
            //build a fake session
            var context = new HttpContext(new HttpRequest("foo", "http://www.text.com", "cue"), new HttpResponse(new StringWriter()));
            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(), 10, true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc, false);
            SessionStateUtility.AddHttpSessionStateToContext(context, sessionContainer);
            HttpContext.Current = context;
            HttpContext.Current.Session["SessionVar"] = 123;

            //test
            var sut = new Core(new CurrentRequestProxy(), fakeCacheProxy, new ConfigurationProxy(), environments);
            sut.SessionTimeout.Sessionalive.Should().Be(HttpContext.Current.Session.Timeout * 60000);
        }

        [Theory, AutoNSubstituteData]
        public void SessionStateTimeoutDoesNotOverride(Dictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy, ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            var sut = new Core(fakeCurrentRequestProxy, fakeCacheProxy, new ConfigurationProxy(), environments);
            sut.SessionTimeout.Sessionalive.Should().Be(1200000);
        }
    }
}