using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Repositories;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

public class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    public ICollection<InventoryItem> GetInventoryItems()
    {
        return inventoryRepository.GetInventoryItems();
    }

    public InventoryItem? GetInventoryItem(Guid id)
    {
        return inventoryRepository.GetInventoryItem(id);
    }

    public InventoryItem CreateInventoryItem(CreateInventoryItemDto data)
    {
        return inventoryRepository.CreateInventoryItem(data.ToInventoryItem());
    }

    public bool TryUpdateInventoryItem(Guid id, UpdateInventoryItemDto data, out InventoryItem? inventoryItem)
    {
        inventoryItem = GetInventoryItem(id);

        if (inventoryItem == null) return false;

        inventoryItem.Update(data.Name, data.Description, data.Quantity, data.Price);
        return true;
    }

    public bool TryDeleteInventoryItem(Guid id, out InventoryItem? inventoryItem)
    {
        inventoryItem = GetInventoryItem(id);

        if (inventoryItem == null) return false;

        inventoryRepository.DeleteInventoryItem(inventoryItem);
        return true;
    }
}
