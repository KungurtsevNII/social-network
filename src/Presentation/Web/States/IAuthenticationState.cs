namespace Web.States;

public interface IAuthenticationState
{
    Task<bool> IsLoggedIn(CancellationToken ct);

    event Action<bool>? IsLoggedInChanged;

    Task LogIn(string username, string password, CancellationToken ct);

    Task LogOut(CancellationToken ct);

    Task<bool> RedirectIfNotAuthorized(CancellationToken ct);
}