using Microsoft.Extensions.DependencyInjection;

namespace Tizzani.BlazorHelpers.BrowserWindow.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBrowserWindowService(this IServiceCollection services)
    {
        return services.AddSingleton<BrowserWindowService>();
    }
}
