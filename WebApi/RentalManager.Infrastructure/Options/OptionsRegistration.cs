using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RentalManager.Infrastructure.Options;

public static class OptionsRegistration
{
    public static void RegisterOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<IdentityServiceOptions>()
            .BindConfiguration(IdentityServiceOptions.IdentityService);

        builder.Services.AddOptions<DocumentServiceOptions>()
            .BindConfiguration(DocumentServiceOptions.DocumentService);
    }
}