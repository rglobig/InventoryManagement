using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Tests;

public static class ActionResultExtensions
{
    public static OkObjectResult IsOk<T>(this ActionResult<T> actionResult)
    {
        var result = actionResult.Result as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().NotBeNull();
        return result;
    }

    public static void IsNotFound<T>(this ActionResult<T> actionResult)
    {
        var result = actionResult.Result as NotFoundResult;
        result.Should().NotBeNull();
    }

    public static void IsBadRequest<T>(this ActionResult<T> actionResult)
    {
        var result = actionResult.Result as BadRequestObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().NotBeNull();
    }

    public static void IsBadRequest(this ActionResult actionResult)
    {
        var result = actionResult as BadRequestObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().NotBeNull();
    }

    public static CreatedResult IsCreated<T>(this ActionResult<T> actionResult)
    {
        var result = actionResult.Result as CreatedResult;
        result.Should().NotBeNull();
        result!.Value.Should().NotBeNull();
        return result;
    }

    public static void IsNoContent(this ActionResult actionResult)
    {
        var result = actionResult as NoContentResult;
        result.Should().NotBeNull();
    }

    public static void AndShouldBe<T>(this OkObjectResult result, T expected)
    {
        result.Value.Should().BeEquivalentTo(expected);
    }

    public static void AndShouldBe<T>(this CreatedResult result, T expected)
    {
        result.Value.Should().BeEquivalentTo(expected);
    }
}