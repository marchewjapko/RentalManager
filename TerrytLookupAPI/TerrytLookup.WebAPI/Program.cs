using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using TerrytLookup.Infrastructure.ExceptionHandling;
using TerrytLookup.Infrastructure.Models.Profiles;
using TerrytLookup.Infrastructure.Repositories.DbContext;
using TerrytLookup.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "TerrytLookup API",
        Description = """
                      <h3> This is a web API serving Terryt data </h3>
                      <br />
                      Terryt data can be fed via CSV files, that can be downloaded from the link below
                      <br />
                      <a href="https://eteryt.stat.gov.pl/eTeryt/rejestr_teryt/udostepnianie_danych/baza_teryt/uzytkownicy_indywidualni/pobieranie/pliki_pelne.aspx">https://eteryt.stat.gov.pl/eTeryt</a>
                      """
    });

    options.IncludeXmlComments(Assembly.GetExecutingAssembly());
});

if (builder.Configuration["InMemory"] == "True")
{
    var guid = Guid.NewGuid()
        .ToString();

    builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase(guid)
        .ConfigureWarnings(warning => warning.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString")));
}

builder.Services.RegisterProfiles();
builder.Services.RegisterApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();

// if (context.Database.IsRelational())
// {
//     context.Database.Migrate();
// }

app.Run();