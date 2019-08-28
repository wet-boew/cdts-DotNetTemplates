using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using GoC.WebTemplate.Components.Entities;
using GoC.WebTemplate.Components.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoC.WebTemplate.Components.Core.Services
{
    public class ModelAccessor
    {
        public Model Model { get; }

        public ModelAccessor(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            Model = 
                new Model(
                    new FileContentMemoryCacheProvider(memoryCache), 
                    new ConfigurationProxy(), 
                    new CdtsMemoryCacheProvider(memoryCache)
                );

            //set the language link according to the culture
            Model.LanguageLink = new LanguageLink
            {
                Href = ModelBuilder.BuildLanguageLinkURL(httpContextAccessor.HttpContext.Request.QueryString.ToString())
            };

            //set timeout based on session
            Model.SessionTimeout.CheckWithServerSessionTimeout(httpContextAccessor.HttpContext.Request.HttpContext.Session);
        }
    }
}
