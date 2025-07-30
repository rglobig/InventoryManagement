using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.DataTransferObjects.Validation;
using InventoryManagement.Domain;

namespace InventoryManagement.Application.Services;

internal sealed class InventoryService(
    IInventoryRepository inventoryRepository,
    CreateInventoryItemDtoValidator createInventoryItemDtoValidator,
    UpdateInventoryItemDtoValidator updateInventoryItemDtoValidator) : IInventoryService
{
    public async Task<Result<IReadOnlyList<InventoryItemDto>>> GetInventoryItems(CancellationToken cancellationToken)
    {
        var items = await inventoryRepository.GetInventoryItems(cancellationToken);
        IReadOnlyList<InventoryItemDto> dtos = items.Select(InventoryItemDto.From).ToList();
        return Result.Success(dtos);
    }

    public async Task<Result<InventoryItemDto>> GetInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        var item = await inventoryRepository.GetInventoryItem(id, cancellationToken);
        return item is null
            ? Result.FailedToFindItem<InventoryItemDto>()
            : Result.Success(InventoryItemDto.From(item));
    }

    public async Task<Result<InventoryItemDto>> CreateInventoryItem(CreateInventoryItemDto data,
        CancellationToken cancellationToken)
    {
        var validationResult = await createInventoryItemDtoValidator.ValidateAsync(data, cancellationToken);
        if (!validationResult.IsValid) return Result.FailedToValidate<CreateInventoryItemDto, InventoryItemDto>();

        var item = await inventoryRepository.CreateInventoryItem(data.ToInventoryItem(), cancellationToken);
        return Result.Success(InventoryItemDto.From(item));
    }

    public async Task<Result<InventoryItemDto>> UpdateInventoryItem(Guid id, UpdateInventoryItemDto data,
        CancellationToken cancellationToken)
    {
        var validationResult = await updateInventoryItemDtoValidator.ValidateAsync(data, cancellationToken);
        if (!validationResult.IsValid) return Result.FailedToValidate<UpdateInventoryItemDto, InventoryItemDto>();

        var item = await inventoryRepository.GetInventoryItem(id, cancellationToken);
        if (item is null) return Result.FailedToFindItem<InventoryItemDto>();

        item.Update(data.Name, data.Description, data.Quantity, data.Price);

        return Result.Success(InventoryItemDto.From(item));
    }

    public async Task<Result<InventoryItemDto>> DeleteInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        var item = await inventoryRepository.GetInventoryItem(id, cancellationToken);
        if (item is null) return Result.FailedToFindItem<InventoryItemDto>();

        await inventoryRepository.DeleteInventoryItem(item, cancellationToken);

        return Result.Success(InventoryItemDto.From(item));
    }
}