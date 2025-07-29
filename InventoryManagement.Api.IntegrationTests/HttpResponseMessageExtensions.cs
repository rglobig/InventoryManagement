using System.Net;
using FluentAssertions;

namespace InventoryManagement.Api.IntegrationTests;

public static class HttpResponseMessageExtensions
{
    public static void IsOk(this HttpResponseMessage response)
    {
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public static void IsCreated(this HttpResponseMessage response)
    {
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    public static void IsNotFound(this HttpResponseMessage response)
    {
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public static void IsBadRequest(this HttpResponseMessage response)
    {
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    public static void IsNoContent(this HttpResponseMessage response)
    {
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    public static async Task<T> GetContentAsync<T>(this HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<T>() ??
               throw new InvalidOperationException("Response content is null");
    }
}