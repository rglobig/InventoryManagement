using Microsoft.AspNetCore.Mvc.Testing;

namespace InventoryManagement.Api.IntegrationTests;

public class InventoryControllerIntegrationTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetAllInventoryItems()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync(@"api/Inventory");

        response.EnsureSuccessStatusCode();
    }
}