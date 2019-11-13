using FluentAssertions;
using GoC.WebTemplate.Components.Utils;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using Xunit;

namespace GoC.WebTemplate.Components.Test.UtilTests
{
    public class ModelBuilderTests
    {
        /// <summary>
        /// Default link is to french as the current culture is english
        /// </summary>
        [Fact]
        public void LanguageLinkSetsFrenchinEnglishCulture()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Constants.ENGLISH_CULTURE);
            var result = ModelBuilder.BuildLanguageLinkURL(string.Empty);
            result.Should().Be("?" + Constants.QUERYSTRING_CULTURE_KEY + "=" + Constants.FRENCH_CULTURE);
        }
        
        [Fact]
        public void LanguageLinkSetsEnglishinFrenchCulture()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Constants.FRENCH_CULTURE);
            var result = ModelBuilder.BuildLanguageLinkURL(string.Empty);
            result.Should().Be("?" + Constants.QUERYSTRING_CULTURE_KEY + "=" + Constants.ENGLISH_CULTURE);
        }

        [Fact]
        public void LanguageLinkDoesntChangeOtherValues()
        {
            var customQueryString = new NameValueCollection
            {
                { "fancypants", "homeboy" }
            };

            var result = ModelBuilder.BuildLanguageLinkURL(customQueryString.ToString());

            result.Should().Contain(customQueryString.ToString());
        }
    }
}
