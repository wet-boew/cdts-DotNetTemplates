using System;
using System.Web;
using FluentAssertions;
using Xunit;
using GoC.WebTemplate.WebForms;

namespace CoreTest
{
    public class WebFormsTests
    {
        [Fact]
        public void BasePageThowsExceptionWithoutMasterPageReference()
        {
            var sut = new BasePage();
            Action test = () =>
            {
                var unused = sut.WebTemplateMaster;
            };
            test.Should().Throw<ArgumentNullException>();
        }
    }
}
