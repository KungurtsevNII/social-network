using Application.Features.Auth.Command.Login;
using Application.Options;
using Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services, IConfiguration cfg) =>
        services
            .AddMediatR(typeof(LoginCommand).Assembly)
            .AddScoped<IEventBus, MediatorEventBus>()
            .Configure<CelebrityOptions>(cfg.GetSection(CelebrityOptions.OptionsPath));
}