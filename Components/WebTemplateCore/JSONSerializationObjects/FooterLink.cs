// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components
{
    public class FooterLink : Link
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