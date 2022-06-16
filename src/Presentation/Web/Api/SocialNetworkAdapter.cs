using System.Net.Http.Json;
using Web.Api.Models;
using Web.Storages;

namespace Web.Api;

public sealed class SocialNetworkAdapter : ISocialNetworkAdapter
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorage _tokenStorage;

    public SocialNetworkAdapter(
        HttpClient httpClient,
        ITokenStorage tokenStorage)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
    }

    public Task RegisterAsync(Register.Request request, CancellationToken ct)
    {
        return _httpClient.PostAsJsonAsync("auth/register", request, ct);
    }

    public async Task<Login.Response> LoginAsync(Login.Request request, CancellationToken ct)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync("auth/login", request, ct);

        return await responseMessage.Content.ReadFromJsonAsync<Login.Response>(cancellationToken: ct)
               ?? throw ResponseCantBeNullException();
    }
    
    private async Task<HttpResponseMessage> MakeRequest(
        HttpMethod method,
        string path,
        object? body,
        CancellationToken ct)
    {
        var jwt = await _tokenStorage.Get(ct);

        var requestMessage = new HttpRequestMessage(
            method,
            path);
        requestMessage.Headers.Add("Authorization", $"Bearer {jwt}");

        if (body is not null)
        {
            requestMessage.Content = JsonContent.Create(body);
        }

        var responseMessage = await _httpClient.SendAsync(requestMessage, ct);
        responseMessage.EnsureSuccessStatusCode();

        return responseMessage;
    }

    private static InvalidOperationException ResponseCantBeNullException()
    {
        return new InvalidOperationException("Response can't be null");
    }
}