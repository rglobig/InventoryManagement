using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace InventoryManagement.Api.Tests;

public class UriProviderTests : UriProvider
{
    [Fact]
    public void GetRequestUriWithId_ShouldReturn_ValidUri()
    {
        var (request, expectedUrl) = CreateRequest();
        var id = Guid.NewGuid();

        var uri = GetRequestUriWithId(request, id);

        uri.Should().Be(new Uri(Path.Combine(expectedUrl, id.ToString()), UriKind.Absolute));
    }

    [Fact]
    public void GetRequestUriWithId_ShouldThrow_ArgumentNullException_WhenRequestIsNull()
    {
        Action act = () => GetRequestUriWithId(null!, Guid.NewGuid());

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GetRequestUriWithId_ShouldThrow_ArgumentNullException_WhenIdIsEmpty()
    {
        var (request, _) = CreateRequest();

        Action act = () => GetRequestUriWithId(request, Guid.Empty);

        act.Should().Throw<ArgumentException>().WithMessage("Id cannot be empty. (Parameter 'id')");
    }

    [Fact]
    public void GetUrl_ShouldReturn_EncodedUrl()
    {
        var (request, expectedUrl) = CreateRequest();

        var url = GetUrl(request);

        url.Should().Be(expectedUrl);
    }

    private static (HttpRequest request, string url) CreateRequest()
    {
        var request = new DefaultHttpContext().Request;
        request.Scheme = "http";
        request.Host = new HostString("localhost", 5000);
        request.Path = "/api/inventory";

        return (request, request.GetEncodedUrl());
    }
}