using dotenv.net;
using InventoryManagement.Api;
using JetBrains.Annotations;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();
app.AddMiddleware();

app.Run();

[UsedImplicitly]
public partial class Program;
