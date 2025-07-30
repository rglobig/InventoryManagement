using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Tests;

public static class ActionResultExtensions
{
    public static OkObjectResult IsOk<T>(this ActionResult<T> actionResult) where T : class
    {
        var result = actionResult.Result as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().NotBeNull();
        return result;
    }

    public static void AndShouldBe<T>(this OkObjectResult result, T expected)
    {
        result.Value.Should().BeEquivalentTo(expected);
    }
}