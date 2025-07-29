using Bogus;
using InventoryManagement.Domain;

namespace InventoryManagement.Api.IntegrationTests;

public sealed class InventoryItemFaker : Faker<InventoryItem>
{
    public InventoryItemFaker()
    {
        CustomInstantiator(_ => new InventoryItem("", null, 0, 0));
        RuleFor(i => i.Name, f => f.Name.FullName());
        RuleFor(i => i.Description, f => f.Lorem.Sentence());
        RuleFor(i => i.Quantity, f => f.Random.Int(1, 100));
        RuleFor(i => i.Price, f => f.Random.Decimal(1, 1000));
    }
}