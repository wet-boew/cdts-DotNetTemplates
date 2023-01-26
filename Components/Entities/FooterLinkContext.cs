using System;

namespace GoC.WebTemplate.Components.Entities
{
    /**
     * This class exists because for the Footer class, the PrivacyLink and the TermsLink properties can be both a footer link and a list of footer links
     * This depends on whether the ShowFooter property is true or false
     * Therefore, we have a custom JsonConverter that serializes based on this condition
     * */
    public class FooterLinkContext
    {
        public bool ShowFooter { get; set; }
        public FooterLink FooterLink { get; set; }
    }
}
