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
builder.Services.AddDbContext<InventoryDbContext>(UseSqlite(builder));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static Action<DbContextOptionsBuilder> UseSqlite(WebApplicationBuilder builder)
{
    return options => options.UseSqlite(builder.Configuration.GetConnectionString("InventoryDb"));
}

public partial class Program { }