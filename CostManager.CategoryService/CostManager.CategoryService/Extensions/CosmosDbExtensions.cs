using CostManager.CategoryService.Abstracts.Interfaces.Data;
using CostManager.CategoryService.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CostManager.CategoryService.Extensions;

public static class CosmosDbExtensions
{
    public static void AddCosmosDb(this IServiceCollection services)
    {
        services.AddSingleton(s =>
        {
            var configuration = services
                .BuildServiceProvider()
                .GetRequiredService<IConfiguration>();
            
            string connectionString = configuration.GetValue<string>("cost-manager-common-db-connection-string");

            return new CosmosClient(
                connectionString: connectionString
            );
        });
        services.AddSingleton<ICategoryRepository, CosmosDbRepository>();
    }
}