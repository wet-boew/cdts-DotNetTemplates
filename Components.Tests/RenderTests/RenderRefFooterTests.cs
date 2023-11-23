using FluentAssertions;
using System;
using Xunit;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderRefFooterTests
    {
        [Theory, AutoNSubstituteData]
        public void RenderRefFooter(Model sut)
        {
            var result = sut.Render.RefFooter(isApplication: false);
            result.ToString().Should().Contain("\"cdnEnv\":\"\"");
        }

        [Theory, AutoNSubstituteData]
        public void RenderRefFooterWithoutSecureSite(Model sut)
        {
            sut.Settings.LeavingSecureSiteWarning.Enabled = false;

            var result = sut.Render.RefFooter(isApplication: false);
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderSecureSiteWarningRefFooter(Model sut)
        {
            sut.Settings.LeavingSecureSiteWarning.Enabled = true;
            sut.Settings.LeavingSecureSiteWarning.RedirectUrl = "Redirect URL 1";
            sut.Settings.LeavingSecureSiteWarning.Message = "Message 2";
            sut.Settings.LeavingSecureSiteWarning.ExcludedDomains = "Exclude Domains 3";
            sut.Settings.LeavingSecureSiteWarning.TargetWarning = "Target";
            sut.Settings.LeavingSecureSiteWarning.DisplayModalForNewWindow = false;
            sut.Settings.LeavingSecureSiteWarning.CancelMessage = null;
            sut.Settings.LeavingSecureSiteWarning.YesMessage = null;
            sut.Settings.LeavingSecureSiteWarning.MsgBoxHeader = null;

            var result = sut.Render.RefFooter(isApplication: false);
            result.ToString().Should().Contain("\"exitSecureSite\":{\"exitScript\":true,\"exitURL\":\"Redirect URL 1\",\"exitMsg\":\"Message 2\",\"exitDomains\":\"Exclude Domains 3\",\"targetWarning\":\"Target\",\"displayModalForNewWindow\":false}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderSecureSiteWarningRefFooterWithoutRedirect(Model sut)
        {
            sut.Settings.LeavingSecureSiteWarning.Enabled = true;
            sut.Settings.LeavingSecureSiteWarning.DisplayModalWindow = true;
            sut.Settings.LeavingSecureSiteWarning.RedirectUrl = null;
            sut.Settings.LeavingSecureSiteWarning.Message = null;
            sut.Settings.LeavingSecureSiteWarning.ExcludedDomains = null;
            sut.Settings.LeavingSecureSiteWarning.TargetWarning = null;
            sut.Settings.LeavingSecureSiteWarning.DisplayModalForNewWindow = false;
            sut.Settings.LeavingSecureSiteWarning.CancelMessage = null;
            sut.Settings.LeavingSecureSiteWarning.YesMessage = null;
            sut.Settings.LeavingSecureSiteWarning.MsgBoxHeader = null;

            var result = sut.Render.RefFooter(isApplication: false);
            result.ToString().Should().Contain("\"exitSecureSite\":{\"exitScript\":true,\"displayModal\":true,\"displayModalForNewWindow\":false}");
        }

        [Theory, AutoNSubstituteData]
        public void WebAnaliticsRenders(Model sut)
        {
            sut.Settings.WebAnalytics.Active = false;
            var result = sut.Render.RefFooter(isApplication: false);
            result.ToString().Should().NotContain("\"webAnalytics\"");
        }

        [Theory, AutoNSubstituteData]
        public void WebAnaliticsOnlyRendersInSpecifiedEnv(Model sut)
        {
            sut.Settings.WebAnalytics.Active = true;
            sut.CdtsEnvironment.CanUseWebAnalytics = false;
            Action act = () => sut.Render.RefFooter(isApplication: false);
            act.Should().Throw<NotSupportedException>().WithMessage("The WebAnalytics is not supported in this enviornment.");
        }

        [Theory, AutoNSubstituteData]
        public void SecureSiteYesCancelRender(Model sut)
        {
            sut.Settings.LeavingSecureSiteWarning.CancelMessage = "My Cancel Message";
            sut.Settings.LeavingSecureSiteWarning.YesMessage = "This is a Yes message";

            var result = sut.Render.RefFooter(isApplication: false);
            result.ToString().Should().Contain("\"cancelMsg\":\"" + sut.Settings.LeavingSecureSiteWarning.CancelMessage + "\"");
            result.ToString().Should().Contain("\"yesMsg\":\"" + sut.Settings.LeavingSecureSiteWarning.YesMessage + "\"");
        }

        [Theory, AutoNSubstituteData]
        public void RenderRefFooterIsApplication(Model sut)
        {
            var result = sut.Render.RefFooter(isApplication: true);
            result.ToString().Should().Contain("\"isApplication\":true");
        }

    }
}
