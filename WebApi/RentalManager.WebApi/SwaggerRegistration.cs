using Microsoft.OpenApi.Models;

namespace RentalManager.WebAPI;

public static class SwaggerRegistration
{
    public static void RegisterSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Rental Manager API",
                Version = "v1"
            });
            options.AddSecurityDefinition("OpenIdConnect", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = new Uri(builder.Configuration["OpenIdProvider:Configuration"]!)
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "OpenIdConnect"
                        }
                    },
                    []
                }
            });
            options.AddSecurityDefinition("JWT Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JWT Bearer"
                        }
                    },
                    []
                }
            });
        });
    }
}