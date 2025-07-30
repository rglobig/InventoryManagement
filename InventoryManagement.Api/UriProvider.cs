using Microsoft.AspNetCore.Http.Extensions;

namespace InventoryManagement.Api;

public class UriProvider : IUriProvider
{
    public Uri GetRequestUriWithId(HttpRequest request, Guid id)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.", nameof(id));

        return new Uri(Path.Combine(GetUrl(request), id.ToString()), UriKind.Absolute);
    }

    protected static string GetUrl(HttpRequest request)
    {
        return request.GetEncodedUrl();
    }
}