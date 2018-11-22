using System;
using System.Collections.Generic;
using System.Globalization;
using FluentAssertions;
using Xunit;
using AutoFixture.Xunit2;
using GoC.WebTemplate.Components;
using GoC.WebTemplate.Components.JSONSerializationObjects;
using GoC.WebTemplate.Components.Proxies;

namespace CoreTest.RenderTests
{
    public class RenderAppTopTests
    {
        [Theory, AutoNSubstituteData]
        public void MenuLinksShouldNotRenderWhenNull(Core sut)
        {
            sut.MenuLinks = null;
            sut.RenderAppTop().ToString().Should().NotContain("menuLinks");
        }

        [Theory, AutoNSubstituteData]
        public void DoNotRenderNewWindowInMenuLinksIfFalse(Core sut)
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

            sut.RenderAppTop().ToString().Should().NotContain("newWindow");
        }

        [Theory, AutoNSubstituteData]
        public void RenderNewWindowInMenuLinksIfTrue(Core sut)
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

            sut.RenderAppTop().ToString().Should().Contain("newWindow");
        }

        [Theory, AutoNSubstituteData]
        public void MenuLinks(Core sut)
        {
            sut.MenuLinks = new List<MenuLink>
      {
        new MenuLink
        {
          Href = "foo",
          Text = "bar",
        }
      };

            sut.RenderAppTop().ToString().Should().Contain("\"menuLinks\":[{\"href\":\"foo\",\"text\":\"bar\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void SubLinks(Core sut)
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
            var result = sut.RenderAppTop();

            result.ToString().Should().Contain("\"menuLinks\":[{\"subLinks\":[{\"subhref\":\"SubLinkHerf1\",\"subtext\":\"SubLinkText1\",\"newWindow\":true}],\"text\":\"MenuLink\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void ThrowExceptionIfMenuLinksAndCustomMenuURLAreBothSet(Core sut)
        {
            sut.MenuLinks = new List<MenuLink> { new MenuLink() };
            sut.CustomSiteMenuURL = "Foo";

            // ReSharper disable once MustUseReturnValue
            Action act = () => sut.RenderAppTop();

            act.Should().Throw<InvalidOperationException>();
        }


        [Theory]
        [InlineAutoNSubstituteData("GCWeb")]
        [InlineAutoNSubstituteData("GCTheme")]
        public void AppNameAndAppURLRenderedSameBetweenGCWebandGCIntranet(string theme, [Frozen] ICDTSEnvironment fakeEnvironment, Core sut)
        {

            fakeEnvironment.Theme = theme;
            sut.ApplicationTitle.Text = "foo";
            sut.ApplicationTitle.Href = "bar";


            sut.RenderAppTop().ToString()
              .Should().Contain("\"appName\":[{\"href\":\"bar\",\"text\":\"foo\"}]");
        }
        [Theory, AutoNSubstituteData]
        public void AppSettingsLinkRendersWhenPresent(Core sut)
        {
            //Setup
            var testurl = "http://tempuri.com";
            sut.AppSettingsURL = testurl;
            //Test
            var results = sut.RenderAppTop();
            //Verify
            results.ToString().Should().Contain("\"appSettings\":[{\"href\":\"" + testurl + "\"}]");
        }

        [Theory, AutoNSubstituteData]
        public void AppSettingsLinkShouldNotRendersWhenPresent(Core sut)
        {
            //Setup           
            sut.AppSettingsURL = null;
            //Test
            var results = sut.RenderAppTop();
            //Verify
            results.ToString().Should().NotContain("\"appSettings\"");
        }

        [Theory, AutoNSubstituteData]
        public void TopSecMenuTrueInAppTopWhenLeftMenuItems(Core sut)
        {
            sut.LeftMenuItems = new List<MenuSection>();
            sut.LeftMenuItems.Add(new MenuSection
            {
                Link = "foo",
                Name = "bar"
            });
            sut.RenderAppTop().ToString().Should().Contain("\"topSecMenu\":true");
        }

        [Theory, AutoNSubstituteData]
        public void TopSecMenuFalseInAppTopWhenLeftMenuItems(Core sut)
        {
            sut.RenderAppTop().ToString().Should().Contain("\"topSecMenu\":false");
        }

        [Theory, AutoNSubstituteData]
        public void IntranetTitleShouldNotRenderWhenNullInAppTop(Core sut)
        {
            sut.IntranetTitle = null;
            sut.RenderAppTop().ToString().Should().NotContain("\"intranetTitle\":[null]");

        }

        [Theory, AutoNSubstituteData]
        public void IntranetTitleAppTop(Core sut)
        {
            sut.IntranetTitle = new Link { Text = "foo", Href = "bar", Acronym = "plat" };
            sut.RenderAppTop().ToString().Should().Contain("\"intranetTitle\":[{\"href\":\"bar\",\"text\":\"foo\",\"acronym\":\"plat\"}]");
        }


        [Theory, AutoNSubstituteData]
        public void RenderCustomSearchWhenSet(Core sut)
        {
            sut.CustomSearch = "Foo";
            sut.RenderAppTop().ToString().Should().Contain("\"customSearch\":\"Foo\"");
        }

        [Theory, AutoNSubstituteData]
        public void CustomSearchIsRendered(Core sut)
        {
            sut.CustomSearch = "foo";
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"customSearch\":\"foo\"");

        }

        [Theory, AutoNSubstituteData]
        public void AppSearchIsNullByDefault(Core sut)
        {
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("\"Search\"");
        }

        [Theory, AutoNSubstituteData]
        public void RenderAppTopMustNotCrashWithNullBreadCrumbs(Core sut)
        {
            sut.Breadcrumbs = null;
            // ReSharper disable once MustUseReturnValue
            Action execute = () => sut.RenderAppTop();
            execute.Should().NotThrow<ArgumentNullException>();
        }

        [Theory, AutoNSubstituteData]
        public void SiteMenuPathShouldNotRenderWhenNull(Core sut)
        {
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("menuPath");
        }

        [Theory, AutoNSubstituteData]
        public void SignInLinkNotRenderedWhenFlagisFalse(Core sut)
        {

            sut.ShowSignInLink = false;
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("signIn");
        }

        [Theory, AutoNSubstituteData]
        public void SignInLinkNotRenderedWhenLinkIsNull(Core sut)
        {

            sut.ShowSignInLink = true;
            sut.SignInLinkURL = null;
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("signIn");
        }

        [Theory, AutoNSubstituteData]
        public void SignOutLinkNotRenderedWhenFlagisFalse(Core sut)
        {

            sut.ShowSignOutLink = false;
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("signOut");
        }

        [Theory, AutoNSubstituteData]
        public void SignOutLinkNotRenderedWhenLinkIsNull(Core sut)
        {

            sut.ShowSignOutLink = true;
            sut.ShowSignInLink = false;
            sut.SignOutLinkURL = null;
            var json = sut.RenderAppTop();
            json.ToString().Should().NotContain("signOut");
        }

        [Theory, AutoNSubstituteData]
        public void SignInAndSignOutLinkBothOn(Core sut)
        {
            sut.ShowSignOutLink = true;
            sut.ShowSignInLink = true;
            // ReSharper disable once MustUseReturnValue
            Action act = () => sut.RenderAppTop();
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory, AutoNSubstituteData]
        public void LanguageLinkRenders(Core sut)
        {
            sut.LanguageLink.Href = "foo-en.lang";
            sut.ShowLanguageLink = true;
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"lngLinks\":[{\"href\":\"foo-en.lang\"");
        }

        [Theory, AutoNSubstituteData]
        public void LanguageLinkRepersentsOpositeOfThreadCulture(
#pragma warning disable xUnit1026 // fakeEnvirnment needs to be frozen here even though it is not noticably used, it is being used on the creation though AutoNSubstituteData
            [Frozen]ICDTSEnvironment fakeEnvironment,
#pragma warning restore xUnit1026 
            IDictionary<string, ICDTSEnvironment> environments,
            ICacheProxy fakeCacheProxy,
            ICurrentRequestProxy fakeCurrentRequestProxy)
        {
            //Start English - link should be french
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(Constants.ENGLISH_CULTURE);
            var sut = new Core(fakeCurrentRequestProxy, fakeCacheProxy, new ConfigurationProxy(), environments);
            sut.ShowLanguageLink = true;
            var json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"lang\":\"fr\"");

            //start french - link should be english
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(Constants.FRENCH_CULTURE);
            sut = new Core(fakeCurrentRequestProxy, fakeCacheProxy, new ConfigurationProxy(), environments);
            sut.ShowLanguageLink = true;
            json = sut.RenderAppTop();
            json.ToString().Should().Contain("\"lang\":\"en\"");
        }

    }
}