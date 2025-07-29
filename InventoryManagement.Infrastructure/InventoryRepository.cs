using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure;

public class InventoryRepository(InventoryDbContext dbContext) : IInventoryRepository
{
    public async Task<InventoryItem> CreateInventoryItem(InventoryItem item, CancellationToken cancellationToken)
    {
        var entity = dbContext.InventoryItems.Add(item);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    public async Task DeleteInventoryItem(InventoryItem item, CancellationToken cancellationToken)
    {
        dbContext.InventoryItems.Remove(item);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<InventoryItem?> GetInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.InventoryItems.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<InventoryItem>> GetInventoryItems(CancellationToken cancellationToken)
    {
        var items = await dbContext.InventoryItems
            .OrderBy(item => item.Name)
            .ToListAsync(cancellationToken);
        return items;
    }
}