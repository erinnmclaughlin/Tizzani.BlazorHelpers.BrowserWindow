using Microsoft.Extensions.DependencyInjection;
using Tizzani.BlazorHelpers.BrowserWindow.Services;

namespace Tizzani.BlazorHelpers.BrowserWindow.DependencyInjection;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBrowserWindowService(this IServiceCollection services)
    {
        return services
            .AddSingleton<BrowserResizeListener>()
            .AddSingleton<BrowserScrollListener>()
            .AddSingleton<IBrowserWindowService, BrowserWindowService>();
    }
}
