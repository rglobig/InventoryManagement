using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Repositories;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

public class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    public async Task<ICollection<InventoryItem>> GetInventoryItems(CancellationToken cancellationToken)
    {
        return await inventoryRepository.GetInventoryItems(cancellationToken);
    }

    public async ValueTask<InventoryItem?> GetInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        return await inventoryRepository.GetInventoryItem(id, cancellationToken);
    }

    public async ValueTask<InventoryItem> CreateInventoryItem(CreateInventoryItemDto data, CancellationToken cancellationToken)
    {
        return await inventoryRepository.CreateInventoryItem(data.ToInventoryItem(), cancellationToken);
    }

    public async ValueTask<InventoryItem?> UpdateInventoryItem(Guid id, UpdateInventoryItemDto data, CancellationToken cancellationToken)
    {
        var inventoryItem = await GetInventoryItem(id, cancellationToken);

        if (inventoryItem == null) return null;

        inventoryItem.Update(data.Name, data.Description, data.Quantity, data.Price);

        return inventoryItem;
    }

    public async ValueTask<InventoryItem?> DeleteInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        var inventoryItem = await GetInventoryItem(id, cancellationToken);

        if (inventoryItem == null) return null;

        await inventoryRepository.DeleteInventoryItem(inventoryItem, cancellationToken);

        return inventoryItem;
    }
}
