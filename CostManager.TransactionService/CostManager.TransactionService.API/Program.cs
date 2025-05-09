using System.Reflection;
using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.API.Extensions;
using CostManager.TransactionService.DB;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    var appAssembly = Assembly.Load(new AssemblyName(builder.Environment.ApplicationName));
    if (appAssembly != null)
    {
        builder.Configuration.AddUserSecrets(appAssembly, optional: true);
    }
}

builder.AddKeyVault();

// Add services to the container.
builder.Services.AddCosmosDb();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();
builder.Services.AddSingleton<ITransactionRepository, CosmosDbTransactionsRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
