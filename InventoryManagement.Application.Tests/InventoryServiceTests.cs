using FluentAssertions;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain;
using Moq;

namespace InventoryManagement.Application.Tests;

public class InventoryServiceTests
{
    [Fact]
    private async Task GetInventoryItems_WithOneItem()
    {
        List<InventoryItem> inventory = [new("iPhone", "Smartphone", 1, 1000)];
        var mock = new Mock<IInventoryRepository>();
        var token = CancellationToken.None;
        mock.Setup(r => r.GetInventoryItems(token)).ReturnsAsync(inventory);
        var service = new InventoryService(mock.Object);

        var result = await service.GetInventoryItems(token);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(inventory.Select(InventoryItemDto.From).ToList());
        mock.Verify(r => r.GetInventoryItems(token), Times.Once);
    }

    [Fact]
    private async Task GetInventoryItem()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;
        var mock = new Mock<IInventoryRepository>();
        var token = CancellationToken.None;
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.GetInventoryItem(id, token);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(InventoryItemDto.From(item));
        mock.Verify(r => r.GetInventoryItem(id, token), Times.Once);
    }

    [Fact]
    private async Task CreateInventoryItem()
    {
        var input = new CreateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        var item = input.ToInventoryItem();

        var mock = new Mock<IInventoryRepository>();
        var token = CancellationToken.None;
        mock.Setup(r => r.CreateInventoryItem(It.IsAny<InventoryItem>(), token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.CreateInventoryItem(input, token);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(InventoryItemDto.From(item));
        mock.Verify(r => r.CreateInventoryItem(It.IsAny<InventoryItem>(), token), Times.Once);
    }

    [Fact]
    private async Task TryUpdateInventoryItem_FoundItem()
    {
        var input = new UpdateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;

        var mock = new Mock<IInventoryRepository>();
        var token = CancellationToken.None;
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.UpdateInventoryItem(id, input, token);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(InventoryItemDto.From(item));
    }

    [Fact]
    private async Task TryUpdateInventoryItem_FoundNoItem()
    {
        var input = new UpdateInventoryItemDto("iPhone", "Smartphone", 1, 1000);
        InventoryItem item = null!;
        var id = Guid.NewGuid();

        var mock = new Mock<IInventoryRepository>();
        var token = CancellationToken.None;
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.UpdateInventoryItem(id, input, token);

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    private async Task TryDeleteInventoryItem_FoundItem()
    {
        var item = new InventoryItem("iPhone", "Smartphone", 1, 1000);
        var id = item.Id;

        var mock = new Mock<IInventoryRepository>();
        var token = CancellationToken.None;
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.DeleteInventoryItem(id, token);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(InventoryItemDto.From(item));
        mock.Verify(r => r.DeleteInventoryItem(item, token), Times.Once);
    }

    [Fact]
    private async Task TryDeleteInventoryItem_FoundNoItem()
    {
        InventoryItem item = null!;
        var id = Guid.NewGuid();

        var mock = new Mock<IInventoryRepository>();
        var token = CancellationToken.None;
        mock.Setup(r => r.GetInventoryItem(id, token)).ReturnsAsync(item);
        var service = new InventoryService(mock.Object);

        var result = await service.DeleteInventoryItem(id, token);

        mock.Verify(r => r.DeleteInventoryItem(item, token), Times.Never);
        result.IsSuccess.Should().BeFalse();
    }
}