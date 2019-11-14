using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using AutoFixture.Xunit2;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Configs.Cdts;

namespace GoC.WebTemplate.Components.Test.RenderTests
{
    public class RenderAppTopTests
    {
        [Theory, AutoNSubstituteData]
        public void MenuLinksShouldNotRenderWhenNull(Model sut)
        {
            sut.MenuLinks = null;
            sut.Render.AppTop().ToString().Should().NotContain("menuLinks");
        }

        [Theory, AutoNSubstituteData]
        public void DoNotRenderNewWindowInMenuLinksIfFalse(Model sut)
        {
            sut.MenuLinks = new List<MenuLink>
            {
                new MenuLink
                {
                    Href = "foo",
                    Text = "bar",
                    NewWindow = false
                }
            };

            sut.Render.AppTop().ToString().Should().NotContain("newWindow");
        }

        [Theory, AutoNSubstituteData]
        public void RenderNewWindowInMenuLinksIfTrue(Model sut)
        {
            sut.MenuLinks = new List<MenuLink>
      {
        new MenuLink
        {
          Href = "foo",
          Text = "bar",
          NewWindow = true
        }
      };

            sut.Render.AppTop().ToString().Should().Contain("newWindow");
        }

        [Theory, AutoNSubstituteData]
        public void MenuLinks(Model sut)
        {
            sut.MenuLinks = new List<MenuLink>
      {
        new MenuLink
        {
          Href = "foo",
          Text = "bar",
        }
      };

            sut.Render.AppTop().ToString().Should().Contain("\"menuLinks\":[{\"href\":\"foo\",\"text\":\"bar\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void SubLinks(Model sut)
        {
            sut.MenuLinks = new List<MenuLink>
            {
                new MenuLink
                {
                    Text = "MenuLink",
                    SubLinks = new List<SubLink>
                    {
                        new SubLink
                        {
                            Text = "SubLinkText1",
                            Href = "SubLinkHerf1",
                            NewWindow = true
                        }
                    }
                }
            };
            var result = sut.Render.AppTop();

            result.ToString().Should().Contain("\"menuLinks\":[{\"subLinks\":[{\"subhref\":\"SubLinkHerf1\",\"subtext\":\"SubLinkText1\",\"newWindow\":true}],\"text\":\"MenuLink\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfMenuLinksAndCustomMenuURLAreBothSet(Model sut)
        {
            sut.MenuLinks = new List<MenuLink> { new MenuLink() };
            sut.CustomSiteMenuURL = "Foo";

            // ReSharper disable once MustUseReturnValue
            Action act = () => sut.Render.AppTop();

            act.Should().Throw<InvalidOperationException>();
        }


        [Theory]
        [InlineAutoNSubstituteData("GCWeb")]
        [InlineAutoNSubstituteData("GCTheme")]
        public void AppNameAndAppURLRenderedSameBetweenGCWebandGCIntranet(string theme, [Frozen] ICdtsEnvironment fakeEnvironment, Model sut)
        {

            fakeEnvironment.Theme = theme;
            sut.ApplicationTitle.Text = "foo";
            sut.ApplicationTitle.Href = "bar";


            sut.Render.AppTop().ToString()
              .Should().Contain("\"appName\":[{\"href\":\"bar\",\"text\":\"foo\"}]");
        }
        [Theory, AutoNSubstituteData]
        public void AppSettingsLinkRendersWhenPresent(Model sut)
        {
            //Setup
            var testurl = "http://tempuri.com";
            sut.AppSettingsURL = testurl;
            //Test
            var results = sut.Render.AppTop();
            //Verify
            results.ToString().Should().Contain("\"appSettings\":[{\"href\":\"" + testurl + "\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void AppSettingsLinkShouldNotRendersWhenPresent(Model sut)
        {
            //Setup           
            sut.AppSettingsURL = null;
            //Test
            var results = sut.Render.AppTop();
            //Verify
            results.ToString().Should().NotContain("\"appSettings\"");
        }

        [Theory, AutoNSubstituteData]
        public void TopSecMenuTrueInAppTopWhenLeftMenuItems(Model sut)
        {
            sut.LeftMenuItems = new List<MenuSection>();
            sut.LeftMenuItems.Add(new MenuSection
            {
                Href = "foo",
                Text = "bar"
            });
            sut.Render.AppTop().ToString().Should().Contain("\"topSecMenu\":true");
        }

        [Theory, AutoNSubstituteData]
        public void TopSecMenuFalseInAppTopWhenLeftMenuItems(Model sut)
        {
            sut.Render.AppTop().ToString().Should().Contain("\"topSecMenu\":false");
        }

        [Theory, AutoNSubstituteData]
        public void IntranetTitleShouldNotRenderWhenNullInAppTop(Model sut)
        {
            sut.IntranetTitle = null;
            sut.Render.AppTop().ToString().Should().NotContain("\"intranetTitle\":[null]");
        }

        [Theory, AutoNSubstituteData]
        public void IntranetTitleAppTop(Model sut)
        {
            sut.IntranetTitle = new IntranetTitle { Text = "foo", Href = "bar", Acronym = "plat" };
            sut.Render.AppTop().ToString().Should().Contain("\"intranetTitle\":[{\"href\":\"bar\",\"text\":\"foo\",\"acronym\":\"plat\"}]");
        }
        
        [Theory, AutoNSubstituteData]
        public void RenderCustomSearchWhenSet(Model sut)
        {
            sut.CustomSearch = new CustomSearch
            {
                Action = "action1",
                Id = "id3",
                Method = "method4",
                Placeholder = "placeholder5",
                HiddenInput = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("name1", "val1"),
                    new KeyValuePair<string, string>("name2", "val2")
                }
            };

            var json = sut.Render.AppTop();
            json.ToString().Should().Contain("\"customSearch\":[{\"action\":\"action1\",\"placeholder\":\"placeholder5\",\"id\":\"id3\",\"method\":\"method4\",\"hiddenInput\":[{\"name\":\"name1\",\"value\":\"val1\"},{\"name\":\"name2\",\"value\":\"val2\"}]}]");
        }
        
        [Theory, AutoNSubstituteData]
        public void RenderCustomSearchNotSet(Model sut)
        {
            var json = sut.Render.AppTop();
            json.ToString().Should().NotContain("customSearch");
        }
        
        [Theory, AutoNSubstituteData]
        public void AppSearchIsNullByDefault(Model sut)
        {
            var json = sut.Render.AppTop();
            json.ToString().Should().NotContain("\"Search\"");
        }

        [Theory, AutoNSubstituteData]
        public void RenderAppTopMustNotCrashWithNullBreadCrumbs(Model sut)
        {
            sut.Breadcrumbs = null;
            // ReSharper disable once MustUseReturnValue
            Action execute = () => sut.Render.AppTop();
            execute.Should().NotThrow<ArgumentNullException>();
        }

        [Theory, AutoNSubstituteData]
        public void SiteMenuPathShouldNotRenderWhenNull(Model sut)
        {
            var json = sut.Render.AppTop();
            json.ToString().Should().NotContain("menuPath");
        }

        [Theory, AutoNSubstituteData]
        public void SignInLinkNotRenderedWhenFlagisFalse(Model sut)
        {

            sut.ShowSignInLink = false;
            var json = sut.Render.AppTop();
            json.ToString().Should().NotContain("signIn");
        }

        [Theory, AutoNSubstituteData]
        public void SignInLinkNotRenderedWhenLinkIsNull(Model sut)
        {

            sut.ShowSignInLink = true;
            sut.Settings.SignInLinkUrl = null;
            var json = sut.Render.AppTop();
            json.ToString().Should().NotContain("signIn");
        }

        [Theory, AutoNSubstituteData]
        public void SignOutLinkNotRenderedWhenFlagisFalse(Model sut)
        {

            sut.ShowSignOutLink = false;
            var json = sut.Render.AppTop();
            json.ToString().Should().NotContain("signOut");
        }

        [Theory, AutoNSubstituteData]
        public void SignOutLinkNotRenderedWhenLinkIsNull(Model sut)
        {

            sut.ShowSignOutLink = true;
            sut.ShowSignInLink = false;
            sut.Settings.SignOutLinkUrl = null;
            var json = sut.Render.AppTop();
            json.ToString().Should().NotContain("signOut");
        }

        [Theory, AutoNSubstituteData]
        public void SignInAndSignOutLinkBothOn(Model sut)
        {
            sut.ShowSignOutLink = true;
            sut.ShowSignInLink = true;
            // ReSharper disable once MustUseReturnValue
            Action act = () => sut.Render.AppTop();
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void LanguageLinkRenders(Model sut)
        {
            sut.LanguageLink.Href = "foo-en.lang";
            sut.Settings.ShowLanguageLink = true;
            var json = sut.Render.AppTop();
            json.ToString().Should().Contain("\"lngLinks\":[{\"href\":\"foo-en.lang\"");
        }

    }
}