using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

public interface IInventoryService
{
    ICollection<InventoryItem> GetInventoryItems();
    InventoryItem? GetInventoryItem(Guid id);
    InventoryItem CreateInventoryItem(CreateInventoryItemDto data);
}
