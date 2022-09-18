using Tizzani.BlazorHelpers.BrowserWindow.Models;

namespace Tizzani.BlazorHelpers.BrowserWindow.Services;

public interface IWindowService : IAsyncDisposable
{
    ValueTask<WindowDimensions> GetDimensions();
    ValueTask<PageOffset> GetPageOffset();
}