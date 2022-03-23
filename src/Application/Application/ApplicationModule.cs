using Application.Features.Auth.Command.Login;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services) =>
        services
            .AddMediatR(typeof(LoginCommand).Assembly);
}