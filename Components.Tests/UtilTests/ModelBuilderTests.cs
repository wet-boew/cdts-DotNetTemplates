using FluentAssertions;
using GoC.WebTemplate.Components.Utils;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Web;
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
            var result = ModelBuilder.BuildLanguageLinkURL(HttpUtility.ParseQueryString(string.Empty));
            result.Should().Be("?" + Constants.QUERYSTRING_CULTURE_KEY + "=" + Constants.FRENCH_CULTURE);
        }
        
        [Fact]
        public void LanguageLinkSetsEnglishinFrenchCulture()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Constants.FRENCH_CULTURE);
            var result = ModelBuilder.BuildLanguageLinkURL(HttpUtility.ParseQueryString(string.Empty));
            result.Should().Be("?" + Constants.QUERYSTRING_CULTURE_KEY + "=" + Constants.ENGLISH_CULTURE);
        }

        [Fact]
        public void LanguageLinkDoesntChangeOtherValues()
        {
            var customQueryString = new NameValueCollection
            {
                { "fancypants", "homeboy" }
            };

            var result = ModelBuilder.BuildLanguageLinkURL(customQueryString);

            result.Should().Contain(customQueryString.ToString());
        }

        [Fact]
        public void LanguageLinkTestEncoding()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Constants.ENGLISH_CULTURE);
            var result = ModelBuilder.BuildLanguageLinkURL(HttpUtility.ParseQueryString("<script> Tést=</script>&a=b&x=y"));
            result.Should().Be("?" + "%3Cscript%3E%20T%C3%A9st=%253c%252fscript%253e&a=b&x=y&" + Constants.QUERYSTRING_CULTURE_KEY + "=" + Constants.FRENCH_CULTURE);
        }
    }
}
