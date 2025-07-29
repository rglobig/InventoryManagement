namespace InventoryManagement.Domain;

public record InventoryItem(string Name, string? Description, int Quantity, decimal Price)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; private set; } = Name;
    public string? Description { get; private set; } = Description;
    public int Quantity { get; private set; } = Quantity;
    public decimal Price { get; private set; } = Price;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; init; } = DateTimeOffset.UtcNow;

    public void Update(string? name, string? description, int? quantity, decimal? price)
    {
        if (!string.IsNullOrWhiteSpace(name) && name != Name) Name = name;
        if (!string.IsNullOrWhiteSpace(description) && description != Description) Description = description;
        if (quantity.HasValue && quantity.Value != Quantity) Quantity = quantity.Value;
        if (price.HasValue && price.Value != Price) Price = price.Value;
    }
}