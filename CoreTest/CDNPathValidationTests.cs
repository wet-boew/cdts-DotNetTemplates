using System;
using System.Collections.Generic;
using FluentAssertions;
using GoC.WebTemplate;
using Ploeh.AutoFixture.Xunit2;
using WebTemplateCore.JSONSerializationObjects;
using Xunit;

namespace CoreTest
{
    public class CDNPathValidationTests
    {
            
        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfThemeIsSetButEnvironmentDoesNotAllowIt([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];

            sut.WebTemplateTheme = "GCWeb";
            currentEnv.IsThemeModifiable = false;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldThrow<InvalidOperationException>()
                .WithMessage($"{sut.Environment} does not allow a theme to be set");
        }

        [Theory, AutoNSubstituteData]
        public void DoNotThrowExceptionIfThemeIsNotSetAndNotModifiable([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];

            sut.WebTemplateTheme = string.Empty;
            currentEnv.IsThemeModifiable = false;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldNotThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfThemeIsModifiableButNotSet([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];

            sut.WebTemplateTheme = string.Empty;
            currentEnv.IsThemeModifiable = true;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldThrow<InvalidOperationException>()
                .WithMessage($"{sut.Environment} requires a theme to be set");
        }

        [Theory, AutoNSubstituteData]
        public void DoNotThrowExceptionIfThemeIsSetAndModifiable([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            sut.WebTemplateTheme = "GCWeb";
            currentEnv.IsThemeModifiable = true;

            sut.UseHTTPS = null;
            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldNotThrow<InvalidOperationException>();
        }
        
        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfHTTPSIsNonModifiableButUseHTTPSIsSet([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.IsSSLModifiable = false;
            sut.UseHTTPS = true;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldThrow<InvalidOperationException>()
                .WithMessage($"{sut.Environment} does not allow useHTTPS to be toggled");
        }

        [Theory, AutoNSubstituteData]
        public void DoNotThrowExceptionIfHTTPSIsNonModifiableAndUseHTTPSIsNull([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.IsSSLModifiable = false;
            sut.UseHTTPS = null;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldNotThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfHTTPSIsModifiableAndUseHTTPSIsNull([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.IsSSLModifiable = true;
            sut.UseHTTPS = null;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.ShouldThrow<InvalidOperationException>()
                .WithMessage($"{sut.Environment} requires UseHTTPS to be true or false not null.");
        }

    }
}