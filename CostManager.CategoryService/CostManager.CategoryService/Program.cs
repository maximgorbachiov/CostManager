using CostManager.CategoryService.Abstracts.Interfaces.Business;
using CostManager.CategoryService.Abstracts.Interfaces.Data;
using CostManager.CategoryService.BusinessLogic.Services;
using CostManager.CategoryService.Repositories;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddSingleton<ICategoryRepository, InMemoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();