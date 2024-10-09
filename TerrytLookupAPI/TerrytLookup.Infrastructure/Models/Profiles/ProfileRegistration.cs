using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TerrytLookup.Infrastructure.Models.Profiles;

public static class ProfileRegistration
{
    public static void RegisterProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(ProfileRegistration)));
    }
}