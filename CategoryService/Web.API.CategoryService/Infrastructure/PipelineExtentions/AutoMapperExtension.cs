using Mappers.MappingProfiles;

namespace CategoryService.Infrastructure.PipelineExtentions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CategoryProfile>();
            });

            return services;
        }
    }
}
