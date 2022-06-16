namespace Web.Storages;

public sealed class TokenStorage : ITokenStorage
{
    private readonly ILocalStorage _localStorage;
    private const string TOKEN_STORAGE_KEY = "token";

    public TokenStorage(ILocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<bool> Exists(CancellationToken ct)
    {
        return !string.IsNullOrWhiteSpace(await Get(ct));
    }

    public Task<string?> Get(CancellationToken ct)
    {
        return _localStorage.GetItemAsync<string>(TOKEN_STORAGE_KEY, ct);
    }

    public Task Save(string token, CancellationToken ct)
    {
        return _localStorage.SetItemAsync(TOKEN_STORAGE_KEY, token, ct);
    }

    public Task Remove(CancellationToken ct)
    {
        return _localStorage.RemoveItemAsync(TOKEN_STORAGE_KEY, ct);
    }
}