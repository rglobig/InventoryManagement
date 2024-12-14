using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using InventoryManagement.Application.Repositories;
using InventoryManagement.Application.Services;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using dotenv.net;

DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddTransient<IInventoryService, InventoryService>();
builder.Services.AddDbContext<InventoryDbContext>(UseNpgsql(builder));
builder.Services.AddApiVersioning(AddVersioning).AddApiExplorer(AddVersionApiExplorer);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

static Action<DbContextOptionsBuilder> UseNpgsql(WebApplicationBuilder builder)
{
    var host = builder.Configuration["DB_HOST"];
    var port = builder.Configuration["DB_PORT"];
    var username = builder.Configuration["DB_USERNAME"];
    var password = builder.Configuration["DB_PASSWORD"];
    var name = builder.Configuration["DB_NAME"];
    var defaultConnection = $"Host={host};Port={port};UserName={username};Password={password};Database={name}";
    Console.WriteLine($"Connection string: {defaultConnection}");
    return options => options.UseNpgsql(defaultConnection);
}

static void AddVersioning(ApiVersioningOptions option)
{
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.ReportApiVersions = true;
    option.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
}

static void AddVersionApiExplorer(ApiExplorerOptions options)
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
}

public partial class Program { }