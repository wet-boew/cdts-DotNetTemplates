﻿using FluentAssertions;
using Xunit;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderRefFooterTests
    {
        [Theory, AutoNSubstituteData]
        public void RenderRefFooter(Model sut)
        {
            sut.Settings.LeavingSecureSiteWarning.Enabled = true;
            sut.Settings.LeavingSecureSiteWarning.RedirectUrl = "Redirect URL 1";
            sut.Settings.LeavingSecureSiteWarning.Message = "Message 2";
            sut.Settings.LeavingSecureSiteWarning.ExcludedDomains = "Exclude Domains 3";

            var result = sut.Render.RefFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"exitScript\":true,\"exitURL\":\"Redirect URL 1\",\"exitMsg\":\"Message 2\",\"exitDomains\":\"Exclude Domains 3\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderRefFooterWithoutSecureSite(Model sut)
        {
            sut.Settings.LeavingSecureSiteWarning.Enabled = false;

            var result = sut.Render.RefFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\"}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderSecureSiteWarningRefFooter(Model sut)
        {
            sut.Settings.LeavingSecureSiteWarning.Enabled = true;
            sut.Settings.LeavingSecureSiteWarning.RedirectUrl = "Redirect URL 1";
            sut.Settings.LeavingSecureSiteWarning.Message = "Message 2";
            sut.Settings.LeavingSecureSiteWarning.ExcludedDomains = "Exclude Domains 3";

            var result = sut.Render.RefFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"exitScript\":true,\"exitURL\":\"Redirect URL 1\",\"exitMsg\":\"Message 2\",\"exitDomains\":\"Exclude Domains 3\"}");
        }


    }
}
