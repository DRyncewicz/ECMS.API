using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace ecms.Infrastructure.Authorization;

public static class ConfigureOpenIDConnect
{
    public static IServiceCollection AddAuthBearer(this IServiceCollection services)
    {
        AddJWTAuthentication(services);
        services.AddAuthorizationBuilder()
                .AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("profile", "openid");
                });

        return services;
    }

    private static void AddJWTAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = "https://auth.ecms.ovh/";
            options.Audience = "https://auth.ecms.ovh/resources";
        });
    }
}