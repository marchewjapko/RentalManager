using Microsoft.Extensions.DependencyInjection;

namespace RentalManager.Infrastructure.Options;

public static class OptionsRegistration
{
    public static void RegisterOptions(this IServiceCollection services)
    {
        services.AddOptions<IdentityServiceOptions>()
            .BindConfiguration(IdentityServiceOptions.IdentityService);
    }
}