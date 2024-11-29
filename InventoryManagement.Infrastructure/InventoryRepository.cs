using InventoryManagement.Application.Repositories;
using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure;

public class InventoryRepository(InventoryDbContext dbContext) : IInventoryRepository
{
    public async ValueTask<InventoryItem> CreateInventoryItem(InventoryItem item, CancellationToken cancellationToken)
    {
        var entity = dbContext.InventoryItems.Add(item);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    public async ValueTask DeleteInventoryItem(InventoryItem item, CancellationToken cancellationToken)
    {
        dbContext.InventoryItems.Remove(item);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask<InventoryItem?> GetInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.InventoryItems.FindAsync(id, cancellationToken);
    }

    public async Task<ICollection<InventoryItem>> GetInventoryItems(CancellationToken cancellationToken)
    {
        var items = await dbContext.InventoryItems
            .OrderBy(item => item.Name)
            .ToListAsync(cancellationToken);
        return items;
    }
}
