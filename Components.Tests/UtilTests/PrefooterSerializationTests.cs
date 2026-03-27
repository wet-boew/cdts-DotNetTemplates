using System.Collections.Generic;
using FluentAssertions;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using NUnit.Framework;

namespace GoC.WebTemplate.Components.Test.UtilTests
{
    public class PrefooterSerializationTests
    {
        [Test]
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
        [Test]
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

        [Test]
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
        [Test]
        public void RenderShowFeedbackAsTrue()
        {
            var preFooter = new PreFooter
            {
                ShowFeedback = new Feedback
                {
                    Enabled = true
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showFeedback\":{\"enabled\":true}");
        } 

        [Test]
        public void RenderShowFeedbackURL()
        {
            var preFooter = new PreFooter
            {
                ShowFeedback = new Feedback
                {
                    Enabled = true,
                    Text = "Contact us",
                    Href = "google.ca"
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showFeedback\":{\"enabled\":true,\"text\":\"Contact us\",\"href\":\"google.ca\"");
        }

        [Test]
        public void RenderShowFeedbackAsFalse()
        {
            var preFooter = new PreFooter
            {
                ShowFeedback = new Feedback
                {
                    Enabled = false
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showFeedback\":{}");
        }

        [Test]
        public void RenderShowFeedbackHiddenFields()
        {
            var preFooter = new PreFooter
            {
                ShowFeedback = new Feedback
                {
                    Enabled = true,
                    Theme = "Theme",
                    Section = "Section"
                }
            };
            var json = JsonSerializationHelper.SerializeToJson(preFooter);
            json.ToString().Should().Contain("\"showFeedback\":{\"enabled\":true,\"theme\":\"Theme\",\"section\":\"Section\"");
        }

    }
}