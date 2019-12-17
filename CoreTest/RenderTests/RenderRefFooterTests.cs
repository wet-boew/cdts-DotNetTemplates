using FluentAssertions;
using GoC.WebTemplate.Components;
using Xunit;

namespace CoreTest.RenderTests
{
    public class RenderRefFooterTests
    {
        [Theory, AutoNSubstituteData]
        public void RenderRefFooter(Core sut)
        {
            sut.LeavingSecureSiteWarning.RedirectURL = "Redirect URL 1";
            sut.LeavingSecureSiteWarning.Message = "Message 2";
            sut.LeavingSecureSiteWarning.ExcludedDomains = "Exclude Domains 3";

            var result = sut.RenderRefFooter();
            result.ToString().Should().Contain("\"cdnEnv\":\"\"","\"exitScript\":true","\"exitURL\":\"Redirect URL 1\"","\"exitMsg\":\"Message 2\"","\"exitDomains\":\"Exclude Domains 3\"","\"displayModal\":false");
        }

        [Theory, AutoNSubstituteData]
        public void RenderRefFooterWithoutSecureSite(Core sut)
        {
            sut.LeavingSecureSiteWarning.Enabled = false;

            var result = sut.RenderRefFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"exitScript\":false,\"displayModal\":false}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderSecureSiteWarningRefFooter(Core sut)
        {
            sut.LeavingSecureSiteWarning.RedirectURL = "Redirect URL 1";
            sut.LeavingSecureSiteWarning.Message = "Message 2";
            sut.LeavingSecureSiteWarning.ExcludedDomains = "Exclude Domains 3";

            var result = sut.RenderRefFooter();
            result.ToString().Should().Contain("\"cdnEnv\":\"\"","\"exitScript\":true","\"exitURL\":\"Redirect URL 1\"","\"exitMsg\":\"Message 2\"","\"exitDomains\":\"Exclude Domains 3\"","\"displayModal\":false");
        }

        [Theory, AutoNSubstituteData]
        public void SecureSiteYesCancelRender(Core sut)
        {
            sut.LeavingSecureSiteWarning.CancelMessage = "My Cancel Message";
            sut.LeavingSecureSiteWarning.YesMessage = "This is a Yes message";

            var result = sut.RenderRefFooter();
            result.ToString().Should().Contain("\"cancelMsg\":\"" + sut.LeavingSecureSiteWarning.CancelMessage + "\"");
            result.ToString().Should().Contain("\"yesMsg\":\"" + sut.LeavingSecureSiteWarning.YesMessage + "\"");
        }


    }
}
