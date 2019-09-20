using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace GoC.WebTemplate.Components.Framework.Tests
{
    internal class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(() => new Fixture().Customize(new WebTemplateCustomization()))
        { }
    }

    public class WebTemplateCustomization : CompositeCustomization
    {
        public WebTemplateCustomization()
            : base(new CoreCustomization(), new AutoNSubstituteCustomization())
        { }
    }

    public class CoreCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
        }
    }
}