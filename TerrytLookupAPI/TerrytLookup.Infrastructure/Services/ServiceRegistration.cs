using Microsoft.Extensions.DependencyInjection;
using TerrytLookup.Core.Repositories;
using TerrytLookup.Infrastructure.Repositories;
using TerrytLookup.Infrastructure.Services.FeedDataService;
using TerrytLookup.Infrastructure.Services.StreetService;
using TerrytLookup.Infrastructure.Services.TownService;
using TerrytLookup.Infrastructure.Services.VoivodeshipService;

namespace TerrytLookup.Infrastructure.Services;

public static class ServiceRegistration
{
    public static void RegisterApiServices(this IServiceCollection services)
    {
        services.AddScoped<IVoivodeshipRepository, VoivodeshipRepository>();
        services.AddScoped<IVoivodeshipService, VoivodeshipService.VoivodeshipService>();

        services.AddScoped<ITownRepository, TownRepository>();
        services.AddScoped<ITownService, TownService.TownService>();

        services.AddScoped<IStreetRepository, StreetRepository>();
        services.AddScoped<IStreetService, StreetService.StreetService>();

        services.AddScoped<IFeedDataService, FeedDataService.FeedDataService>();
    }
}