using FluentAssertions;
using GoC.WebTemplate.Components.Utils;
using System.Threading;
using Xunit;

namespace GoC.WebTemplate.Components.Test.UtilTests
{
    public class LanguageSwitcherTests
    {
        [Theory, AutoNSubstituteData]
        public void CultureGetsSetToEnglish(LanguageSwitcher sut)
        {
            sut.UpdateCulture("en");
            Thread.CurrentThread.CurrentUICulture.Name.Should().Be(Constants.ENGLISH_CULTURE);
        }


        [Theory, AutoNSubstituteData]
        public void CultureGetsSetToFrench(LanguageSwitcher sut)
        {
            sut.UpdateCulture("fr");
            Thread.CurrentThread.CurrentUICulture.Name.Should().Be(Constants.FRENCH_CULTURE);
        }
    }
}
