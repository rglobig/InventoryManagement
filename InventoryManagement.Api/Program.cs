using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using InventoryManagement.Application.Repositories;
using InventoryManagement.Application.Services;
using InventoryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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
    return options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
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