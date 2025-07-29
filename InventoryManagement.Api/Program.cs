using dotenv.net;
using InventoryManagement.Api;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();
app.AddMiddleware();

app.Run();

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program
{
}