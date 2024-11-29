using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

public interface IInventoryService
{
    Task<ICollection<InventoryItem>> GetInventoryItems(CancellationToken cancellationToken);
    ValueTask<InventoryItem?> GetInventoryItem(Guid id, CancellationToken cancellationToken);
    ValueTask<InventoryItem> CreateInventoryItem(CreateInventoryItemDto data, CancellationToken cancellationToken);
    ValueTask<InventoryItem?> UpdateInventoryItem(Guid id, UpdateInventoryItemDto data, CancellationToken cancellationToken);
    ValueTask<InventoryItem?> DeleteInventoryItem(Guid id, CancellationToken cancellationToken);
}
