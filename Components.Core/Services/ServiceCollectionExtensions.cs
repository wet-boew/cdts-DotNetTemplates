using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace GoC.WebTemplate.Components.Core.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddModelAccessor(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.TryAdd(ServiceDescriptor.Scoped<IModelAccessor, ModelAccessor>());
        }
    }
}
