using System;
using FluentAssertions;
using GoC.WebTemplate.Components;
using Xunit;

namespace CoreTest.RenderTests
{
    public class RenderPreFooterTests
    {

        [Theory, AutoNSubstituteData]
        public void RenderPreFooterTest(Core sut)
        {
            //NOTE: Only testing VersionIdentifier and ScreenIdentifier for now. More properties can be added later.
            sut.DateModified = Convert.ToDateTime("9 january 2015");
            sut.VersionIdentifier = "1.2.3";
            sut.ScreenIdentifier = "Test ID";
            sut.ShowFeedbackLink = true;
            sut.FeedbackLinkURL = "test feedback url";
            var result = sut.RenderPreFooter();

            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"versionIdentifier\":\"1.2.3\",\"dateModified\":\"2015-01-09\",\"showPostContent\":false,\"showFeedback\":\"test feedback url\",\"showShare\":true,\"screenIdentifier\":\"Test ID\"}");
        }
        [Theory, AutoNSubstituteData]
        public void RenderPreFooterWithNullsTest(Core sut)
        {
            //NOTE: Only testing VersionIdentifier and ScreenIdentifier for now. More properties can be added later.
            sut.DateModified = DateTime.MinValue;
            sut.VersionIdentifier = "  ";
            sut.ScreenIdentifier = null;
            sut.ShowFeedbackLink = true;
            sut.FeedbackLinkURL = "test feedback url";
            var result = sut.RenderPreFooter();

            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"showPostContent\":false,\"showFeedback\":\"test feedback url\",\"showShare\":true}");
        }

        [Theory, AutoNSubstituteData]
        public void RenderTransactionalPreFooter(Core sut)
        {
            sut.DateModified = Convert.ToDateTime("21 september 2018");
            sut.VersionIdentifier = "Version Ident 1";
            sut.ScreenIdentifier = "Screen Ident 2";

            var result = sut.RenderTransactionalPreFooter();
            result.ToString().Should().Be("{\"cdnEnv\":\"\",\"versionIdentifier\":\"Version Ident 1\",\"dateModified\":\"2018-09-21\",\"showPostContent\":false,\"showFeedback\":false,\"showShare\":false,\"screenIdentifier\":\"Screen Ident 2\"}");
        }

    }
}