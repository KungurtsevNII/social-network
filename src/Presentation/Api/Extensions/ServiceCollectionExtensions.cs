using System.Text;
using Api.HostedServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddHostedServices(this IServiceCollection services) =>
        services
            .AddHostedService<PostObserver>()
    ;
    
    internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) =>
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(setup =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
    
    internal static IServiceCollection AddJwtTokenAuthentication(
        this IServiceCollection services,
        IConfiguration cfg) => 
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg.GetValue<string>("TokenKey"))),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/post-hub"))) // Ensure that this path is the same as yours!
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                }).Services;

    internal static IServiceCollection AddCustomCors(this IServiceCollection services) =>
        services
            .AddCors(options =>
            {
                options.AddPolicy(
                    name: "_allowSpecificOrigins",
                    cors =>
                    {
                        cors.WithOrigins("https://localhost:7263","http://localhost:5263")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            })
        ;
}