using FluentAssertions;
using InventoryManagement.Application.DataTransferObjects;
using InventoryManagement.Domain;
using Microsoft.AspNetCore.Mvc.Testing;

namespace InventoryManagement.Api.IntegrationTests;

public class InventoryControllerIntegrationTests(
    WebApplicationFactory<Program> factory,
    InventoryItemFaker inventoryItemFaker) : IntegrationTestBase(factory), IClassFixture<InventoryItemFaker>
{
    private const string BaseUrl = @"api/v1.0/Inventory";
    private readonly InventoryItemDto _createdDto = new(Guid.Empty, "iPhone", "Smartphone", 1, 1000);
    private readonly CreateInventoryItemDto _createDto = new("iPhone", "Smartphone", 1, 1000);
    private readonly InventoryItemDto _updatedDto = new(Guid.Empty, "Huawei", "Smartphone", 3, 500);
    private readonly UpdateInventoryItemDto _updateDto = new("Huawei", "Smartphone", 3, 500);
    private List<InventoryItem> _inventoryItems = null!;

    private List<InventoryItemDto> GetInventoryItemsAsDtos()
    {
        return _inventoryItems.Select(InventoryItemDto.From).ToList();
    }

    private static string BaseUrlWithId(Guid id)
    {
        return $"{BaseUrl}/{id}";
    }

    private InventoryItem GetRandomItem()
    {
        return _inventoryItems[Random.Shared.Next(_inventoryItems.Count)];
    }

    protected override async Task SeedDatabase(IServiceProvider serviceProvider)
    {
        _inventoryItems = inventoryItemFaker.Generate(50);

        var inventoryRepository = serviceProvider.GetRequiredService<IInventoryRepository>();
        foreach (var item in _inventoryItems)
            await inventoryRepository.CreateInventoryItem(item, CancellationToken.None);
    }

    [Fact]
    public async Task Create_InventoryItem_Returns_Created()
    {
        var response = await PostAsync(BaseUrl, _createDto);

        response.IsCreated();
        var data = await response.GetContentAsync<InventoryItemDto>();
        data.Should().BeEquivalentTo(_createdDto with { Id = data.Id });
    }

    [Fact]
    public async Task Get_All_InventoryItems_Returns_Correct_Collection()
    {
        var response = await GetAsync(BaseUrl);

        response.IsOk();
        var dtos = await response.GetContentAsync<ICollection<InventoryItemDto>>();
        GetInventoryItemsAsDtos().Should().BeEquivalentTo(dtos);
    }

    [Fact]
    public async Task Get_InventoryItem_With_Wrong_Id_Returns_NotFound()
    {
        var response = await GetAsync(BaseUrlWithId(Guid.Empty));

        response.IsNotFound();
    }

    [Fact]
    public async Task Get_InventoryItem_With_Correct_Id_Returns_OK()
    {
        var item = GetRandomItem();

        var response = await GetAsync(BaseUrlWithId(item.Id));

        response.IsOk();
        var data = await response.GetContentAsync<InventoryItemDto>();
        data.Should().BeEquivalentTo(InventoryItemDto.From(item));
    }

    [Fact]
    public async Task Update_InventoryItem_Returns_OK()
    {
        var id = GetRandomItem().Id;

        var response = await PatchAsync(BaseUrlWithId(id), _updateDto);

        response.IsOk();
        var data = await response.GetContentAsync<InventoryItemDto>();
        data.Should().BeEquivalentTo(_updatedDto with { Id = id });
    }

    [Fact]
    public async Task Update_InventoryItem_With_Wrong_Id_Returns_BadRequest()
    {
        var response = await PatchAsync(BaseUrlWithId(Guid.Empty), _updateDto);

        response.IsBadRequest();
    }

    [Fact]
    public async Task Delete_InventoryItem_Returns_NoContent()
    {
        var id = GetRandomItem().Id;

        var response = await DeleteAsync(BaseUrlWithId(id));

        response.IsNoContent();
    }

    [Fact]
    public async Task Delete_InventoryItem_With_Wrong_Id_Returns_BadRequest()
    {
        var response = await DeleteAsync(BaseUrlWithId(Guid.Empty));

        response.IsBadRequest();
    }
}
