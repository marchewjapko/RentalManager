using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Models.Profiles;
using RentalManager.Infrastructure.Options;
using RentalManager.Infrastructure.Repositories.DbContext;
using RentalManager.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
const string allowSpecificOrigins = "allowSpecificOrigins";

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options => {
    options.AddPolicy(allowSpecificOrigins,
        policy => {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

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

builder.Services.RegisterApiServices();

builder.Services.RegisterValidatorServices();

ProblemDetailsConfiguration.ConfigureCustomProblemDetails(builder.Services, builder.Environment);

if (builder.Configuration["InMemory"] == "True")
{
    var guid = Guid.NewGuid()
        .ToString();
    builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase(guid));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DbConnectionString"))
    );
}

builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => {
        options.Authority = builder.Configuration["OpenIdProvider:TokenAuthority"];
        options.Audience = builder.Configuration["OpenIdProvider:TokenAudience"];
        options.MetadataAddress = builder.Configuration["OpenIdProvider:Configuration"]!;
    });

builder.Services.RegisterProfiles();

builder.RegisterOptions();

var app = builder.Build();

app.UseSwagger();

if (builder.Environment.IsDevelopment())
{
    app.UseSwaggerUI(x => {
        x.OAuthClientId(builder.Configuration["OpenIdProvider:TokenAudience"]);
        x.OAuthClientSecret(builder.Configuration["OpenIdProvider:ClientSecret"]);
        x.OAuthScopes("openid", "profile", "last_name", "first_name", "id");
    });
}
else
{
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowSpecificOrigins);

app.MapControllers();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

if (builder.Configuration["InMemory"] == "False")
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations()
        .Any())
    {
        context.Database.Migrate();
    }
}

app.Run();