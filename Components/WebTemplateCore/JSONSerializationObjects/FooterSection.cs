﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace GoC.WebTemplate.Components
{
    [Serializable]
    public class FooterSection : IFooterSection
    {
        public FooterSection(List<FooterLink> customFooterLinks)
        {
            CustomFooterLinks = customFooterLinks;
        }

        public FooterSection()
        {
            CustomFooterLinks = new List<FooterLink>();
        }

        public string SectionName { get; set; }

        [JsonProperty(PropertyName="customFooterLinks")]
        public List<FooterLink> CustomFooterLinks { get; set; }
    }
}