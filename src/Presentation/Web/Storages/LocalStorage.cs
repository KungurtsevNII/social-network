using System.Text.Json;
using Microsoft.JSInterop;

namespace Web.Storages;

public class LocalStorage : ILocalStorage
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorage(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T?> GetItemAsync<T>(string key, CancellationToken ct)
    {
        var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", ct, key);
        if (json is null)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetItemAsync<T>(string key, T value, CancellationToken ct)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", ct, key, JsonSerializer.Serialize(value));
    }

    public async Task RemoveItemAsync(string key, CancellationToken ct)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", ct, key);
    }
}