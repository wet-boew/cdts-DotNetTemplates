using System.Collections.Generic;
using FluentAssertions;
using GoC.WebTemplate;
using WebTemplateCore.JSONSerializationObjects;
using Xunit;

namespace CoreTest
{
    public class PrefooterSerializationTests
    {
        [Fact]
        public void RenderShowShareAsList()
        {
            var preFooter = new PreFooter
            {
                ShowShare = new ShareList
                {
                    Show = true,
                    Enums = new List<Core.SocialMediaSites>
                    {
                        Core.SocialMediaSites.bitly, Core.SocialMediaSites.blogger,
                    }
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showShare\":[\"bitly\",\"blogger\"]");
        }
        [Fact]
        public void RenderShowShareAsTrue()
        {
            var preFooter = new PreFooter
            {
                ShowShare = new ShareList
                {
                    Show = true,
                    Enums = new List<Core.SocialMediaSites>()
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showShare\":true");
        }

        [Fact]
        public void RenderShowShareAsFalse()
        {
            var preFooter = new PreFooter
            {
                ShowShare = new ShareList
                {
                    Show = false,
                    Enums = new List<Core.SocialMediaSites>()
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showShare\":false");
            
        }
        [Fact]
        public void RenderShowFeedbackAsTrue()
        {
            var preFooter = new PreFooter
            {
                ShowFeedback = new FeedbackLink
                {
                    Show = true,
                    URL = string.Empty
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showFeedback\":true");
        } 

        [Fact]
        public void RenderShowFeedbackAsURL()
        {
            var preFooter = new PreFooter
            {
                ShowFeedback = new FeedbackLink
                {
                    Show = true,
                    URL = "foo"
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showFeedback\":\"foo\"");
        }

        [Fact]
        public void RenderShowFeedbackAsFalse()
        {
            var preFooter = new PreFooter
            {
                ShowFeedback = new FeedbackLink
                {
                    Show = false,
                    URL = "foo"
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showFeedback\":false");
        }

    }
}