namespace CDTS_Core.WebTemplateCore
{
    public class FooterLink : Link
    {
        public bool NewWindow
        {
            get;
            set;
        }

        public FooterLink()
        {
        }

        public FooterLink(string href, string text, bool newWindow)
            : base(href, text)
        {
            NewWindow = newWindow;
        }
    }

}
