namespace InventoryManagement.Domain;

public interface IInventoryRepository
{
    Task<IReadOnlyList<InventoryItem>> GetInventoryItems(CancellationToken cancellationToken);
    Task<InventoryItem?> GetInventoryItem(Guid id, CancellationToken cancellationToken);
    Task<InventoryItem> CreateInventoryItem(InventoryItem item, CancellationToken cancellationToken);
    Task DeleteInventoryItem(InventoryItem item, CancellationToken cancellationToken);
}
