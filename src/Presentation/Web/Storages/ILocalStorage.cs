namespace Web.Storages;

public interface ILocalStorage
{
    Task<T?> GetItemAsync<T>(string key, CancellationToken ct);
    Task SetItemAsync<T>(string key, T value, CancellationToken ct);
    Task RemoveItemAsync(string key, CancellationToken ct);
}