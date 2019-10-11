using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GoC.WebTemplate.Components.Core.Services
{
    public class ModelAccessor
    {
        public Model Model { get; }

        public ModelAccessor(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
        {
            var configs = new GocWebTemplateConfigurationSection();

            var configBuilder =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

            configBuilder.GetSection("GoCWebTemplate").Bind(configs);

            var settings = new WebTemplateSettings(configs);

            Model = 
                new Model(
                    new FileContentMemoryCacheProvider(memoryCache, hostingEnvironment),
                    settings, 
                    new CdtsMemoryCacheProvider(memoryCache),
                    httpContextAccessor.HttpContext.Request.QueryString.ToString()
                );
        }
    }
}
