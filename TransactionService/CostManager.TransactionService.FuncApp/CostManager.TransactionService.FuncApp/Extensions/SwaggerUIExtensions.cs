using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CostManager.TransactionService.FuncApp.Extensions
{
    public static class SwaggerUIExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
            {
                var options = new OpenApiConfigurationOptions()
                {
                    Info = new OpenApiInfo()
                    {
                        Version = DefaultOpenApiConfigurationOptions.GetOpenApiDocVersion(),
                        Title = $"{DefaultOpenApiConfigurationOptions.GetOpenApiDocTitle()} (Injected)",
                        Description = DefaultOpenApiConfigurationOptions.GetOpenApiDocDescription()
                    }
                };

                return options;
            });

            return services;
        }
    }
}
