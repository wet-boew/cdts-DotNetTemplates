using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FluentAssertions;
using GoC.WebTemplate;
using Xunit;

namespace CoreTest
{
    public class RenderTests
    {
        public void SetupHttpContext()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest(null,"http://foo.bar", "fakeQueryString"),
                new HttpResponse(null));
        }

        [Fact]
        public void RenderLeftMenuTest()
        {
            //Setup
            SetupHttpContext(); 
            var core = new Core();
            core.LeftMenuItems.Add(new MenuSection("SectionName", "SectionLink", new[] {new Link("Href", "Text")}));

            //Execute
            var result = core.RenderLeftMenu();

            //Verify
            result.ToString().Should().Be("sections: [ {sectionName: 'SectionName', sectionLink: 'SectionLink', menuLinks: [{href: 'Href', text: 'Text'},]},]");
        }

        [Fact]
        public void RenderEmptyLeftMenu()
        {
            SetupHttpContext();
            var core = new Core();
            var result = core.RenderLeftMenu();
            result.ToString().Should().BeEmpty();
        }


    }
}
