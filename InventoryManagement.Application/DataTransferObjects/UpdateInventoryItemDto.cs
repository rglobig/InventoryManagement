namespace InventoryManagement.Application.DataTransferObjects;

public sealed record UpdateInventoryItemDto(string? Name, string? Description, int? Quantity, decimal? Price);