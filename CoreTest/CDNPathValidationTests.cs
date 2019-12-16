using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using Xunit;

namespace CoreTest
{
    public class CDNPathValidationTests
    {


        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfHTTPSIsNonModifiableButUseHTTPSIsSet([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.IsEncryptionModifiable = false;
            sut.UseHTTPS = true;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.Should().Throw<InvalidOperationException>()
                .WithMessage($"{sut.Environment} does not allow useHTTPS to be toggled");
        }

        [Theory, AutoNSubstituteData]
        public void DoNotThrowExceptionIfHTTPSIsNonModifiableAndUseHTTPSIsNull([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.IsEncryptionModifiable = false;
            sut.UseHTTPS = null;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.Should().NotThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfHTTPSIsModifiableAndUseHTTPSIsNull([Frozen] IDictionary<string, ICDTSEnvironment> environments,
            Core sut)
        {
            var currentEnv = environments[sut.Environment];
            currentEnv.IsEncryptionModifiable = true;
            sut.UseHTTPS = null;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.Should().Throw<InvalidOperationException>()
                .WithMessage($"{sut.Environment} requires UseHTTPS to be true or false not null.");
        }

    }
}