using System.Collections.Generic;
using FluentAssertions;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using Xunit;

namespace GoC.WebTemplate.Components.Test.UtilTests
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
                    Enums = new List<SocialMediaSites>
                    {
                        SocialMediaSites.blogger,
                        SocialMediaSites.diigo
                    }
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showShare\":[\"blogger\",\"diigo\"]");
        }
        [Fact]
        public void RenderShowShareAsTrue()
        {
            var preFooter = new PreFooter
            {
                ShowShare = new ShareList
                {
                    Show = true,
                    Enums = new List<SocialMediaSites>()
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
                    Enums = new List<SocialMediaSites>()
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
                    Url = string.Empty
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
                    Url = "foo"
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
                    Url = "foo"
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showFeedback\":false");
        }

    }
}