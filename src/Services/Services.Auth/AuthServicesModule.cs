using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Auth;

public static class AuthServicesModule
{
    public static IServiceCollection AddAuthServicesModule(this IServiceCollection services) =>
        services
            .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
}