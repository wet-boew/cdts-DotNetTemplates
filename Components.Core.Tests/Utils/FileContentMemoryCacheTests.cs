using FluentAssertions;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using Xunit;

namespace GoC.WebTemplate.Components.Core.Tests.Utils
{
    public class FileContentMemoryCacheTests
    {
        [Theory, AutoNSubstituteData]
        void GetFullFilePath(FileContentMemoryCacheProvider sut)
        {
            var fileName = "appsettings.json";
            var staticFilePath = "tests//core";

            var result = sut.GetFullFilePath(fileName, staticFilePath);

            result.Should().Be(staticFilePath + "\\" + fileName);
        }
    }
}
