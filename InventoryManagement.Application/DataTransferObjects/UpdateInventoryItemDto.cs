namespace InventoryManagement.Application.DataTransferObjects;

public record UpdateInventoryItemDto(string? Name, string? Description, int? Quantity, decimal? Price);