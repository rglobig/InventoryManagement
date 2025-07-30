using InventoryManagement.Api.Controllers;
using InventoryManagement.Application;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain;
using Moq;

namespace InventoryManagement.Api.Tests;

public class InventoryControllerTests
{
    private readonly InventoryController _controller;
    private readonly List<InventoryItem> _inventory = [new("iPhone", "Smartphone", 1, 1000)];
    private readonly Mock<IInventoryService> _inventoryServiceMock;

    public InventoryControllerTests()
    {
        _inventoryServiceMock = new Mock<IInventoryService>();
        _inventoryServiceMock.Setup(s => s.GetInventoryItems(CancellationToken.None))
            .ReturnsAsync(Result.Success(InventoryDtos));
        _controller = new InventoryController(_inventoryServiceMock.Object);
    }

    private IReadOnlyList<InventoryItemDto> InventoryDtos => _inventory.Select(InventoryItemDto.From).ToList();

    [Fact]
    private async Task Get_All_InventoryItems_Returns_OK()
    {
        var token = CancellationToken.None;

        var actionResult = await _controller.GetAllInventoryItemsAsync(token);

        actionResult.IsOk().AndShouldBe(InventoryDtos);
        _inventoryServiceMock.Verify(s => s.GetInventoryItems(token), Times.Once);
    }
}
