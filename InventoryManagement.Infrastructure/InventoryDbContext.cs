using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure;

internal sealed class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public DbSet<InventoryItem> InventoryItems { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(item => item.Id);
            entity.Property(item => item.Description).HasMaxLength(500);
            entity.Property(item => item.CreatedAt).ValueGeneratedOnAdd();
            entity.Property(item => item.UpdatedAt).ValueGeneratedOnUpdate();
        });
    }
}
