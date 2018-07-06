using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using Xunit;

namespace CoreTest
{
    public class EnvironmentTests
    {
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
