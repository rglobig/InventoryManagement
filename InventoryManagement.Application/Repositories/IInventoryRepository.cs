using InventoryManagement.Domain;

namespace InventoryManagement.Application.Repositories;

public interface IInventoryRepository
{
    Task<ICollection<InventoryItem>> GetInventoryItems(CancellationToken cancellationToken);
    ValueTask<InventoryItem?> GetInventoryItem(Guid id, CancellationToken cancellationToken);
    ValueTask<InventoryItem> CreateInventoryItem(InventoryItem item, CancellationToken cancellationToken);
    ValueTask DeleteInventoryItem(InventoryItem item, CancellationToken cancellationToken);
}
