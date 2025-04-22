using CostManager.CategoryService.Abstracts.Interfaces.Business;
using CostManager.CategoryService.Abstracts.Interfaces.Data;
using CostManager.CategoryService.BusinessLogic.Services;
using CostManager.CategoryService.Extensions;
using CostManager.CategoryService.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration((_, configBuilder) =>
    {
        configBuilder
            .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "local.settings.json"), optional: true, reloadOnChange: false)
            .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "settings.json"), optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .AddKeyVault();
    })
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        //services.AddSingleton<ICategoryRepository, InMemoryRepository>();
        services.AddCosmosDb();
        services.AddScoped<ICategoryService, CategoryService>();
    })
    .Build();

host.Run();
