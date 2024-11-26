using FluentAssertions;
using InventoryManagement.Api.Controllers;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryManagement.Api.Tests;

public class InventoryControllerTests
{
    [Fact]
    public void GetAllInventoryItems()
    {
        List<InventoryItem> inventory = [new InventoryItem("iPhone", "Smartphone", 1, 1000)];
        var mock = new Mock<IInventoryService>();
        mock.Setup(s => s.GetInventoryItems()).Returns(inventory);
        var controller = new InventoryController(mock.Object);

        var actionResult = controller.GetAllInventoryItems();

        var result = actionResult.Result as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().BeEquivalentTo(inventory);
    }
}
