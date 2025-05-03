using InventoryManagement.Application.DataTransferObjects;

namespace InventoryManagement.Application.Services;

public interface IInventoryService
{
    Task<Result<IReadOnlyList<InventoryItemDto>>> GetInventoryItems(CancellationToken cancellationToken);
    Task<Result<InventoryItemDto>> GetInventoryItem(Guid id, CancellationToken cancellationToken);
    Task<Result<InventoryItemDto>> CreateInventoryItem(CreateInventoryItemDto data, CancellationToken cancellationToken);
    Task<Result<InventoryItemDto>> UpdateInventoryItem(Guid id, UpdateInventoryItemDto data, CancellationToken cancellationToken);
    Task<Result<InventoryItemDto>> DeleteInventoryItem(Guid id, CancellationToken cancellationToken);
}
