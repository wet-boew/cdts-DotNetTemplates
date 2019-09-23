using System;
using System.Globalization;
using System.Threading;
using FluentAssertions;
using Xunit;
using GoC.WebTemplate.Components.Utils;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderPreFooterTests
    {

        [Theory, AutoNSubstituteData]
        public void RenderPreFooterTest(Model sut)
        {
            //NOTE: Only testing VersionIdentifier and ScreenIdentifier for now. More properties can be added later.
            sut.Settings.ShowSharePageLink = true;
            sut.Settings.ShowPostContent = false;
            sut.DateModified = Convert.ToDateTime("9 january 2015");
            sut.VersionIdentifier = "1.2.3";
            sut.ScreenIdentifier = "Test ID";
            sut.FeedbackLink.Show = true;
            sut.FeedbackLink.Url = "test feedback url";
            var result = sut.Render.PreFooter();

            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"versionIdentifier\":\"1.2.3\",\"dateModified\":\"2015-01-09\",\"showFeedback\":\"test feedback url\",\"showShare\":true,\"screenIdentifier\":\"Test ID\"}");
        }
        [Theory, AutoNSubstituteData]
        public void RenderPreFooterWithNullsTest(Model sut)
        {
            //NOTE: Only testing VersionIdentifier and ScreenIdentifier for now. More properties can be added later.
            sut.Settings.ShowSharePageLink = true;
            sut.DateModified = DateTime.MinValue;
            sut.VersionIdentifier = "  ";
            sut.ScreenIdentifier = null;
            sut.FeedbackLink.Show = true;
            sut.FeedbackLink.Url = "test feedback url";
            var result = sut.Render.PreFooter();

            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"showFeedback\":\"test feedback url\",\"showShare\":true}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderPreFooterWithFeebackLinkUrlFr(Model sut)
        {
            sut.Settings.ShowSharePageLink = true;
            sut.DateModified = DateTime.MinValue;
            sut.VersionIdentifier = "  ";
            sut.ScreenIdentifier = null;
            sut.FeedbackLink.Show = true;
            sut.FeedbackLink.Url = "test feedback url";
            sut.FeedbackLink.UrlFr = "test feedback french url";
            var result = sut.Render.PreFooter();

            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"showFeedback\":\"test feedback url\",\"showShare\":true}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderPreFooterWithFeebackLinkUrlFrInFrenchCulture(Model sut)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Constants.FRENCH_CULTURE);

            sut.Settings.ShowSharePageLink = true;
            sut.DateModified = DateTime.MinValue;
            sut.VersionIdentifier = "  ";
            sut.ScreenIdentifier = null;
            sut.FeedbackLink.Show = true;
            sut.FeedbackLink.Url = "test feedback url";
            sut.FeedbackLink.UrlFr = "test feedback french url";
            var result = sut.Render.PreFooter();

            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"showFeedback\":\"test feedback french url\",\"showShare\":true}");
            //need to reset the culture back to english as that is what other tests expect it to be by default
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Constants.ENGLISH_CULTURE);
        }

        [Theory, AutoNSubstituteData]
        public void RenderTransactionalPreFooter(Model sut)
        {
            sut.Settings.ShowSharePageLink = false;
            sut.DateModified = Convert.ToDateTime("21 september 2018");
            sut.VersionIdentifier = "Version Ident 1";
            sut.ScreenIdentifier = "Screen Ident 2";

            var result = sut.Render.TransactionalPreFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"versionIdentifier\":\"Version Ident 1\",\"dateModified\":\"2018-09-21\",\"showFeedback\":false,\"showShare\":false,\"screenIdentifier\":\"Screen Ident 2\"}");
        }

    }
}