using FluentAssertions;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using NUnit.Framework;

namespace GoC.WebTemplate.Components.Core.Tests.Utils
{
    public class FileContentMemoryCacheTests
    {
        [Theory, AutoNSubstituteData]
        public void GetFullFilePath(FileContentMemoryCacheProvider sut)
        {
            var fileName = "appsettings.json";
            var staticFilePath = "tests//core";

            var result = sut.GetFullFilePath(fileName, staticFilePath);

            result.Should().Be(staticFilePath + "\\" + fileName);
        }
    }
}
