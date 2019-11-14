using FluentAssertions;
using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils.Caching;
using System.Web;
using Xunit;

namespace GoC.WebTemplate.Components.Framework.Tests
{
    public class SessionTests
    {
        [Theory, AutoNSubstituteData]
        public void SessionStateTimeoutDoesOverride(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, WebTemplateSettings settings)
        {
            SessionCreaterTestHelper.CreateSession(10);
            var sut = new Model(fileContentCacheProvider, settings, cdtsCacheProvider);            
            sut.Settings.SessionTimeout.SessionAlive = 1200000; //assume value set from config 
            
            sut.Settings.SessionTimeout.CheckWithServerSessionTimeout(HttpContext.Current.Session);

            sut.Settings.SessionTimeout.SessionAlive.Should().Be(HttpContext.Current.Session.Timeout * 60000);
        }

        [Theory, AutoNSubstituteData]
        public void SessionStateTimeoutDoesNotOverride(IFileContentCacheProvider fileContentCacheProvider, ICdtsCacheProvider cdtsCacheProvider, WebTemplateSettings settings)
        {
            SessionCreaterTestHelper.CreateSession(20);
            var sut = new Model(fileContentCacheProvider, settings, cdtsCacheProvider);
            sut.Settings.SessionTimeout.SessionAlive = 60000; //assume value set from config 

            sut.Settings.SessionTimeout.CheckWithServerSessionTimeout(HttpContext.Current.Session);

            sut.Settings.SessionTimeout.SessionAlive.Should().Be(60000);
        }
    }
}
