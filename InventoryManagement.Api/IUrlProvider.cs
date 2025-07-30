using Microsoft.AspNetCore.Http.Extensions;

namespace InventoryManagement.Api;

public interface IUriProvider
{
    string GetUrl(HttpRequest request);
    Uri GetRequestUriWithId(HttpRequest request, Guid id);
}

public sealed class UriProvider : IUriProvider
{
    public string GetUrl(HttpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request.GetEncodedUrl();
    }

    public Uri GetRequestUriWithId(HttpRequest request, Guid id)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.", nameof(id));

        return new Uri(Path.Combine(GetUrl(request), id.ToString()), UriKind.Absolute);
    }
}