using Application.Features.Auth.Command.Login;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services) =>
        services
            .AddMediatR(typeof(LoginCommand).Assembly)
            .AddScoped<IEventBus, MediatorEventBus>();
}