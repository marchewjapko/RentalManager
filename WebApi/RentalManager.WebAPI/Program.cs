using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using RentalManager.Infrastructure.ExceptionHandling;
using RentalManager.Infrastructure.Models.Profiles;
using RentalManager.Infrastructure.Options;
using RentalManager.Infrastructure.Repositories.DbContext;
using RentalManager.Infrastructure.Services;
using RentalManager.WebAPI;

var builder = WebApplication.CreateBuilder(args);
const string allowSpecificOrigins = "allowSpecificOrigins";

// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

// Configure CORS
builder.Services.AddCors(options => {
    options.AddPolicy(allowSpecificOrigins, policy => {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

// Add HTTP client
builder.Services.AddHttpClient();

// Configure JSON options for controllers
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Add API explorer
builder.Services.AddEndpointsApiExplorer();

// Configure database context
if (builder.Configuration["InMemory"] == "True") {
    var guid = Guid.NewGuid().ToString();
    builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase(guid));
} else {
    builder.Services.AddDbContext<AppDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));
}

// Configure logging
builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

// Configure authentication
builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => {
        options.Authority = builder.Configuration["OpenIdProvider:TokenAuthority"];
        options.Audience = builder.Configuration["OpenIdProvider:TokenAudience"];
        options.MetadataAddress = builder.Configuration["OpenIdProvider:Configuration"]!;
    });

// Register application services
builder.Services.RegisterApiServices();
builder.Services.RegisterValidatorServices();
builder.Services.RegisterProfiles();
builder.Services.RegisterOptions();

// Register Swagger
builder.RegisterSwagger();

// Clear default logging providers and add console logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure middleware
app.UseSwagger();

if (builder.Environment.IsDevelopment()) {
    app.UseSwaggerUI(x => {
        x.OAuthClientId(builder.Configuration["OpenIdProvider:TokenAudience"]);
        x.OAuthClientSecret(builder.Configuration["OpenIdProvider:ClientSecret"]);
        x.OAuthScopes("openid", "profile", "last_name", "first_name", "id");
    });
} else {
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(allowSpecificOrigins);
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

// Migrate database if not using in-memory database
if (builder.Configuration["InMemory"] == "False") {
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    
    await context.Database.MigrateAsync();

    await app.RunAsync();
}

// Use exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

await app.RunAsync();