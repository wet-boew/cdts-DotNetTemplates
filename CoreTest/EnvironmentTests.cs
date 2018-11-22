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
    public class EnvironmentTests
    {

        [Fact]
        public void LoadCDTSEnvironmentsFromNull()
        {
            var cdtsEnvFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CDTSEnvironments.json");
            var result = new CDTSEnvironmentLoader(new CacheProxy()).LoadCDTSEnvironments(cdtsEnvFilename);
            result.Should().HaveCountGreaterOrEqualTo(11);
        }

        [Theory, AutoNSubstituteData]
        public void LoadCDTSEnvironments([Frozen] ICacheProxy cacheProxy)
        {
            var cdtsEnvFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CDTSEnvironments.json");
            var result = new CDTSEnvironmentLoader(cacheProxy).LoadCDTSEnvironments(cdtsEnvFilename);
            result.Should().NotBeNull();
        }

        [Theory, AutoNSubstituteData]
        public void SubThemeMustChangeWhenEnvironmentChanges([Frozen]IDictionary<string, ICDTSEnvironment> environments,
Core sut)
        {
            sut.Environment = "ITEM2";
            var subTheme = "foo";
            environments["ITEM1"].SubTheme = "IncorrectSubTheme";
            environments["ITEM2"].SubTheme = subTheme;
            sut.WebTemplateSubTheme.Should().Be(subTheme);
        }

        [Theory, AutoNSubstituteData]
        public void SubThemeMustReturnCorectValue([Frozen]IDictionary<string, ICDTSEnvironment> environments,
Core sut)
        {
            environments["ITEM1"].SubTheme = "CorrectSubTheme";
            environments["ITEM2"].SubTheme = "IncorrectSubTheme";
            sut.WebTemplateSubTheme.Should().Be("CorrectSubTheme");
        }

    }
}
