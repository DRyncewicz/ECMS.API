using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ecms.API.OpenApi;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services, IConfiguration configuration)
    {
        SwaggerOptions swaggerOptions = configuration.GetSection(SwaggerOptions.SectionName).Get<SwaggerOptions>()!;

        services.AddSwaggerGen(o =>
        {
            o.OperationFilter<AuthorizeCheckOperationFilter>();
            o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme.ToLower(), new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(swaggerOptions.AuthorizationUrl),
                        TokenUrl = new Uri(swaggerOptions.TokenUrl),
                        Scopes = swaggerOptions.Scopes,
                    }
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            o.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}