using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using InventoryManagement.Application;
using InventoryManagement.Infrastructure;

namespace InventoryManagement.Api;

public static class Dependencies
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiDependencies();
        services.AddApplicationDependencies();
        services.AddInfrastructureDependencies(configuration);
    }

    private static void AddApiDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddApiVersioning(AddVersioning)
            .AddApiExplorer(AddVersionApiExplorer);
    }

    private static void AddVersioning(ApiVersioningOptions option)
    {
        option.AssumeDefaultVersionWhenUnspecified = true;
        option.DefaultApiVersion = new ApiVersion(1, 0);
        option.ReportApiVersions = true;
        option.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("X-Version"),
            new MediaTypeApiVersionReader("ver"));
    }

    private static void AddVersionApiExplorer(ApiExplorerOptions options)
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    }

    public static void AddMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
    }
}