namespace InventoryManagement.Domain.Tests;

public class InventoryItemTests
{
    [Fact]
    void InventoryItem_Update()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var newName = "Pixel";
        var newDescription = "Android";
        var newQuantity = 2;
        var newPrice = 800;
        item.Update(newName, newDescription, newQuantity, newPrice);
        Assert.Equal(newName, item.Name);
        Assert.Equal(newDescription, item.Description);
        Assert.Equal(newQuantity, item.Quantity);
        Assert.Equal(newPrice, item.Price);
    }
}
