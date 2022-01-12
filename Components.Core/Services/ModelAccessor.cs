using GoC.WebTemplate.Components.Configs;
using GoC.WebTemplate.Components.Configs.Schemas;
using GoC.WebTemplate.Components.Core.Utils.Caching;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GoC.WebTemplate.Components.Core.Services
{
    public class ModelAccessor : IModelAccessor
    {
        public IModel Model { get; }

        public ModelAccessor(IMemoryCache memoryCache, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            var configs = new GocWebTemplateConfigurationSection();

            configuration.GetSection("GoCWebTemplate").Bind(configs);

            var settings = new WebTemplateSettings(configs);

            Model = 
                new Model(
                    new FileContentMemoryCacheProvider(memoryCache, hostingEnvironment),
                    settings, 
                    new CdtsMemoryCacheProvider(memoryCache)
                );
        }
    }
}
