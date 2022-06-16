namespace Web.Api.Models;

public sealed class Login
{
    public sealed record Request(string Email, string Password);
    public sealed record Response(string Token);
}