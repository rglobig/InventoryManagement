namespace InventoryManagement.Api;

public interface IUriProvider
{
    Uri GetRequestUriWithId(HttpRequest request, Guid id);
}