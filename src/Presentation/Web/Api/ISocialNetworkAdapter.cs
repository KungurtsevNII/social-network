using Web.Api.Models;

namespace Web.Api;

public interface ISocialNetworkAdapter
{
    Task RegisterAsync(Register.Request request, CancellationToken ct);

    Task<Login.Response> LoginAsync(Login.Request request, CancellationToken ct);
}