// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components
{
    public class FooterLink : Link, IFooterSection
    {
        public FooterLink()
        {
            
        }
        public FooterLink(string href, string text, bool newWindow) : base(href, text)
        {

            NewWindow = newWindow;
        }

        public bool NewWindow { get; set; }
    }
}