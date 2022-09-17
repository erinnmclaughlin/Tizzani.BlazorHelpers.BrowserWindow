using Microsoft.Extensions.DependencyInjection;
using Tizzani.BlazorHelpers.BrowserWindow.Services;

namespace Tizzani.BlazorHelpers.BrowserWindow.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBrowserWindowService(this IServiceCollection services)
    {
        return services.AddBrowserWindowService(BrowserWindowSettings.Default);
    }

    public static IServiceCollection AddBrowserWindowService(this IServiceCollection services, BrowserWindowSettings settings)
    {
        return services
            .AddSingleton(settings)
            .AddSingleton<BrowserWindowRegistrationService>()
            .AddSingleton<IBrowserWindowService, BrowserWindowService>();
    }
}
