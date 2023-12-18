using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RentalManager.Core.Domain;
using RentalManager.Infrastructure.Exceptions;
using RentalManager.Infrastructure.Repositories.DbContext;
using RentalManager.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
const string allowSpecificOrigins = "allowSpecificOrigins";

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
// builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "RentalManager.API", Version = "v1"
    });
    c.EnableAnnotations();

    c.TagActionsBy(d =>
    {
        return new List<string>() { d.ActionDescriptor.RouteValues["action"]! };
    });
});

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureIdentityOptions();

builder.Services.RegisterApiServices();
builder.Services.RegisterValidatorServices();

ProblemDetailsConfiguration.ConfigureCustomProblemDetails(builder.Services, builder.Environment);

if (builder.Environment.EnvironmentName == "InMemory")
{
    builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("TestingDatabase"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(
        options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DbConnectionString"))
    );
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(allowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.UseExceptionHandler();

if (builder.Environment.EnvironmentName != "InMemory")
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