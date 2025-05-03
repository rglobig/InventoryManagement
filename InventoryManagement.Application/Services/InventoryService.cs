using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.DataTransferObjects.Validation;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

public class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    public async Task<Result<IReadOnlyList<InventoryItemDto>>> GetInventoryItems(CancellationToken cancellationToken)
    {
        var items = await inventoryRepository.GetInventoryItems(cancellationToken);
        IReadOnlyList<InventoryItemDto> dtos = items.Select(InventoryItemDto.From).ToList();
        return Result<IReadOnlyList<InventoryItemDto>>.Success(dtos);
    }

    public async Task<Result<InventoryItemDto>> GetInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        var item = await inventoryRepository.GetInventoryItem(id, cancellationToken);
        return item is null ? 
            Result<InventoryItemDto>.FailedToFindItem() : 
            Result<InventoryItemDto>.Success(InventoryItemDto.From(item));
    }

    public async Task<Result<InventoryItemDto>> CreateInventoryItem(CreateInventoryItemDto data, CancellationToken cancellationToken)
    {
        var validationResult = await new CreateInventoryItemDtoValidator().ValidateAsync(data, cancellationToken);
        if (!validationResult.IsValid) return Result<InventoryItemDto>.FailedToValidate<CreateInventoryItemDto>();
        
        var item = await inventoryRepository.CreateInventoryItem(data.ToInventoryItem(), cancellationToken);
        return Result<InventoryItemDto>.Success(InventoryItemDto.From(item));
    }

    public async Task<Result<InventoryItemDto>> UpdateInventoryItem(Guid id, UpdateInventoryItemDto data, CancellationToken cancellationToken)
    {
        var validationResult = await new UpdateInventoryItemDtoValidator().ValidateAsync(data, cancellationToken);
        if (!validationResult.IsValid) return Result<InventoryItemDto>.FailedToValidate<UpdateInventoryItemDto>();
        
        var item = await inventoryRepository.GetInventoryItem(id, cancellationToken);
        if(item is null) return Result<InventoryItemDto>.FailedToFindItem();
        
        item.Update(data.Name, data.Description, data.Quantity, data.Price);

        return Result<InventoryItemDto>.Success(InventoryItemDto.From(item));
    }

    public async Task<Result<InventoryItemDto>> DeleteInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        var item = await inventoryRepository.GetInventoryItem(id, cancellationToken);
        if(item is null) return Result<InventoryItemDto>.FailedToFindItem();

        await inventoryRepository.DeleteInventoryItem(item, cancellationToken);

        return Result<InventoryItemDto>.Success(InventoryItemDto.From(item));
    }
}
