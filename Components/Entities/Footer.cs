using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GoC.WebTemplate.Components.Entities
{
    public class Footer
    {
        public string CdnEnv { get; set; }
        public string SubTheme { get; set; }
        [JsonProperty(DefaultValueHandling=DefaultValueHandling.Include)]
        public bool ShowFooter { get; set; }
        public List<Link> ContactLinks { get; set; }
        //We have to use custom serializers because privacyLink can be a FooterLink or a list of FooterLink
        [JsonConverter(typeof(FooterLinkConverter))]
        public FooterLinkContext PrivacyLink { get; set; }
        //We have to use custom serializers because termsLink can be a FooterLink or a list of FooterLink
        [JsonConverter(typeof(FooterLinkConverter))]
        public FooterLinkContext TermsLink { get; set; }
        public string LocalPath { get; set; }
        public bool HideFooterMain { get; set; }
        public bool HideFooterCorporate { get; set; }
        public ContextualFooter ContextualFooter { get; set; }

        internal class FooterLinkConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var footerLink = value as FooterLinkContext;

                //this converter should never be on a type that's not a FooterLinkContext so just throw exceptions.
                Debug.Assert(footerLink != null, "The footerLink cannot be null.");

                var listFooterLink = new List<FooterLink> { footerLink.FooterLink };

                if (!footerLink.ShowFooter)
                {
                    serializer.Serialize(writer, listFooterLink);
                    return;
                }

                serializer.Serialize(writer, footerLink.FooterLink);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                //We never deserialie so we shouldn't have to implement this.
                throw new NotImplementedException();
            }

            public override bool CanConvert(Type objectType)
            {
                //We never deserialize so this does not need to be used
                throw new NotImplementedException();
            }
        }
    }
}