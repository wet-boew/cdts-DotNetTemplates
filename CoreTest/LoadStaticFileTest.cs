using System;
using System.Collections.Generic;
using System.IO;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.Components.Proxies;
using Xunit;

namespace CoreTest
{
    public class LoadStaticFileTest
    {
        [Theory, AutoNSubstituteData]
        public void LoadStaticFile(Core sut)
        {
            var result = sut.LoadStaticFile("refTop.html");
            result.ToString().Should().Be("");
        }


        [Theory, AutoNSubstituteData]
        public void LoadStaticFileWithNullCache(IDictionary<string, ICDTSEnvironment> environments,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            var sut = new Core(fakeCurrentRequestProxy, new CacheProxy(), new ConfigurationProxy(), environments);
            sut.StaticFilesPath = AppDomain.CurrentDomain.BaseDirectory;
            var result = sut.LoadStaticFile("app.config");
            result.ToString().Should().Contain("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        }
    }
}
