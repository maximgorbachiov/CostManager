using AutoMapper;
using Mappers.MappingProfiles;

namespace Mappers
{
    public static class DomainModelsMapper
    {
        public static MapperConfiguration GetDomainMappings()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CategoryProfile>();
            });

            return configuration;
        }
    }
}