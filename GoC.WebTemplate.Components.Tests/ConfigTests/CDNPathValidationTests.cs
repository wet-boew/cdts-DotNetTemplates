using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using GoC.WebTemplate.Components.Utils.Caching;
using Xunit;

namespace GoC.WebTemplate.Components.Test.ConfigTests
{
    public class CDNPathValidationTests
    {


        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfHTTPSIsNonModifiableButUseHTTPSIsSet([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment];
            currentEnv.IsEncryptionModifiable = false;
            sut.Settings.UseHttps = true;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.Should().Throw<InvalidOperationException>()
                .WithMessage($"{sut.Settings.Environment} does not allow useHTTPS to be toggled");
        }

        [Theory, AutoNSubstituteData]
        public void DoNotThrowExceptionIfHTTPSIsNonModifiableAndUseHTTPSIsNull([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment];
            currentEnv.IsEncryptionModifiable = false;
            sut.Settings.UseHttps = null;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.Should().NotThrow<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfHTTPSIsModifiableAndUseHTTPSIsNull([Frozen]ICdtsCacheProvider cdtsCacheProvider, Model sut)
        {
            var currentEnv = new CdtsEnvironmentCache(cdtsCacheProvider).GetContent()[sut.Settings.Environment];
            currentEnv.IsEncryptionModifiable = true;
            sut.Settings.UseHttps = null;

            Action test = () =>
            {
                var unused = sut.CDNPath;
            };
            test.Should().Throw<InvalidOperationException>()
                .WithMessage($"{sut.Settings.Environment} requires UseHTTPS to be true or false not null.");
        }

    }
}